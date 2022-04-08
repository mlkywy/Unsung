#voice:NPC2_voice
INCLUDE globals.ink

{ spoke_to_bandit == false: -> main | -> spoken_with_npc }

-> main

=== main ===
How can I help you, hero?
You want to use my boat?
Hm... I'm not sure I can trust ya'.

-> END

=== spoken_with_npc ===
Ah, I see you've met my friend... He's a decent fella, I promise!
Since you're both so close, I suppose I can take you on my boat...
Well, let's go!
~ ferryman_ride = true
-> END