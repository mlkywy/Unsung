#speaker:Bandit? #portrait:bandit_portrait #voice:hakim_voice
INCLUDE globals.ink

{ bandit_answer == "": -> main | -> already_chosen }

-> main

=== main ===
Stop, don't hurt me!
L-Listen, coz, I ain't associated with those bandits anymore!
+ [Why do you look like them?]
- I'm just in disguise! Smart, right?
I've turned over a new leaf, honest!
I was hired to take back some stolen goods, but I think they're beginning to suspect me... 
So I'm hiding in here with everything 'til they're gone.
Wait... You already cleared 'em out? Wow, you must be some kind of hero!
'Specially with that armor!
What did you think of those bandits, coz? 
    + [They're scary.]
        -> chosen("scary")
    + [They're annoying.]
        -> chosen("annoying")
    + [They must have their reasons.]
        -> chosen("alright")
        
=== chosen(answer) ===
~ bandit_answer = answer
~ spoke_to_bandit = true

You think they're {bandit_answer}, huh? 
By the way, have you met the Ferryman to the west?
Tell 'im that I've got his things right here. 
In fact, you can deliver 'em for me, coz. I'm getting out of here.
-> END

=== already_chosen ===
You think they're {bandit_answer}, huh? 
By the way, have you met the Ferryman to the west?
Tell 'im that I've got his things right here. 
In fact, you can deliver 'em for me, coz. I'm getting out of here.
-> END