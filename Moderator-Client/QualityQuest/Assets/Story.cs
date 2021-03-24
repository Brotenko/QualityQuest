using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story
{

    public void main()
    {
        StoryEvent root = new StoryEvent(0,"Charakter Auswahl",new HashSet<StoryEvent>(),StoryEventType.StoryFlow);

        StoryEvent decision1 = new StoryEvent(1, "Mit welchem Charakter möchtest du das Spiel spielen", new HashSet<StoryEvent>(),StoryEventType.StoryDecision);

        root.addChild(decision1);

        StoryEvent character1 = new StoryEvent(2, "Olaf", new HashSet<StoryEvent>(),StoryEventType.StoryDecisionOption);
        StoryEvent character2 = new StoryEvent(3, "Olaf", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent character3 = new StoryEvent(4, "Olaf", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent character4 = new StoryEvent(5, "Olaf", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);


        decision1.addChild(character1);
        decision1.addChild(character2);
        decision1.addChild(character3);
        decision1.addChild(character4);

        StoryEvent storyelement1 = new StoryEvent(6, "Dir fehlt noch ein Anwendungsfach am Ende deines Studiums", new HashSet<StoryEvent>(),StoryEventType.StoryFlow);

        character1.addChild(storyelement1);
        character2.addChild(storyelement1);
        character3.addChild(storyelement1);
        character4.addChild(storyelement1);

        StoryEvent decision2 = new StoryEvent(7, "Welches Anwednungsfach möchtest du auswählen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement1.addChild(decision2);

        StoryEvent decision2option1 = new StoryEvent(8, "Einführung in Bali", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision2option2 = new StoryEvent(9, "Anforderungsanalyse", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision2option3 = new StoryEvent(10, "Einführung in die Softwarearchitektur", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision2option4 = new StoryEvent(11, "Usability als Erfolgsfaktor", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision2.addChild(decision2option1);
        decision2.addChild(decision2option2);
        decision2.addChild(decision2option3);
        decision2.addChild(decision2option4);

        StoryEvent storyelemnt2option1 = new StoryEvent(12, "Du hast dein Anwendungsfach erfolgreich bestanden", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption, new Skills(0, 1, 0, 2));
        StoryEvent storyelemnt2option2 = new StoryEvent(13, "Du hast dein Anwendungsfach erfolgreich bestanden", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption, new Skills(0, 2, 0, 1));
        StoryEvent storyelemnt2option3 = new StoryEvent(14, "Du hast dein Anwendungsfach erfolgreich bestanden", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption, new Skills(0, 3, 0, 0));
        StoryEvent storyelemnt2option4 = new StoryEvent(15, "Du hast dein Anwendungsfach erfolgreich bestanden", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption, new Skills(2, 1, 0, 0));

        decision2option1.addChild(storyelemnt2option1);
        decision2option2.addChild(storyelemnt2option2);
        decision2option3.addChild(storyelemnt2option3);
        decision2option4.addChild(storyelemnt2option4);

        StoryEvent storyelement3 = new StoryEvent(16, "Nach dem du dein Anwendungsfach bestanden hast kannst du deine Freizeit genießen oder an einer außerschulischen Aktivität teilnehemn.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        decision2option1.addChild(storyelement3);
        decision2option2.addChild(storyelement3);
        decision2option3.addChild(storyelement3);
        decision2option4.addChild(storyelement3);

        StoryEvent decision3 = new StoryEvent(17, "Was willst du tun?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement3.addChild(decision3);

        StoryEvent decision3option1 = new StoryEvent(18, "Du nimmst an einem Hackathon an der Universität teil.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision3option2 = new StoryEvent(19, "Du nutzt deine Zeit um eine dir nicht bekannte Programmiersprache zu lernen", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision3option3 = new StoryEvent(20, "Du genießt deine Freizeit und machst Party", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision3.addChild(decision3option1);
        decision3.addChild(decision3option2);
        decision3.addChild(decision3option3);

        StoryEvent storyelement4option1 = new StoryEvent(21, "Du lernst viele neue Leute kennen und erweiterst deine Programmierkenntnisse.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(1, 0, 0, 2));
        StoryEvent storyelement4option2 = new StoryEvent(22, "Du lernst die Programmiersprache CSharp kennen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 1, 0, 2));
        StoryEvent storyelement4option3 = new StoryEvent(23, "Du genießt das Leben und machst Party, aber dafür rosten deine Programmierkenntnisse etwas ein.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 4, -1));

        decision3option1.addChild(storyelement4option1);
        decision3option2.addChild(storyelement4option2);
        decision3option3.addChild(storyelement4option3);

        StoryEvent storyelement5 = new StoryEvent(24, "Du hast dein Studium erfolgreich abgeschlossen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement4option1.addChild(storyelement5);
        storyelement4option2.addChild(storyelement5);
        storyelement4option3.addChild(storyelement5);

        StoryEvent storyelement6 = new StoryEvent(25, "...Ein halbes Jahr vergeht", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement5.addChild(storyelement6);

        StoryEvent storyelement7 = new StoryEvent(26, "Du hast einen Job bei NewTek GmbH angenommen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement6.addChild(storyelement7);
        
        StoryEvent storyelement8 = new StoryEvent(27, "Dein Chef hat einen neuen Auftrag von WizzBook an Land gezogen", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement7.addChild(storyelement8);

        StoryEvent storyelement9 = new StoryEvent(28, "Deine Aufgabe ist es das User Interface zu designen und zu implementieren.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
       
        storyelement8.addChild(storyelement9);

        StoryEvent decision4 = new StoryEvent(29, "Wie willst du mit deiner Aufgabe anfangen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement9.addChild(decision4);

        StoryEvent decision4option1 = new StoryEvent(30, "Du frägst deinen Mentor.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision4option2 = new StoryEvent(31, "Du suchst nach einer Lösung auf HeapOverflow.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision4option3 = new StoryEvent(32, "Du fängst einfach mit der Arbeit an.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision4.addChild(decision4option1);
        decision4.addChild(decision4option2);
        decision4.addChild(decision4option3);

        StoryEvent storyelement10option1 = new StoryEvent(33, "Du lernst deinen Mentor Yaggaya kennen. Yaggaya hilft dir bei deiner Aufgabe.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow,new Skills(1,0,0,1));
        StoryEvent storyelement10option2 = new StoryEvent(34, "Du löst das problem, verschwendest aber eine Menge Zeit.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow,new Skills(0,0,0,1));
        StoryEvent storyelement10option3 = new StoryEvent(35, "Du verschwendest eine Menge Zeit ohne das Problem zu lösen. Du benötigst für deine Aufgabe einen extra Tag.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0,-1,0,0));

        decision4option1.addChild(storyelement10option1);
        decision4option2.addChild(storyelement10option2);
        decision4option3.addChild(storyelement10option3);

        StoryEvent storyelement11 = new StoryEvent(36,"Nach deiner ersten Aufgabe musst du nun die dazugehörigen Tests implementieren.",new HashSet<StoryEvent>(),StoryEventType.StoryFlow);

        storyelement10option1.addChild(storyelement11);
        storyelement10option2.addChild(storyelement11);
        storyelement10option3.addChild(storyelement11);

        StoryEvent storyelement12 = new StoryEvent(37, "Du hast nur wenige Tage Zeit die Tests zu implementieren, da die Deadline sicher immer weiter nähert.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement11.addChild(storyelement12);

        StoryEvent storydecision5 = new StoryEvent(38, "Wie willst du vorgehen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement12.addChild(storydecision5);

        StoryEvent decision5option1 = new StoryEvent(39, "", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision5option2 = new StoryEvent(40, "", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);


        storydecision5.addChild(decision5option1);
        storydecision5.addChild(decision5option2);
                     
    }

}
