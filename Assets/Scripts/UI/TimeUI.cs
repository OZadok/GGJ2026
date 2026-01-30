using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    [SerializeField] Image fillImage;
    [SerializeField] TextMeshProUGUI timeLeftText;

    void Start()
    {
        MainManager.Instance.OnTimerUpdated += UpdateTimeUI;
    }

    private void UpdateTimeUI(int currentTime, int levelTimeLength)
    {
        float fillAmount = Mathf.InverseLerp(levelTimeLength, 0, currentTime);
        fillImage.fillAmount = fillAmount;

        timeLeftText.text = currentTime.ToString();
    }
}
