using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

[RequireComponent(typeof(UIDocument))]
public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private ScoreSystem scoreSystem;
    private Label scoreLabel;
    private Coroutine initializationRoutine;
    private bool isSubscribed;

    private void OnEnable()
    {
        if (scoreSystem == null)
        {
            Debug.LogError("Atribui o ScoreSystem ao ScoreDisplay no Inspector.", this);
            enabled = false;
            return;
        }

        initializationRoutine = StartCoroutine(InitializeWhenReady());
    }
    private IEnumerator InitializeWhenReady(){
        yield return null;
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        scoreLabel = root.Q<Label>("score-label");
        if (scoreLabel == null){
            Debug.LogError("Não encontrei o Label 'score-label' no UXML.", this);
            initializationRoutine = null;
            yield break;
        }
        scoreSystem.ScoreChanged += UpdateDisplay;
        isSubscribed = true;
        UpdateDisplay(scoreSystem.CurrentScore);
        initializationRoutine = null;
    }
    private void OnDisable(){
        if (initializationRoutine != null){
            StopCoroutine(initializationRoutine);
            initializationRoutine = null;
        }
        if (isSubscribed && scoreSystem != null){
            scoreSystem.ScoreChanged -= UpdateDisplay;
            isSubscribed = false;
        }
    }

    private void UpdateDisplay(int value){
        if (scoreLabel != null)
        {
            scoreLabel.text = $"Pontuação: {value}";
        }
    }
}
