using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Slider slider;

    private void OnEnable()
    {
        if (health == null || slider == null)
        {
            Debug.LogError("A HealthBar precisa de referências para Health e Slider.", this);
            return;
        }

        health.OnHealthChanged += UpdateDisplay;
        UpdateDisplay(health.GetCurrentHealth(), health.GetMaxHealth());
    }

    private void OnDisable()
    {
        if (health != null)
        {
            health.OnHealthChanged -= UpdateDisplay;
        }
    }

    private void UpdateDisplay(float currentHealth, float maxHealth)
    {
        slider.minValue = 0f;
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
    }
}
