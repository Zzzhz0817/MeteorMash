using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerPlayer : DialogueTrigger
{
    protected void Start()
    {
        playerInRange = true;
    }


    protected override void Update()
    {
        if (!triggered){
            DialogueManager.GetInstance().EnterGuidanceMode("Press F to interact");
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            DialogueManager.GetInstance().ContinueStory();
            triggered = true;
            guiding = true;
        }

        if (guiding && Input.GetKeyDown(KeyCode.F))
        {
            DialogueManager.GetInstance().ExitGuidanceMode();
            guiding = false;
        }
    }
}
