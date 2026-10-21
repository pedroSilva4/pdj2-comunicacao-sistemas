using System;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(MeshRenderer))]
public class ScoreDoor : MonoBehaviour
{
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField, Min(1)] private int requiredScore = 2;

    public event Action OnDoorEntered;

    private Collider doorCollider;
    private MeshRenderer doorRenderer;
    private bool opened;

    private void Awake()
    {
        doorCollider = GetComponent<Collider>();
        doorRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        if (scoreSystem == null)
        {
            Debug.LogError("Atribui o ScoreSystem à ScoreDoor no Inspector.", this);
            enabled = false;
            return;
        }

        scoreSystem.ScoreChanged += CheckScore;
        CheckScore(scoreSystem.CurrentScore);
    }

    private void OnDisable()
    {
        if (scoreSystem != null)
        {
            scoreSystem.ScoreChanged -= CheckScore;
        }
    }

    private void CheckScore(int currentScore)
    {
        if (opened || currentScore < requiredScore)
        {
            return;
        }

        opened = true;
        doorCollider.isTrigger = true;   // deixa de bloquear
        doorRenderer.enabled = false;    // fica invisível; o GameObject continua ativo
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!opened || !other.CompareTag("Player"))
        {
            return;
        }

        OnDoorEntered?.Invoke();
    }
}