using System;
using Core;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private GameObject _interactButtonView;
    
    [SerializeField] private Item _item;

    private void Start()
    {
        _interactButtonView.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        _interactButtonView.SetActive(true);

        other.GetComponent<PlayerScript>().SetCollector(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        Hide(other.GetComponent<PlayerScript>());
    }

    private void Hide(PlayerScript player)
    {
        _interactButtonView.SetActive(false);
        player.SetCollector(null);
    }
    

    public void Collect(PlayerScript player)
    {
        Hide(player);
        player.GetComponent<EntityScript>().SetItem(_item);
    }
}
