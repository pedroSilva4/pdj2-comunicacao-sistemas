using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Health playerHealth;
    [SerializeField] private ScoreDoor scoreDoor;

    private Label healthLabel;
    private VisualElement gameOverPanel;
    private VisualElement winPanel;
    private Label winLabel;

    private Coroutine initializationRoutine;
    private bool isSubscribed;

    private void OnEnable()
    {
        initializationRoutine = StartCoroutine(InitializeWhenReady());
    }

    private IEnumerator InitializeWhenReady()
    {
        yield return null;

        UIDocument uiDocument = GetComponent<UIDocument>();
        VisualElement root = uiDocument.rootVisualElement;

        healthLabel = root.Q<Label>("health-label");
        gameOverPanel = root.Q<VisualElement>("game-over-panel");

        winPanel = root.Q<VisualElement>("winPanel");
        winLabel = root.Q<Label>("win-label");

        if (playerHealth == null || healthLabel == null || gameOverPanel == null)
        {
            Debug.LogError(
                "Atribui a Health do Player e confirma os elementos health-label e game-over-panel no UXML.",
                this);
            initializationRoutine = null;
            yield break;
        }
        playerHealth.OnHealthChanged += UpdateHealthDisplay;
        playerHealth.OnDead += ShowGameOver;

        if (scoreDoor != null)
        {
            scoreDoor.OnDoorEntered += ShowWinMessage;
        }

        isSubscribed = true;

        UpdateHealthDisplay(playerHealth.GetCurrentHealth(), playerHealth.GetMaxHealth());
        gameOverPanel.style.display = playerHealth.IsDead() ? DisplayStyle.Flex : DisplayStyle.None;

        if (winPanel != null)
        {
            winPanel.style.display = DisplayStyle.None;
        }

        initializationRoutine = null;
    }

    private void OnDisable()
    {
        if (initializationRoutine != null)
        {
            StopCoroutine(initializationRoutine);
            initializationRoutine = null;
        }

        if (isSubscribed)
        {
            if (playerHealth != null)
            {
                playerHealth.OnHealthChanged -= UpdateHealthDisplay;
                playerHealth.OnDead -= ShowGameOver;
            }

            if (scoreDoor != null)
            {
                scoreDoor.OnDoorEntered -= ShowWinMessage;
            }

            isSubscribed = false;
        }
    }

    private void UpdateHealthDisplay(float currentHealth, float maxHealth)
    {
        if (healthLabel != null)
        {
            healthLabel.text = $"Health: {currentHealth:F0} / {maxHealth:F0}";
        }
    }

    private void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.style.display = DisplayStyle.Flex;
        }
    }

    private void ShowWinMessage()
    {
        if (winPanel != null)
        {
            winPanel.style.display = DisplayStyle.Flex;
            if (winLabel != null)
            {
                winLabel.text = "O Player entrou na porta!";
            }
        }
        else
        {
            Debug.Log("O Player entrou na porta!");
        }
    }
}