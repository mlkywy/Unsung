
INCLUDE globals.ink

{ spoke_to_bandit == false: -> main | -> spoken_with_npc }

-> main

=== main ===
Halt, murderhobos. #speaker:Ferryman #portrait:ferryman_portrait #voice:ferryman_voice
That one. The one in the dreadful leather armor.
She harbors foul arcane arts within her very being...
I refuse to assist mages, much less those who harbor them. 
- The Queen's Parayen is meant to be a haven for us mages, is it not? Where's this attitude coming from!? #speaker:Tomyrietta #portrait:tomyrietta_portrait #voice:tomyrietta_voice
I'm not loyal to the Queen and her rhetoric. #speaker:Ferryman #portrait:ferryman_portrait #voice:ferryman_voice
Once I have the means, I will leave this country.
However I'll let you in for a price. 
- Name it. #speaker:Mona #portrait:mona_portrait #voice:mona_voice
A bandit defector was returning my property before an ambush was struck. #speaker:Ferryman #portrait:ferryman_portrait #voice:ferryman_voice
Find it and you'll find me in a more agreeable mood. 
-> END

=== spoken_with_npc ===
Ahhh... I see you have spoken with my friend. #speaker:Ferryman #portrait:ferryman_portrait #voice:ferryman_voice
I thank you very much for returning my belongings to me. 
Come, across the waters I will take you as promised...
~ player_teleport = true
-> END