#voice:NPC1_voice
INCLUDE globals.ink

{ bandit_answer == "": -> main | -> already_chosen }

-> main

=== main ===
What do you think about those bandits? L-Listen, I ain't associated with them.
    + [They're scary.]
        -> chosen("scary")
    + [Pretty annoying.]
        -> chosen("annoying")
    + [They must have their reasons.]
        -> chosen("alright")
        
=== chosen(answer) ===
~ bandit_answer = answer
~ spoke_to_bandit = true

Interesting...
-> END

=== already_chosen ===
You think they're {bandit_answer}, huh? You and me both.
-> END