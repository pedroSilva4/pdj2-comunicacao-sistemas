using System;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(MeshRenderer))]
public class ScoreDoor : MonoBehaviour
{
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField, Min(1)] private int requiredScore = 2;

    public static event System.Action OnDoorEntered;

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

        if (doorCollider != null) doorCollider.isTrigger = true;
        if (doorRenderer != null) doorRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!opened || !other.CompareTag("Player"))
        {
            return;
        }

        OnDoorEntered?.Invoke();
        Debug.Log("Vitória! Evento OnDoorEntered enviado.");
    }
    // TODO (exercício extra): notificar que o Player entrou na porta.
    // Usa OnDoorEntered para que um apresentador independente mostre a vitória.
}