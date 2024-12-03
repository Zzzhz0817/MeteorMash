using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerTutorial : DialogueTrigger
{


    protected override void Update()
    {
        if (!triggered && playerInRange)
        {
            if (!DialogueManager.GetInstance().guidanceIsPlaying && !DialogueManager.GetInstance().dialogueIsPlaying){
                if(playerInRange = true)
                {
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                    triggered = true;
                }
            }
        }
    }
}
