using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]

    // [Header("Globals Ink File")]
    // [SerializeField] private InkFile globalsInkFile;

     // variable for the load_globals.ink JSON
    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

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

        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
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
        if (dialogueIsPlaying && (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)))
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

            if (dialogueText.text.Contains("Slowly, carefully, I attach the transfer hose between us") ||
                dialogueText.text.Contains("I curse under my breath, my hands trembling as I hook up the transfer system") ||
                dialogueText.text.Contains("Kneeling beside her, I connect the transfer hose to her tank") ||
                dialogueText.text.Contains("The door to the control room looms ahead, its surface warped and jammed tight") ||
                dialogueText.text.Contains("The control panel flickers to life as I slide Singh") ||
                dialogueText.text.Contains("Air rushes past me as I step through the broken doorway"))
            {
                Time.timeScale = 0f;
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

            // Take oxygen from Grady, Juno and Singh
            if (dialogueText.text.Contains("Slowly, carefully, I attach the transfer hose between us") ||
                dialogueText.text.Contains("I curse under my breath, my hands trembling as I hook up the transfer system") ||
                dialogueText.text.Contains("Kneeling beside her, I connect the transfer hose to her tank"))
            {
                Time.timeScale = 1f;
                player.PlaySound("OxygenRefill");
                player.AddOxygen(50f);
            }

            // Using Laser
            if (dialogueText.text.Contains("The door to the control room looms ahead, its surface warped and jammed tight"))
            {
                Time.timeScale = 1f;
                player.PlaySound("LaserCutter");
            }

            // Using ID card
            if (dialogueText.text.Contains("The control panel flickers to life as I slide Singh"))
            {
                Time.timeScale = 1f;
                player.PlaySound("IDCard");
            }

            // Using Sealant
            if (dialogueText.text.Contains("Air rushes past me as I step through the broken doorway"))
            {
                Time.timeScale = 1f;
                player.PlaySound("SealantSpray");
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
