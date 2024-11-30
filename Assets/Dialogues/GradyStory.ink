// Grady Dialogue: Finding Grady’s Body
INCLUDE Globals.ink
~ laser_acquired = true

I drift toward the marker, the asteroid growing larger as the HUD guides me in. My thrusters fire one last burst, and I land softly, anchoring myself on the rough surface.

Grady’s body lies just ahead, sprawled across a jagged rock. His suit is scorched, the visor cracked, but his tool belt is intact. The Hull laser is clipped there, still pristine.
    * [Get the Hull laser.] -> get_laser

=== get_laser ===
I kneel beside him, carefully unclipping the Hull laser from his tool belt. It’s still intact, untouched by the blast. My fingers hover for a moment as memories flood back.

He’d come up to us in a bar. We were halfway through celebrating a decent haul when this wiry kid, barely old enough to be out here, strode right up to our table. His eyes lit up when he saw our suits.

“You’re salvagers, right? I can tell—those suits are top-grade. Look, I know what I’m doing, and I’ll work hard. Just give me a shot! You won’t regret it!”

Grady practically dared us to say no. Juno laughed so hard she almost spilled her drink, but Singh leaned back and said, ‘Alright, kid. One run. Prove you’ve got what it takes.’

And he did. Over and over. Brave, reckless, always first to dive into danger. Always chasing that big fortune he thought was just one haul away.

“I’ll make us rich,” he said once, after a long day out in the field. “Not just comfortable. Rich. You’ll see.”

But now, here he is. Brave, energetic Grady, his dreams of fortune ending here on a cold rock in the middle of nowhere.

“Damn it, kid. You always thought you were invincible. That we were.”  

I grip the Hull laser tightly, securing it to my suit. I glance at Grady’s tank. The sign shows there is still some air left.
    * [Take Grady's air supply.] -> take_oxygen
    * [Leave it as it is.] -> leave

=== take_oxygen ===
I hate what I’m about to do, but there’s no other choice. Slowly, carefully, I attach the transfer hose between us and siphon what little air remains in his system.

“I’m sorry, kid. But I’ll make it count. I’ll carry your dream back with me.”
-> DONE

=== leave ===
 My hand hovers over the transfer hose, but I pull it back. Grady wouldn’t have wanted this. He always ran in headfirst, not holding back for anything. That spirit shouldn’t end like this.

I shake my head, the tightness in my chest not just from the lack of air. “No, Grady. You ran ahead, like you always do. Let me follow your lead this time.”

->DONE