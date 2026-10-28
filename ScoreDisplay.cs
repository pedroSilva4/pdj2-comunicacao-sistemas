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

        scoreSystem.ScoreChanged += UpdateDisplay;
    }

    private void OnDisable()
    {
        if (scoreSystem != null)
        {
            scoreSystem.ScoreChanged -= UpdateDisplay;
        }
    }

    private void Start()
    {
        var uiDocument = GetComponent<UIDocument>();
        scoreLabel = uiDocument.rootVisualElement.Q<Label>("score-label");

        UpdateDisplay(scoreSystem.CurrentScore);
    }

    private void UpdateDisplay(int value)
    {
        if (scoreLabel != null)
        {
            scoreLabel.text = $"Pontuação: {value}";
        }
    }
}
