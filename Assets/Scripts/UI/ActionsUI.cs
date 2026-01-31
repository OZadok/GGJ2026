using System;
using SuperMaxim.Messaging;
using UnityEngine;
using UnityEngine.UI;

public class ActionsUI : MonoBehaviour
{
    [SerializeField] private GameObject action1, action2, action3;
    [SerializeField] private Image[] action1Images;

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
        if (MainManager.Instance.playerPrefab.HaveBeer)
        {
            SetAction1Enabled();
        }
        else
        {
            SetAction1Disabled();
        }
    }

    private void SetAllActionsActive(bool active)
    {
        action1.SetActive(active);
        action2.SetActive(active);
        action3.SetActive(active);
    }

    private void SetAction1Disabled()
    {
        foreach (var action1Image in action1Images)
        {
            action1Image.color = Color.grey;
        }
    }
    
    private void SetAction1Enabled()
    {
        foreach (var action1Image in action1Images)
        {
            action1Image.color = Color.white;
        }
    }
}
