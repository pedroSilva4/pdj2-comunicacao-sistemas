using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Collider))]
public class DoorTrigger : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private ScoreDoor scoreDoor;
    [SerializeField] private UIDocument uiDocument;

    [Header("Configuração da UI")]
    [SerializeField] private string winPanelName = "win-panel";
    [SerializeField] private string winLabelName = "win-label";
    [SerializeField] private string winMessage = "O Player entrou na porta!";

    private VisualElement winPanel;
    private Label winLabel;

    private void Awake()
    {

        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void Start()
    {
        if (uiDocument == null)
        {
            uiDocument = FindFirstObjectByType<UIDocument>();
        }

        if (uiDocument != null)
        {
            VisualElement root = uiDocument.rootVisualElement;
            winPanel = root.Q<VisualElement>(winPanelName);
            winLabel = root.Q<Label>(winLabelName);

            if (winPanel != null)
            {
                winPanel.style.display = DisplayStyle.None;
            }
            else
            {
                Debug.LogWarning($"Não foi encontrado nenhum VisualElement com o nome '{winPanelName}' no UIDocument.", this);
            }
        }
        else
        {
            Debug.LogError("Nenhum UIDocument foi atribuído ou encontrado na cena.", this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }


        if (scoreDoor != null && !scoreDoor.opened)
        {
            Debug.Log("O Player tocou na porta, mas ela ainda não está aberta (score insuficiente).", this);
            return;
        }

        ShowWinPanel();
    }

    private void ShowWinPanel()
    {
        if (winPanel != null)
        {
            if (winLabel != null)
            {
                winLabel.text = winMessage;
            }

            winPanel.style.display = DisplayStyle.Flex;
            Debug.Log("Win-panel ativado com sucesso!");
        }
        else
        {
            Debug.LogWarning("O Player entrou na porta, mas o win-panel não foi encontrado na UI.", this);
        }
    }
}