using System;
using SuperMaxim.Messaging;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioEvent beerDrinking;
    [SerializeField] private AudioEvent punchTable;
    [SerializeField] private AudioEvent hurray;

    private void OnEnable()
    {
        Messenger.Default.Subscribe<PlayerDrinkingBeerEvent>(OnDrinkingBeer);
        Messenger.Default.Subscribe<PlayerPunchTableEvent>(OnPlayerPunchTable);
        Messenger.Default.Subscribe<PlayerHurrayEvent>(OnPlayerHurray);
    }

    private void OnDisable()
    {
        Messenger.Default.Unsubscribe<PlayerDrinkingBeerEvent>(OnDrinkingBeer);
        Messenger.Default.Unsubscribe<PlayerPunchTableEvent>(OnPlayerPunchTable);
        Messenger.Default.Unsubscribe<PlayerHurrayEvent>(OnPlayerHurray);
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
