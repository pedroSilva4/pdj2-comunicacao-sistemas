using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Min(1f)] private float maxHealth = 100f;

    private float currentHealth;
    private bool isDead;

    public event Action<float, float> OnHealthChanged;
    public event Action OnDead;

    private void OnValidate()
    {
        maxHealth = Mathf.Max(1f, maxHealth);
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(float damageAmount)
    {
        if (isDead || damageAmount <= 0f)
        {
            return;
        }

        currentHealth = Mathf.Max(0f, currentHealth - damageAmount);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth == 0f)
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        if (isDead || healAmount <= 0f)
        {
            return;
        }

        float previousHealth = currentHealth;
        currentHealth = Mathf.Min(maxHealth, currentHealth + healAmount);

        if (currentHealth != previousHealth)
        {
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        OnDead?.Invoke();
        gameObject.SetActive(false);
    }
}
