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

        // TODO: subscrever a ScoreChanged e verificar a pontuação atual.
    }

    private void OnDisable()
    {
        // TODO: remover a subscrição a ScoreChanged.
    }

    private void CheckScore(int currentScore)
    {
        if (opened || currentScore < requiredScore)
        {
            return;
        }
        opened = true;
        doorCollider.isTrigger = true;
        doorRenderer.enabled = false;
        // TODO: marcar a porta como aberta, transformar o collider em trigger
        // e desativar o MeshRenderer sem desativar o GameObject.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!opened || !other.CompareTag("Player"))
        {
            return;
        }

        // TODO (exercício extra): notificar que o Player entrou na porta.
        // Usa OnDoorEntered para que um apresentador independente mostre a vitória.
    }
}
