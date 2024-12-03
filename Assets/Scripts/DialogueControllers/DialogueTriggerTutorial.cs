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
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                DialogueManager.GetInstance().ContinueStory();
                triggered = true;
            }
        }
    }
}
