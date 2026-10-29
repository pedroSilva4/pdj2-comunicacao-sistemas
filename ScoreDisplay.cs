using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private ScoreSystem scoreSystem;

    private Label scoreLabel;

    private void OnEnable()
    {
        if (scoreSystem == null)
        {
            Debug.LogError("Atribui o ScoreSystem ao ScoreDisplay no Inspector.", this);
            enabled = false;
            return;
        }

        var uiDocument = GetComponent<UIDocument>();
        scoreLabel = uiDocument.rootVisualElement.Q<Label>("score-label");

        if (scoreLabel == null)
        {
            Debug.LogError("Não foi encontrado nenhum Label com o nome 'score-label' no UIDocument.", this);
            return;
        }

        scoreSystem.ScoreChanged += UpdateDisplay;
        UpdateDisplay(scoreSystem.CurrentScore);
    }

    private void OnDisable()
    {
        if (scoreSystem != null)
        {
            scoreSystem.ScoreChanged -= UpdateDisplay;
        }
    }

    private void UpdateDisplay(int value)
    {
        if (scoreLabel != null)
        {
            scoreLabel.text = $"Pontuação: {value}";
        }
    }
}