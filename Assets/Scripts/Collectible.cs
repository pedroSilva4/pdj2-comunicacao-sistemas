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

        //Debug.LogWarning("TODO: pedir ao ScoreSystem para adicionar pontos e desativar o item.", this);
        scoreSystem.AddPoints(points);
        collected = true;
        gameObject.SetActive(false);
    }
}
