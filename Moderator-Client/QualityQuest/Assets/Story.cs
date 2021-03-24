using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story
{

    public void main()
    {
        StoryEvent root = new StoryEvent(0,"Charakter Auswahl",new HashSet<StoryEvent>());
        StoryEvent decision1 = new StoryEvent(1, "Mit welchem Charakter möchtest du das SPiel spielen", new HashSet<StoryEvent>());
        StoryEvent character1 = new StoryEvent(2, "Olaf", new HashSet<StoryEvent>());
        StoryEvent character2 = new StoryEvent(3, "Olaf", new HashSet<StoryEvent>());
        StoryEvent character3 = new StoryEvent(4, "Olaf", new HashSet<StoryEvent>());
        StoryEvent character4 = new StoryEvent(5, "Olaf", new HashSet<StoryEvent>());
        StoryEvent storyelement1 = new StoryEvent(6, "Dir fehlt noch ein Anwendungsfach am Ennde deines Studiums", new HashSet<StoryEvent>());

        root.addChild(decision1);
        decision1.addChild(character1);
        decision1.addChild(character2);
        decision1.addChild(character3);
        decision1.addChild(character4);


    }

}
