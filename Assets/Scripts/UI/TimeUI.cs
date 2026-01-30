using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    [SerializeField] Image fillImage;
    [SerializeField] TextMeshProUGUI timeLeftText;

    void Update()
    {
        var main = MainManager.Instance;
        if (main == null)
            return;

        float currentTime = main.RemainingTime;
        float levelTimeLength = main.LevelTimeSeconds;

        // Smoothly follow the authoritative timer so UI eases instead of snapping.
 
        float fillAmount = Mathf.InverseLerp(levelTimeLength, 0f, currentTime);
        fillImage.fillAmount = fillAmount;
 }


}
