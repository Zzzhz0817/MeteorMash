INCLUDE Globals.ink
// First Entry Dialogue: Reaching the Spaceship  

I slam into the ship’s outer airlock, gripping the handle like a lifeline. The hatch cycles open, barely. I fling myself inside. 

“Made it… barely.”
    * [Check others' signals on the console.] -> check_signals

=== check_signals ===
I stagger to the console and pull up the life tracker display. My hands tremble as the markers for Grady, Singh, and Juno appear—each surrounded by a faint red glow. 

“No. No… this can’t be right.”  

I tap through the data. The readings confirm it: no vital signs, no chance of recovery. My stomach sinks, and my throat tightens. 

I slam my fist against the console, taking a moment to steady my breathing. The wreckage of the alien warship still hangs in the distance, a cold reminder of what went wrong. 

“Alright. No time to mourn. They wouldn’t want me to die here, too.”
    * [Check the system diagnostics on the console.]
-> check_system

=== check_system ===
The system diagnostics tell me what I need to know. The airlock is damaged and has a limited oxygen supply. The ship will not fly without authorization.

To pilot the ship, I’ll need three things: a Hull laser to cut into the jammed control room door, the Captain’s ID card to start the system, and a Sealant to patch the open door.
~task_received = true

Each item is logged to a different crew member. Grady had the laser. Juno carried the sealant. Singh had the ID card. Their markers are… scattered. 

The display zooms out, showing the locations of their bodies—each drifting on nearby asteroids, scattered from the blast. I swallow hard, my voice quiet.  

“Alright. One thing at a time. Get the tools, seal the door, get out of here. You can do this.”  

I refill my oxygen with what's left of the airlock's emergency supply. The hiss of the suit breaks the silence as I step back into space. The HUD highlights the markers.
-> DONE