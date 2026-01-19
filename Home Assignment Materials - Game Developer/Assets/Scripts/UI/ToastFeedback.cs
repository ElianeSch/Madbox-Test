using DG.Tweening;
using TMPro;
using UnityEngine;

public class ToastFeedback : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private float fadeDuration = 0.4f;
    [SerializeField] private float stayDuration = 1.5f;

    public void Show(string message)
    {
        gameObject.SetActive(true);
        feedbackText.text = message;
        canvasGroup.DOKill();

        canvasGroup.alpha = 0f;

        Sequence s = DOTween.Sequence();
        s.Append(canvasGroup.DOFade(1f, fadeDuration));
        s.AppendInterval(stayDuration);
        s.Append(canvasGroup.DOFade(0f, fadeDuration));
        s.OnComplete(() => Destroy(gameObject));
    }
}
