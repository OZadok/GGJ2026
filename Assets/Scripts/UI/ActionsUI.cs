using System;
using SuperMaxim.Messaging;
using UnityEngine;

public class ActionsUI : MonoBehaviour
{
    [SerializeField] private GameObject action1, action2, action3;

    private void Start()
    {
        SetAllActionsActive(false);
    }

    private void OnEnable()
    {
        Messenger.Default.Subscribe<PlayerZoneChangeEvent>(OnPlayerZoneChanged);
    }

    private void OnDisable()
    {
        Messenger.Default.Unsubscribe<PlayerZoneChangeEvent>(OnPlayerZoneChanged);
    }

    private void OnPlayerZoneChanged(PlayerZoneChangeEvent playerZoneChangeEvent)
    {
        // if can't drink beer, don't show the target action.
        SetAllActionsActive(playerZoneChangeEvent.Zone);
    }

    private void SetAllActionsActive(bool active)
    {
        action1.SetActive(active);
        action2.SetActive(active);
        action3.SetActive(active);
    }
}
