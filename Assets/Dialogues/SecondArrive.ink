INCLUDE Globals.ink

// Returning to the Spaceship
VAR all_acquired = false
~ all_acquired = laser_acquired && sealant_acquired && id_card_acquired

{all_acquired:
    -> all_collected
  - else:
    -> not_all_collected
}

=== all_collected ===
The ship looms ahead, its docking bay doors a welcome sight in the endless void. My oxygen gauge blinks angrily, but the last burst from my thrusters is just enough to push me through the airlock. 

The doors seal behind me with a hiss, and I collapse to my knees, ripping off my helmet as recycled air floods my lungs.

I glance at the items strapped to my suit: the Hull laser, the sealant, and Singh’s ID card. Each one feels heavier than it should, their weight not from their mass, but from what they represent.

“I got them all,” I whisper, my voice rough. “Now let’s finish this.”
-> check_laser

=== not_all_collected ===
The ship’s airlock cycles shut behind me with a heavy hiss, and I collapse against the wall, gulping down the recycled air. My HUD stops flashing oxygen warnings, but the weight in my chest doesn’t lift. 

“I’m not done yet,” I mutter, pulling myself upright. The faint hum of the ship’s life support feels deafening in the silence. “Not by a long shot.”
-> check_laser

=== check_laser ===
{laser_acquired && !laser_used: ->use_laser | -> check_id}

=== check_id ===
{laser_used && id_card_acquired && !id_card_used: ->use_id |-> check_sealant}

=== check_sealant ===
{all_acquired: -> use_sealant|->ready_to_leave}

// Using the Hull Laser  
=== use_laser ===
~ laser_used = true
The door to the control room looms ahead, its surface warped and jammed tight. Grady’s Hull laser sits in my hand, its grip still marked by his fingerprints. My stomach twists, but I steady myself. 

I fire the laser, its beam cutting through the thick metal with precision. Sparks fly, lighting up the dim corridor as the door edges give way.  

“Still running ahead, huh, Grady?” I murmur, watching the beam carve through the obstruction. “Guess I’ll finish the job for you.”  

The door groans, finally giving way. I shove it aside, wiping the sweat from my brow. One step closer.
-> check_id

// Using Singh’s ID Card  
=== use_id ===
~id_card_used = true
The control panel flickers to life as I slide Singh’s ID card through the reader. The familiar hum of the ship’s systems stirs to life, and for a moment, it’s like she’s standing beside me again, issuing orders with that steady, commanding voice.

I grip the card tightly, my voice barely a whisper. “You gave us this, Singh. Gave me this. I won’t waste it.” 

The console beeps, confirming access. The ship’s systems open up before me, but there’s still work to do.
-> check_sealant

// Using the Sealant
=== use_sealant ===
~sealant_used = true
Air rushes past me as I step through the broken doorway. The pressure gauge on my suit flashes, but I don’t panic. Juno’s sealant is in my hand before I even think, the thick compound spreading over the breach like a second skin.

The leak stops with a satisfying hiss, and my suit confirms a stable atmosphere. For a second, I can almost hear Juno’s casual drawl, like she’s looking over my shoulder.

“Easy fix, right, Juno?” I say, my voice barely audible. “You always made it look easy.”

The room stabilizes, and I nod, pocketing the empty tube. Another step closer.
-> ending


// Preparing to Leave Again  
=== ready_to_leave ===
I stand at the airlock, staring out into the void. The HUD marks the final target, floating out there among the rocks. My chest tightens, but I straighten up, adjusting the straps on my suit.

“Grady, Juno, Singh—I’ll finish this. I’ll carry it all back.”  

The ship’s airlock opens, and the cold silence of space floods my senses. I take a steadying breath, gripping the tether line.

“Just wait for me.”
-> END

// Game ending leave
=== ending ===
I stand in the control room, staring at the navigation console as it boots up. The void outside feels heavier now, the silence pressing against the ship’s walls. My reflection stares back from the glass, the faint glow of the HUD the only light in the room.

“Grady, Juno, Singh,” I say, my voice steady now. “You brought me this far. I’ll bring us the rest of the way.”

I set the course, the ship’s engines humming faintly as they power up. There’s a long road ahead, but the weight of their hopes and dreams will carry me forward.

“We’ll finish it. Together.”
-> END