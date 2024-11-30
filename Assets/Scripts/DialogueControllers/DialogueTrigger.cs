using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] protected TextAsset inkJSON;
    protected bool playerInRange = false;
    protected bool triggered = false;
    protected bool guiding = false;


    protected virtual void Update()
    {
        if (!triggered && playerInRange)
        {
            if (!DialogueManager.GetInstance().guidanceIsPlaying && !DialogueManager.GetInstance().dialogueIsPlaying){
                DialogueManager.GetInstance().EnterGuidanceMode("Press F to interact");
                guiding = true;
            }
        }
        else if (guiding && DialogueManager.GetInstance().guidanceIsPlaying){
            DialogueManager.GetInstance().ExitGuidanceMode();
            guiding = false;
        }

        if (guiding && DialogueManager.GetInstance().guidanceIsPlaying && !DialogueManager.GetInstance().dialogueIsPlaying && Input.GetKeyDown(KeyCode.F))
        {
            DialogueManager.GetInstance().ExitGuidanceMode();
            guiding = false;
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            triggered = true;
        }
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    protected virtual void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
