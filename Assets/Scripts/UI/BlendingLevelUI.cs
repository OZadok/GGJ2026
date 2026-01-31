using System;
using UnityEngine;
using UnityEngine.UI;

public class BlendingLevelUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image blendingLevelImage;
    [SerializeField] private Color lowColor = Color.red;
    [SerializeField] private Color midColor = Color.yellow;
    [SerializeField] private Color highColor = Color.green;

    private PlayerScript _playerScript;
    private void Start()
    {
        _playerScript = FindAnyObjectByType<PlayerScript>();
    }

    private void FixedUpdate()
    {
        slider.value = Mathf.Lerp(slider.value, _playerScript.BlendingLevel, 0.1f);
        SetSliderColor();
    }

    private const float LowThreshold = 10f;
    private const float MidThreshold = 70f;

    private void SetSliderColor()
    {
        if (slider.value <= LowThreshold)
        {
            blendingLevelImage.color = lowColor;
        }
        else if (slider.value <= MidThreshold)
        {
            blendingLevelImage.color = Color.Lerp(lowColor, midColor, (slider.value - LowThreshold) / (MidThreshold - LowThreshold));
        }
        else
        {
            blendingLevelImage.color = Color.Lerp(midColor, highColor, (slider.value - MidThreshold) / (100 - MidThreshold));
        }
    }
}
