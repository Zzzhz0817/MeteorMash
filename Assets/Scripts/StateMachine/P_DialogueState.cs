using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class P_DialogueState : P_State
{
    public override void EnterState(P_StateManager player)
    {
        player.oxygenBoostingAudioSource.enabled = false;
        player.crawlingAudioSource.enabled = false;
        player.grabbingAudioSource.enabled = false;

        Cursor.visible = true;
        // Pause the game
        Time.timeScale = 0f;

        // Disable all movement
        player.rb.velocity = Vector3.zero;
    }

    public override void UpdateState(P_StateManager player)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            player.SwitchState(player.previousState);
        }
    }

    public override void ExitState(P_StateManager player)
    {
        // Resume the game
        Time.timeScale = 1f;

        player.oxygenBoostingAudioSource.enabled = true;
        player.crawlingAudioSource.enabled = false;
        player.grabbingAudioSource.enabled = false;

        Cursor.visible = false;
    }
}
