using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    [SerializeField] private char character;
    [SerializeField] private Image keyImage;

    private void OnEnable() => PlayerScript.OnActionPreformed += OnActionPreformed;
    private void OnDisable() => PlayerScript.OnActionPreformed -= OnActionPreformed;

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
    }
}
