using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerScript : MonoBehaviour
{
    private Zone _zone;
    private EntityScript _entityScript;
    
    private Group _group;

    private bool _action1, _action2, _action3;

    private void Awake()
    {
        _entityScript =  GetComponent<EntityScript>();
    }

    public void JoinZone(Zone zone)
    {
        Debug.Log("Join Zone " + zone.name);

        
        _group = MainManager.Instance.GetGroupIsBlendingTo(_entityScript);
        zone.AlertNpcs(_group);
        _zone = zone;
        
        // put player in the position he needs to be?
        if (_group)
        {
            StartCoroutine(WaitForAction());
        }
    }

    private IEnumerator WaitForAction()
    {
        var timeToAction = _group.actions[0].maxTime;
        var startTime = Time.time;
        while (_zone)
        {
            yield return null;
            if (_action1 && _group.actions[0].type == ActionsType.Drink 
                || _action2 && _group.actions[0].type == ActionsType.PunchTable)
            {
                _zone.AlertNpcs(true);
                startTime = Time.time;
                _action1 = false;
                _action2 = false;
                continue;
            }
            else if (_action1 || _action2)
            {
                // incorrect action
                _zone.AlertNpcs(false);
                startTime = Time.time;
                _action1 = false;
                _action2 = false;
                continue;
            }
            
            if (Time.time - startTime > timeToAction)
            {
                _zone.AlertNpcs(false);
            }
        }
    }

    public void OnAction1(InputAction.CallbackContext context)
    {
        _action1 = context.performed;
        if (_action1)
        {
            _entityScript.DoAction1();
        }
    }
    
    public void OnAction2(InputAction.CallbackContext context)
    {
        _action2 = context.performed;
        if (_action2)
        {
            _entityScript.DoAction2();
        }
    }
    
    public void OnAction3(InputAction.CallbackContext context)
    {
        _action3 = context.performed;
        if (_action3)
        {
            _entityScript.DoAction3();
        }
    }
}
