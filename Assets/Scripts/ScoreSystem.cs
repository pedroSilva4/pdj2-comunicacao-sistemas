using System;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int score;

    public event Action<int> ScoreChanged;

    public int CurrentScore => score;

    public void AddPoints(int amount)
    {
        if (amount <= 0)
        {
            Debug.LogError("A pontuação a adicionar tem de ser positiva.", this);
            return;
        }

        // 1.º atualiza o estado, 2.º só depois notifica
        score += amount;
        ScoreChanged?.Invoke(score);
    }
}