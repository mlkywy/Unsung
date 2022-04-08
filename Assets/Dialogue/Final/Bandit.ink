INCLUDE globals.ink

{ bandits_answer == "": -> main | -> already_chosen }

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
~ bandits_answer = answer
Interesting...
-> END

=== already_chosen ===
You think they're {bandits_answer}, huh? You and me both.
-> END