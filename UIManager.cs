using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UIManager : MonoBehaviour
{
    [SerializeField] private Health playerHealth;

    private Label healthLabel;
    private VisualElement gameOverPanel;
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
        isSubscribed = true;

        UpdateHealthDisplay(playerHealth.GetCurrentHealth(), playerHealth.GetMaxHealth());
        gameOverPanel.style.display = playerHealth.IsDead() ? DisplayStyle.Flex : DisplayStyle.None;
        initializationRoutine = null;
    }

    private void OnDisable()
    {
        if (initializationRoutine != null)
        {
            StopCoroutine(initializationRoutine);
            initializationRoutine = null;
        }

        if (isSubscribed && playerHealth != null)
        {
            playerHealth.OnHealthChanged -= UpdateHealthDisplay;
            playerHealth.OnDead -= ShowGameOver;
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
}
