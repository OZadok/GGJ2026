using System;
using UnityEngine;
using UnityEngine.UI;

public class BlendingLevelUI : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private PlayerScript _playerScript;
    private void Start()
    {
        _playerScript = FindAnyObjectByType<PlayerScript>();
    }

    private void FixedUpdate()
    {
        slider.value = Mathf.Lerp(slider.value, _playerScript.BlendingLevel, 0.1f);
    }
}
