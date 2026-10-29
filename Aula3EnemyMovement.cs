using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Aula3EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Min(0f)] private float moveSpeed = 2f;
    [SerializeField, Min(0f)] private float followDistance = 7f;
    [SerializeField, Min(0f)] private float stopDistance = 1.2f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("Atribui o Player ao campo Target.", this);
            enabled = false;
        }
    }

    private void FixedUpdate()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        float distanceSquared = direction.sqrMagnitude;
        if (distanceSquared > followDistance * followDistance ||
            distanceSquared <= stopDistance * stopDistance)
        {
            return;
        }

        rb.MovePosition(rb.position + direction.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
