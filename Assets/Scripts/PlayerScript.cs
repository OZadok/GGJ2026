using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public List<Wearables> wearables;
    public List<Item> items;
    
    private void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            // check for interaction that can be
            // e.g. take beer, join zone, etc...
        }
    }

    private void JoinZone(/*ZoneData zoneData*/)
    {
        // put player in the position he needs to be?
        // check for valid group.
        // if player is not in valid group
        //   zone.playerIsNotInValidGroup()
        // grace time...
        // wait for some action?
    }
}
