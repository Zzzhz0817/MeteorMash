INCLUDE Globals.ink
// First Entry Dialogue: Reaching the Spaceship  

I slam into the ship’s outer airlock, gripping the handle like a lifeline. The hatch cycles open, and I stumble inside. The pressure stabilizes, and I rip off my helmet, gasping for air. 

“Made it… barely.”
    * [Check others' signals on the console.] -> check_signals

=== check_signals ===
I stagger to the console and pull up the life tracker display. My hands tremble as the markers for Grady, Singh, and Juno appear—each surrounded by a faint red glow. 

“No. No… this can’t be right.”  

I tap through the data. The readings confirm it: no vital signs, no chance of recovery. My stomach sinks, and my throat tightens. 

I slam my fist against the console, taking a moment to steady my breathing. The wreckage of the alien warship still hangs in the distance, a cold reminder of what went wrong. I glance at the control room door—it’s jammed shut.  

“Alright. No time to mourn. They wouldn’t want me to die here, too.”
    * [Check the system diagnostics on the console.]
-> check_system

=== check_system ===
The system diagnostics tell me what I need to know. To pilot the ship, I’ll need three things: a Hull laser to cut into the jammed control room door, a Sealant to patch it once it’s open, and the Captain’s ID card to start the system. Each item is logged to a different crew member.
~task_received = true

“Grady had the laser. Juno carried the sealant. Singh… she had the ID card. Their markers are… scattered. Damn it.”  

The display zooms out, showing the locations of their bodies—each drifting on nearby asteroids, scattered from the blast. I swallow hard, my voice quiet.  

“Alright. One thing at a time. Get the tools, seal the door, get out of here. You can do this.”  

I pull the helmet back on, the hiss of the suit breaks the silence as I step back into space. The HUD highlights the markers.
-> DONE