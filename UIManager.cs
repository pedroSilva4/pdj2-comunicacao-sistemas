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
    private Label victoryPanel;


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

        // CORREÇÃO: Vai buscar o painel de vitória do teu UXML (garante que crias um com este nome no UI Builder)
        victoryPanel = root.Q<Label>("victory-panel");

        if (playerHealth == null || healthLabel == null || gameOverPanel == null)
        {
            Debug.LogError(
                "Atribui a Health do Player e confirma os elementos health-label e game-over-panel no UXML.",
                this);
            initializationRoutine = null;
            yield break;
        }

        // Subscrições existentes da vida do Player
        playerHealth.OnHealthChanged += UpdateHealthDisplay;
        playerHealth.OnDead += ShowGameOver;

        // CORREÇÃO: Subscreve ao evento de vitória da Porta
        ScoreDoor.OnDoorEntered += ShowVictory;

        isSubscribed = true;

        UpdateHealthDisplay(playerHealth.GetCurrentHealth(), playerHealth.GetMaxHealth());
        gameOverPanel.style.display = playerHealth.IsDead() ? DisplayStyle.Flex : DisplayStyle.None;

        // CORREÇÃO: Garante que a vitória começa escondida
        if (victoryPanel != null) victoryPanel.style.display = DisplayStyle.None;

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

            // CORREÇÃO: Remove a subscrição da vitória aqui também!
            ScoreDoor.OnDoorEntered -= ShowVictory;

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
    private void ShowVictory()
    {
        // Este aviso vai aparecer na consola da Unity assim que passares a porta
        Debug.Log("A função ShowVictory foi chamada com sucesso pela Porta!");

        if (victoryPanel != null)
        {
            victoryPanel.style.display = DisplayStyle.Flex;
            Debug.Log("O texto Victory! foi ativado no ecrã.");
        }
        else
        {
            // Se este aviso aparecer, significa que o nome no UI Builder está diferente!
            Debug.LogError("Erro: O UIManager não encontrou nenhum Label com o nome 'victory-panel'!");
        }
    }

}
