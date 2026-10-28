using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class DamageZone : MonoBehaviour
{
    [SerializeField, Min(0f)] private float damageAmount = 10f;
    [SerializeField, Min(0f)] private float damageCooldown = 1f;

    private float nextDamageTime;

    private void Awake()
    {
        Collider zoneCollider = GetComponent<Collider>();
        if (!zoneCollider.isTrigger)
        {
            Debug.LogError("O Collider da DamageZone tem de estar configurado como Trigger.", this);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ( damageAmount <= 0f || Time.time < nextDamageTime)
        {
            return;
        }

        Health health = other.GetComponentInParent<Health>();
        if (health == null)
        {
            return;
        }

        health.TakeDamage(damageAmount);
        nextDamageTime = Time.time + damageCooldown;
    }
}
