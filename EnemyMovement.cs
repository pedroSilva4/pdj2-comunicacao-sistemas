using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Min(0f)] private float moveSpeed = 2f;

    [SerializeField, Min(0f)] private float stopDistance = 1.5f;

    [SerializeField, Min(4f)] private float followDistance = 5f;



    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("Atribui um alvo ao EnemyController no Inspector.", this);
            enabled = false;
        }
    }

    private void Update()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > stopDistance * stopDistance)
        {
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        }
    }
}
