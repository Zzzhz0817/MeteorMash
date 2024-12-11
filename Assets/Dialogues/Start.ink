// Start Dialogue: Waking Up Floating in Space

I snap awake to the vast emptiness of space. 

My suit hisses—a soft, steady leak. The HUD flashes: Oxygen falling. Suit breach detected. My head spins as I twist around. 
->main

=== main ===
“What… what happened?”
    * [Look around.] -> look_around
    

=== look_around ===
// add Beginner's Tutorial for moving the mouse to change their view
[Tutorial: Move the mouse around to change your view.]

Looking around, behind me, an alien warship looms, with a gaping hole where the airlock used to be. Then it hits me — Grady was cutting through the hatch. He thought it was safe, an airlock we could use. But as soon as we breached it...

“BOOM. God, the explosion… I blacked out.”

The thought sends a chill down my spine. My crewmates - Grady, Singh, Juno - what happened to them? Are they alive? I fumble for the comms.
    * [Try the comms.] -> try_comms

=== try_comms ===
“Grady? Singh? Juno? Can anyone hear me?”  

Nothing but static. I curse under my breath, my eyes locking onto our salvage shuttle. It hangs far away, impossibly distant. The life trackers are on the ship. I need to get there to check on them, using the precious oxygen to boost.

// instruction on the 3D map and the task on the screen
[The 3D map in the right of your HUD is a map of your surroundings. The gray diamonds are asteroids, and the small shuttle model indicates the position of your shuttle.]

[The blue sphere is a symbol of yourself. The blue arrow marks the direction you are facing, and the red arrow marks the direction where your head is pointing.]

[The oxygen bar is on the top of your HUD. You are consuming the oxygen slowly, and orienting using air thrusters takes extra oxygen. You need to get to the shuttle before running out of oxygen.]

My chest tightens as I look around. The shuttle glints in the distance. My HUD calculates the trajectory. Boosting directly? It’ll take more oxygen than I have.

“No tether. Not enough air. Great. Alright… think.”

I spot a small asteroid floating between me and the ship. If I can land on it and push off, I might get enough momentum without burning too much oxygen.

“Okay. One step at a time. Get to that rock first.”

With a shaky hand, I activate the suit’s air thrusters.

// add Beginner's Tutorial for orienting using air thrusters here (WASDQE, shift, space)
[Tutorial: Press 'WASD' to move around, and press 'Q' and 'E' to spin. You can also press 'Space' to going up and 'Ctrl' to going down. All of those actions uses oxygen.]

// add Beginner's Tutorial for graping the asteroid here
[Tutorial: When getting closer to an asteroid with low speed, hold Left Mouse Button to prepare for grounding. You must keep holding Left Mouse Button to remain grounded.]


-> DONE