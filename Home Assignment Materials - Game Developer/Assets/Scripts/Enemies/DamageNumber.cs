using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float lifetime = 0.8f;
    [SerializeField] private float floatUpDistance = 0.8f;
    [SerializeField] private float popScale = 1.3f;
    [SerializeField] private float popDuration = 0.12f;
    [SerializeField] private float shakeStrength = 0.15f;

    public void Initialize(int damage)
    {
        text.text = damage.ToString();
        text.alpha = 1f;
        popScale *= Random.Range(0.9f, 1.1f);

        transform
            .DOScale(popScale, popDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() => transform.DOScale(1f*Random.Range(0.8f, 1.2f), 0.1f));

        transform.DOShakePosition(
            duration: 0.2f,
            strength: new Vector3(shakeStrength, shakeStrength, 0f),
            vibrato: 10,
            randomness: 90,
            fadeOut: true
        );

        transform.DOMoveY(
            transform.position.y + floatUpDistance,
            lifetime
        ).SetEase(Ease.OutCubic);

        text.DOFade(0f, lifetime * 0.7f)
            .SetDelay(lifetime * 0.3f);
        Destroy(gameObject, lifetime + 0.1f);
    }

    private void LateUpdate()
    {
        if (Camera.main != null)
            transform.forward = Camera.main.transform.forward;
    }
}
