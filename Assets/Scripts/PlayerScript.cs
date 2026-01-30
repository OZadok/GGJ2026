using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public List<Item> items;

    private Collector _collector;

    

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.ReadValueAsButton()) return;
        
        // check for interaction that can be
        // e.g. take beer, join zone, etc...
        if (_collector)
        {
            _collector.Collect(this);
        }
    }

    public void SetCollector(Collector collector)
    {
        _collector = collector;
    }

    private void JoinZone(ZoneData zoneData)
    {
        // put player in the position he needs to be?
        // check for valid group.
        // if player is not in valid group
        //   zone.playerIsNotInValidGroup()
        // grace time...
        // wait for some action?
    }
}
