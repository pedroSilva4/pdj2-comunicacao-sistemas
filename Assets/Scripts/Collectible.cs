using UnityEngine;

public class Collectible : MonoBehaviour, IInteractable
{
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField, Min(1)] private int points = 1;

    private bool collected;

    public void Interact(GameObject interactor)
    {
        if (collected)
        {
            return;
        }

        if (scoreSystem == null)
        {
            Debug.LogError("Atribui o ScoreSystem ao Collectible no Inspector.", this);
            return;
        }

        // Marcar ANTES de tudo: evita recolha dupla se o trigger disparar duas vezes
        collected = true;
        scoreSystem.AddPoints(points);
        gameObject.SetActive(false);
    }
}