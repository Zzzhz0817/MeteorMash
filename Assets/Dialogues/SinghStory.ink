// Singh Dialogue: Finding Singh’s Body
INCLUDE Globals.ink
~ id_card_acquired = true

The marker leads me to a large asteroid, its surface pockmarked and uneven. I land gently, scanning the area. Singh’s body lies not far away, half-buried in a shallow crater, her suit dark against the pale gray rock.
    * [Look for the captain's ID card.] -> get_id_card
    
=== get_id_card ===
I kneel beside her, brushing away the loose dust from her belt. The captain’s ID card is still clipped securely, a small, unassuming piece of plastic that now feels like the heaviest thing in the galaxy. I pause, gripping it tightly, memories surfacing unbidden.

She was always the steady one, the leader, even when it was just the two of us. Back then, we had nothing — barely enough to keep a ship running, scraping by on tiny hauls that hardly covered fuel. But she never wavered. Never stopped believing we could do more. 

“We’ll make it,” she told me once, sitting on the deck of our first rust-bucket ship. “Maybe not today, maybe not tomorrow, but we will. You’ll see.”

She was right, of course. She always was. When others doubted us, Singh built this team from the ground up. She brought everyone together, convincing us with her vision, her drive. 

“You’ve got ambition,” I told her once, half-laughing as we patched up another hole in the ship’s hull. “Bigger than the stars, Singh.”

She grinned at me, that sharp, confident grin I’d come to rely on. “Someone has to dream big. You’re just here to keep me grounded.” 

And I did, for years. But now… here she is. The one who gave me everything, lying cold and still on this lifeless rock.

“You shouldn’t be here. You were supposed to go farther than all of us.”  

I unclip the ID card and secure it to my suit. I glance at her tank. There’s air left — just enough to help.
    * [Take Singh's air supply.] -> take_oxygen
    * [Leave it as it is.] -> leave

=== take_oxygen ===
Kneeling beside her, I connect the transfer hose to her tank. The hiss of air fills my system, keeping me alive for just a little longer.

I grit my teeth, my vision blurring as I whisper, “Even now, you’re carrying me, Singh. Just like you always did. I’ll make sure it wasn’t for nothing.”

The transfer completes, and I disconnect the line. Singh’s ID card rests in my pocket, heavy with her dreams.  
-> DONE

=== leave ===
I know there’s air there, enough to keep me going. My hand hovers over the transfer hose, trembling.

But Singh wouldn’t have hesitated. She wouldn’t have let me waste time doubting myself. She built this team on faith — faith in us, faith in me.

“You always believed in me, Singh.” I pull my hand back and secure the ID card to my suit. “And I’m going to honor that.”
-> DONE