using System;
using System.Collections;
using SuperMaxim.Messaging;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerScript : MonoBehaviour
{

    public bool IsSus { get; private set; }
    private Zone _zone;
    private EntityScript _entityScript;
    
    private Group _group;

    private bool _action1, _action2, _action3;

    private bool _actionIsPressed;
    
    private Coroutine _waitFOrActionCoroutine;

    private void Awake()
    {
        IsSus = true;
        _entityScript =  GetComponent<EntityScript>();
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
        IsSus = true;
        if (_waitFOrActionCoroutine != null)
        {
            StopCoroutine(_waitFOrActionCoroutine);
        }
    }

    public void JoinZone(Zone zone)
    {
        Debug.Log("Join Zone " + zone.name);
        
        _group = MainManager.Instance.GetGroupIsBlendingTo(_entityScript);
        IsSus = false;
        zone.AlertNpcs(_group);
        _zone = zone;
        
        // put player in the position he needs to be?
        if (_group)
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
                IsSus = true;
                startTime = Time.time;
                _action1 = false;
                _action2 = false;
                _action3 = false;
                continue;
            }
            
            if (Time.time - startTime > timeToAction)
            {
                _zone.AlertNpcs(false);
                IsSus = true;
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
        }
    }
    
    public void OnAction2(InputAction.CallbackContext context)
    {
        _action2 = context.performed;
        if (_action2&& _zone)
        {
            _actionIsPressed = true;
            _entityScript.DoAction2();
        }
    }
    
    public void OnAction3(InputAction.CallbackContext context)
    {
        _action3 = context.performed;
        if (_action3&& _zone)
        {
            _actionIsPressed = true;
            _entityScript.DoAction3();
        }
    }
}
