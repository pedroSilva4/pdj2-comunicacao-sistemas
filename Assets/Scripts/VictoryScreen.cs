using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private ScoreDoor door;

    private VisualElement victoryPanel;
    private Coroutine initializationRoutine;
    private bool isSubscribed;

    private void OnEnable()
    {
        if (door == null)
        {
            Debug.LogError("Atribui a ScoreDoor ao VictoryScreen no Inspector.", this);
            enabled = false;
            return;
        }

        initializationRoutine = StartCoroutine(InitializeWhenReady());
    }

    private IEnumerator InitializeWhenReady()
    {
        yield return null;

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        victoryPanel = root.Q<VisualElement>("victory-panel");

        if (victoryPanel == null)
        {
            Debug.LogError("Não encontro o elemento 'victory-panel' no UXML.", this);
            initializationRoutine = null;
            yield break;
        }

        victoryPanel.style.display = DisplayStyle.None;
        door.OnDoorEntered += ShowVictory;
        isSubscribed = true;
        initializationRoutine = null;
    }

    private void OnDisable()
    {
        if (initializationRoutine != null)
        {
            StopCoroutine(initializationRoutine);
            initializationRoutine = null;
        }

        if (isSubscribed && door != null)
        {
            door.OnDoorEntered -= ShowVictory;
            isSubscribed = false;
        }
    }

    private void ShowVictory()
    {
        if (victoryPanel != null)
        {
            victoryPanel.style.display = DisplayStyle.Flex;
        }
    }
}