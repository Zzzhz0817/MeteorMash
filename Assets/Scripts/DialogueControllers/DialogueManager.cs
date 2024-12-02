using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using Ink.UnityIntegration;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]

    [Header("Globals Ink File")]
    [SerializeField] private InkFile globalsInkFile;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject guidancePanel;
    [SerializeField] private TextMeshProUGUI guidanceText;
    [SerializeField] private P_StateManager player;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    public bool guidanceIsPlaying { get; private set; }

    private static DialogueManager instance;

    private DialogueVariables dialogueVariables;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of DialogueManager found!");
        }
        instance = this;

        dialogueVariables = new DialogueVariables(globalsInkFile.filePath);
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        guidanceIsPlaying = false;
        dialoguePanel.SetActive(false);
        guidancePanel.SetActive(false);

        // get all choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (dialogueIsPlaying && Input.GetKeyDown(KeyCode.F))
        {
            ContinueStory();
        }
    }

    public void EnterGuidanceMode(string guidance)
    {
        Debug.Log(guidancePanel);
        guidancePanel.SetActive(true);
        guidanceIsPlaying = true;
        guidanceText.text = guidance;
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialoguePanel.SetActive(true);
        dialogueIsPlaying = true;
        player.SwitchState(player.dialogueState);

        dialogueVariables.StartListening(currentStory);

        // ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialogueVariables.StopListening(currentStory);

        dialoguePanel.SetActive(false);
        dialogueIsPlaying = false;
        dialogueText.text = "";
        player.SwitchToPreviousState();
    }

    public void ExitGuidanceMode()
    {
        Debug.Log("Exiting guidance mode");
        guidancePanel.SetActive(false);
        guidanceIsPlaying = false;
        guidanceText.text = "";
    }

    public void ContinueStory()
    {
        // check if current dialogue has choices
        // if it does, do not continue by pressing F
        List<Choice> currentChoices = currentStory.currentChoices;
        if (currentChoices.Count > 0)
        {
            return;
        }
        
        if (currentStory.canContinue)
        {
            // change player state to dialogue state after tutorial
            if (dialogueText.text.Contains("Tutorial"))
            {
                player.SwitchState(player.dialogueState);
            }

            // set text for current dialogue line
            dialogueText.text = currentStory.Continue();
            // display choices if there are any
            DisplayChoices();

            // change player state to previous state during tutorial
            if (dialogueText.text.Contains("Tutorial"))
            {
                Debug.Log("Tutorial dialogue");
                if (player.previousState != null)
                {
                    Debug.Log("Switching to previous state");
                    player.SwitchToPreviousState();
                }
                else
                {
                    Debug.Log("Switching to flying state");
                    player.SwitchState(player.flyingState);
                }
            }
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure we have enough choices UI
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("Not enough choices UI to display " 
                + currentChoices.Count + " choices!");
            return;
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // inactivate the rest of the choices UI
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    public Ink.Runtime.Object GetVariable(string variableName)
    {
        Ink.Runtime.Object variableValue;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogError("Variable " + variableName + " not found!");
        }
        return variableValue;
    }
}
