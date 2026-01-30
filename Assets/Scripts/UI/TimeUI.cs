using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    [SerializeField] Image fillImage;
    [SerializeField] TextMeshProUGUI timeLeftText;
    [SerializeField, Tooltip("Seconds it takes for the radial fill to catch up to the timer value.")] float fillLerpDuration = 0.25f;

    float targetFill;

    void Start()
    {
        MainManager.Instance.OnTimerUpdated += UpdateTimeUI;
    }

    void OnDestroy()
    {
        if (MainManager.Instance != null)
        {
            MainManager.Instance.OnTimerUpdated -= UpdateTimeUI;
        }
    }

    void Update()
    {
        float step = Time.deltaTime / Mathf.Max(0.001f, fillLerpDuration);
        fillImage.fillAmount = Mathf.MoveTowards(fillImage.fillAmount, targetFill, step);
    }

    private void UpdateTimeUI(float currentTime, float levelTimeLength)
    {
        targetFill = Mathf.InverseLerp(levelTimeLength, 0, currentTime);
        timeLeftText.text = Mathf.CeilToInt(currentTime).ToString();
    }
}
