using System;
using SuperMaxim.Messaging;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioEvent beerDrinking;

    private void OnEnable()
    {
        Messenger.Default.Subscribe<PlayerDrinkingBeerEvent>(OnDrinkingBeer);
    }

    private void OnDisable()
    {
        Messenger.Default.Unsubscribe<PlayerDrinkingBeerEvent>(OnDrinkingBeer);
    }
    
    private void OnDrinkingBeer(PlayerDrinkingBeerEvent obj)
    {
        beerDrinking.Play();
    }
}
