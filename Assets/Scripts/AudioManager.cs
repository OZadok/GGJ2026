using System;
using SuperMaxim.Messaging;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioEvent beerDrinking;
    [SerializeField] private AudioEvent punchTable;
    [SerializeField] private AudioEvent hurray;
    [SerializeField] private AudioEvent npcSuspicious;
    [SerializeField] private AudioEvent npcNotSuspicious;

    private void OnEnable()
    {
        Messenger.Default.Subscribe<PlayerDrinkingBeerEvent>(OnDrinkingBeer);
        Messenger.Default.Subscribe<PlayerPunchTableEvent>(OnPlayerPunchTable);
        Messenger.Default.Subscribe<PlayerHurrayEvent>(OnPlayerHurray);
        Messenger.Default.Subscribe<NpcSuspiciousEvent>(OnNpcSuspicious);
    }

    private void OnDisable()
    {
        Messenger.Default.Unsubscribe<PlayerDrinkingBeerEvent>(OnDrinkingBeer);
        Messenger.Default.Unsubscribe<PlayerPunchTableEvent>(OnPlayerPunchTable);
        Messenger.Default.Unsubscribe<PlayerHurrayEvent>(OnPlayerHurray);
        Messenger.Default.Unsubscribe<NpcSuspiciousEvent>(OnNpcSuspicious);
    }

    private void OnNpcSuspicious(NpcSuspiciousEvent npcSuspiciousEvent)
    {
        if (npcSuspiciousEvent.IsSuspicious)
        {
            npcSuspicious?.Play();
        }
        else
        {
            npcNotSuspicious?.Play();
        }
    }

    private void OnPlayerHurray(PlayerHurrayEvent obj)
    {
        hurray?.Play();
    }

    private void OnPlayerPunchTable(PlayerPunchTableEvent obj)
    {
        punchTable?.Play();
    }

    private void OnDrinkingBeer(PlayerDrinkingBeerEvent obj)
    {
        beerDrinking.Play();
    }
}
