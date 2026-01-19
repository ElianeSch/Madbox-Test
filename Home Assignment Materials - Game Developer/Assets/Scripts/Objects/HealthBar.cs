using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image fillImage;

    private void Awake()
    {
        if (health != null)
            health.OnHealthChanged += UpdateHealthBar;
        gameObject.SetActive(false);
    }

    private void UpdateHealthBar(int current, int max, int damage)
    {
        fillImage.fillAmount = (float)current / max;
        gameObject.SetActive(current > 0);
    }
}
