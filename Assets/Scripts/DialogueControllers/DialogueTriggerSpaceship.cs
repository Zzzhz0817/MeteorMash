using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerSpaceship : DialogueTrigger
{
    [Header("Ink JSON 2")]
    [SerializeField] private TextAsset inkJSON2;

    protected override void Update()
    {
        if (playerInRange)
        {
            if (!DialogueManager.GetInstance().guidanceIsPlaying && !DialogueManager.GetInstance().dialogueIsPlaying)
            {
                DialogueManager.GetInstance().EnterGuidanceMode("Press F to interact");
                guiding = true;
            }
        }
        else if (guiding && DialogueManager.GetInstance().guidanceIsPlaying)
        {
            DialogueManager.GetInstance().ExitGuidanceMode();
            guiding = false;
        }

        if (guiding && DialogueManager.GetInstance().guidanceIsPlaying && !DialogueManager.GetInstance().dialogueIsPlaying && Input.GetKeyDown(KeyCode.F))
        {
            DialogueManager.GetInstance().ExitGuidanceMode();
            guiding = false;
            if (!triggered)
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                triggered = true;
            }
            else
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON2);
            }
        }
    }
}
