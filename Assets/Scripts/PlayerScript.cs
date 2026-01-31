using System.Collections;
using SuperMaxim.Core.Extensions;
using SuperMaxim.Messaging;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] private int penaltyAmount = 15;
    [SerializeField] private int successAmount = 20;
    [SerializeField] private int zoneJoiningBonusAmount = 40;
    [SerializeField] public int winningThreshold = 70;

    public bool IsSus => BlendingLevel < winningThreshold;

    public int BlendingLevel
    {
        get => _blendingLevel;
        set => _blendingLevel = Mathf.Max(0, value);
    }

    private Zone _zone;
    private EntityScript _entityScript;
    
    private Group _group;

    private bool _action1, _action2, _action3;

    private bool _actionIsPressed;
    
    private Coroutine _waitFOrActionCoroutine;
    private Vector3 _originPosition;

    private int _blendingLevel;
    
    private void Awake()
    {
        _entityScript =  GetComponent<EntityScript>();
        _originPosition = transform.position;
    }

    private void OnEnable()
    {
        Messenger.Default.Subscribe<PlayerZoneChangeEvent>(OnPlayerZoneChange);
    }

    private void OnDisable()
    {
        Messenger.Default.Unsubscribe<PlayerZoneChangeEvent>(OnPlayerZoneChange);
    }

    private void OnPlayerZoneChange(PlayerZoneChangeEvent playerZoneChangeEvent)
    {
        if (playerZoneChangeEvent.Zone) return;
        _zone = null;
        BlendingLevel -= zoneJoiningBonusAmount;
        if (_waitFOrActionCoroutine != null)
        {
            StopCoroutine(_waitFOrActionCoroutine);
        }
    }
    public Zone GetCurrentZone()
    {
        return _zone;
    }

    public void Reset()
    {
        transform.position = _originPosition;
        foreach (var entityScriptItem in _entityScript.items)
        {
            Destroy(entityScriptItem.Value);
        }
        _entityScript.items.Clear();
        BlendingLevel = 0;
    }
    
    public void JoinZone(Zone zone)
    {
        Debug.Log("Join Zone " + zone.name);
        _zone = zone;
        
        _group = MainManager.Instance.GetGroupIsBlendingTo(_entityScript);
        BlendingLevel += zoneJoiningBonusAmount;
        zone.AlertNpcs(_group);
        
        // put player in the position he needs to be?
        if (_group && _group.actions.Count > 0)
        {
            _waitFOrActionCoroutine = StartCoroutine(WaitForAction());
        }

        Messenger.Default.Publish(new PlayerZoneChangeEvent(zone));
    }

    private IEnumerator WaitForAction()
    {
        var action = _group.actions[0];
        var timeToAction = action.maxTime;
        var startTime = Time.time;
        
        yield return new WaitUntil(() => _actionIsPressed || Time.time - startTime > timeToAction);
        _actionIsPressed = false;
        if (_action1 && action.type == ActionsType.Drink
            || _action2 && action.type == ActionsType.PunchTable
            || _action3 && action.type == ActionsType.Hurray)
        {
            _zone.AlertNpcs(true);
            BlendingLevel += successAmount;
            _action1 = false;
            _action2 = false;
            _action3 = false;
        }
        
        startTime = Time.time;
        
        while (_zone)
        {
            yield return null;
            if (Time.time - startTime >= action.minTime && 
                (_action1 && action.type == ActionsType.Drink
                 || _action2 && action.type == ActionsType.PunchTable
                 || _action3 && action.type == ActionsType.Hurray))
            {
                _zone.AlertNpcs(true);
                BlendingLevel += successAmount;
                startTime = Time.time;
                _action1 = false;
                _action2 = false;
                _action3 = false;
                continue;
            }
            else if (_action1 || _action2 || _action3)
            {
                // incorrect action
                _zone.AlertNpcs(false);
                BlendingLevel -= penaltyAmount;
                startTime = Time.time;
                _action1 = false;
                _action2 = false;
                _action3 = false;
                continue;
            }
            
            if (Time.time - startTime > timeToAction)
            {
                _zone.AlertNpcs(false);
                BlendingLevel -= penaltyAmount;
                startTime = Time.time;
            }
        }
    }

    public void OnAction1(InputAction.CallbackContext context)
    {
        _action1 = context.performed;
        if (_action1 && _zone)
        {
            _actionIsPressed = true;
            _entityScript.DoAction1();
            Messenger.Default.Publish(new PlayerDrinkingBeerEvent());
        }
    }
    
    public void OnAction2(InputAction.CallbackContext context)
    {
        _action2 = context.performed;
        if (_action2&& _zone)
        {
            _actionIsPressed = true;
            _entityScript.DoAction2();
            Messenger.Default.Publish(new PlayerPunchTableEvent());
        }
    }
    
    public void OnAction3(InputAction.CallbackContext context)
    {
        _action3 = context.performed;
        if (_action3&& _zone)
        {
            _actionIsPressed = true;
            _entityScript.DoAction3();
            Messenger.Default.Publish(new PlayerHurrayEvent());
        }
    }
}
