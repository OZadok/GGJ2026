using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    [SerializeField] private char character;
    [SerializeField] private Image keyImage;
    [SerializeField] private Image cooldownImage;

    private RectTransform _cooldownRT;

    private float _originalOffsetMinY;
    private float _originalOffsetMaxY;
    private PlayerScript _player;
    
    private void Awake()
    {
        _player = GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<PlayerScript>();
        

        _cooldownRT = cooldownImage.rectTransform;

        // SAVE ORIGINAL "100%" STATE
        _originalOffsetMinY = _cooldownRT.offsetMin.y;
        _originalOffsetMaxY = _cooldownRT.offsetMax.y;
    }

    private void OnEnable() => _player.OnActionPreformed += OnActionPreformed;
    private void OnDisable() => _player.OnActionPreformed -= OnActionPreformed;

    private void OnActionPreformed(char keyPressed)
    {
        if (keyPressed != character) return;
        AnimateButton();
    }
    
    private void AnimateButton()
    {
        Sequence seq = DOTween.Sequence();
        
        seq.Append(keyImage.transform.DOScale(0.9f, 0.2f));
        seq.Join(keyImage.DOColor(keyImage.color * 0.7f, 0.2f));
        
        seq.Append(keyImage.transform.DOScale(1f, 0.2f));
        seq.Join(keyImage.DOColor(Color.white, 0.2f));
        DisplayCooldown();
    }
    
    private void DisplayCooldown()
    {
        cooldownImage.gameObject.SetActive(true);
        DOTween.Kill(_cooldownRT);

        // RESET TO FULL
        _cooldownRT.offsetMin =
            new Vector2(_cooldownRT.offsetMin.x, _originalOffsetMinY);
        _cooldownRT.offsetMax =
            new Vector2(_cooldownRT.offsetMax.x, _originalOffsetMaxY);

        float endY = -_cooldownRT.rect.height;

        DOTween.To(
            () => _cooldownRT.offsetMax.y,
            y => _cooldownRT.offsetMax =
                new Vector2(_cooldownRT.offsetMax.x, y),
            endY,
            _player.ActionCoolDown
        );
    }
}
