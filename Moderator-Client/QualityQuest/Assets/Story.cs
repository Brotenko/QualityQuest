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

        StoryEvent storyelemnt2option1 = new StoryEvent(12, "Du hast dein Anwendungsfach erfolgreich bestanden", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 1, 0, 2));
        StoryEvent storyelemnt2option2 = new StoryEvent(13, "Du hast dein Anwendungsfach erfolgreich bestanden", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 2, 0, 1));
        StoryEvent storyelemnt2option3 = new StoryEvent(14, "Du hast dein Anwendungsfach erfolgreich bestanden", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 3, 0, 0));
        StoryEvent storyelemnt2option4 = new StoryEvent(15, "Du hast dein Anwendungsfach erfolgreich bestanden", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(2, 1, 0, 0));

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

        StoryEvent decision5 = new StoryEvent(38, "Wie willst du vorgehen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement12.addChild(decision5);

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        StoryEvent decision5option1 = new StoryEvent(39, "Du schreibst umfangreichge Tests bis sie fertig sind ohne auf die Deadline zu schauen", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision5option2 = new StoryEvent(40, "Du schreibst Tests bis die Deadline erreicht ist, was du bis dahin nicht schaffst bleibt ungetestet.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        
        decision5.addChild(decision5option1);
        decision5.addChild(decision5option2);

        StoryEvent storyelement11option1 = new StoryEvent(41, "Durch deine guten Programmierkenntnisse kannst du trotzdem die Deadline einhalten.", new HashSet<StoryEvent>(),StoryEventType.StoryFlow);
        StoryEvent storyelement11option2 = new StoryEvent(42, "Leider wirst du nicht rechtzeitig fertig aber du hast sehr umfangreiche Tests geschrieben.", new HashSet<StoryEvent>(),StoryEventType.StoryFlow);

        decision5option1.addChild(storyelement11option1);
        decision5option1.addChild(storyelement11option2);

        StoryEvent storyelement11option3 = new StoryEvent(43, "Durch deine guten Programmierkenntnisse hast du es trotz der kurzen Zeit geschafft umfangreiche Tests zu schreiben", new HashSet<StoryEvent>(),StoryEventType.StoryFlow);
        StoryEvent storyelement11option4 = new StoryEvent(44, "Die Deadline wurde erreciht aber du hast es nicht geschafft alles umfangreich zu testen.", new HashSet<StoryEvent>(),StoryEventType.StoryFlow);

        decision5option2.addChild(storyelement11option3);
        decision5option2.addChild(storyelement11option4);

        StoryEvent storyelement12option1 = new StoryEvent(45, "Yaggaya ist sehr zufrieden mit deiner herangehensweise und deinen Tests.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 3, 0, 1));
        StoryEvent storyelement12option2 = new StoryEvent(46, "Yaggaya ist sehr zufrieden mit deiner herangehensweise und deinen Tests, obwohl du nicht rechtzeitig fertig geworden bist.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 2, 0, 2));

        storyelement11option1.addChild(storyelement12option1);
        storyelement11option2.addChild(storyelement12option2);

        StoryEvent storyelement12option3 = new StoryEvent(47, "Yaggaya ist sehr zufrieden mit deiner Leistung.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 0, 2));
        StoryEvent storyelement12option4 = new StoryEvent(48, "Yaggaya ist nicht zufrieden mit deienr Leistung. Du musst die Tests nochmal überarbeiten und überschreitest die Deadline um ein paar Tage.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, -2, 0, 1));

        storyelement11option3.addChild(storyelement12option3);
        storyelement11option4.addChild(storyelement12option4);

        StoryEvent storyelement13 = new StoryEvent(49, "Das schreiben der Tests ist abgeschlossen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement12option1.addChild(storyelement13);
        storyelement12option2.addChild(storyelement13);
        storyelement12option3.addChild(storyelement13);
        storyelement12option4.addChild(storyelement13);

        // company party

        StoryEvent storyelement14 = new StoryEvent(50, "Es steht eine Firmenfeier an.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement13.addChild(storyelement14);

        StoryEvent storyelement15 = new StoryEvent(51, "Die Tests haben ein paar Fehler aufgedeckt, wenn du sie behebst kannst du nicht zu Firmenfeier.",new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement14.addChild(storyelement15);
        
        StoryEvent decision6 = new StoryEvent(52, "Gehst du zur Firmenparty?",new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement15.addChild(decision6);

        StoryEvent decision6option1 = new StoryEvent(53, "Du gehst zu Firmenfeier.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision6option2 = new StoryEvent(54, "Du arbeitest am Projekt weiter.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision6.addChild(decision6option1);
        decision6.addChild(decision6option2);

        StoryEvent storyelement16 = new StoryEvent(55, "Du arbeitest weiter am Projekt.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 0, 2));

        decision6option2.addChild(storyelement16);

        // at the company party

        StoryEvent storyelement18 = new StoryEvent(57, "Du bist auf der Firmenfeier", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        decision6option1.addChild(storyelement18);

        StoryEvent decision7 = new StoryEvent(58, "Was willst du auf der Firmenfeier machen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement18.addChild(decision7);

        StoryEvent decision7option1 = new StoryEvent(59, "Du plauderst mit Kollegen.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision7option2 = new StoryEvent(60, "Du redest mit deinem Mentor und deinem Chef.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision7option3 = new StoryEvent(61, "Du sitzt alleine.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision7.addChild(decision7option1);
        decision7.addChild(decision7option2);
        decision7.addChild(decision7option3);

        StoryEvent storyelement19option1 = new StoryEvent(62, "Du bekommst von Kollegen ein paar Tipps.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 0, 2));

        // how to influences later decisions ???
        StoryEvent storyelement19option2 = new StoryEvent(63, "Es wurden zusätzliche Dialogoptionen für spätere Entscheidungen freigeschaltet ", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        decision7option1.addChild(storyelement19option1);
        decision7option2.addChild(storyelement19option2);

        StoryEvent storyelement20 = new StoryEvent(64, "Du hast durst und holst dir ein Getränk.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement19option1.addChild(storyelement20);
        storyelement19option2.addChild(storyelement20);
        decision7option3.addChild(storyelement20);

        StoryEvent decision8 = new StoryEvent(65, "Was möchtest du trinken?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement20.addChild(decision8);

        StoryEvent decision8option1 = new StoryEvent(66, "Ein Bier reicht für den Abend.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision8option2 = new StoryEvent(67, "Eine Cola tuts auch.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision8option3 = new StoryEvent(68, "Ein paar Shots sind ok, es kostet ja nichts.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision8.addChild(decision8option1);
        decision8.addChild(decision8option2);
        decision8.addChild(decision8option3);

        StoryEvent storyelement21option1 = new StoryEvent(69, "Du hast die Firmenfeier gut überstanden", new HashSet<StoryEvent>(), StoryEventType.StoryFlow,new Skills(0,0,1,0));

        decision8option1.addChild(storyelement21option1);
        decision8option2.addChild(storyelement21option1);

        StoryEvent storyelement21option2 = new StoryEvent(70, "Du trinkst zwei Shots.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        decision8option3.addChild(storyelement21option2);

        StoryEvent decision9 = new StoryEvent(71, "Willst du noch mehr Shots trinken?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement21option2.addChild(decision9);

        StoryEvent decision9option1 = new StoryEvent(72, "Mehr Shots trinken.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision9option2 = new StoryEvent(72, "Nein, zwei sind genug.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision9.addChild(decision9option1);
        decision9.addChild(decision9option2);

        StoryEvent storyelement22option1 = new StoryEvent(70, "Du sorgst für gute Stimmung.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow,new Skills(0,0,2,0));


        // affected by dice roll
        StoryEvent storyelement22option2 = new StoryEvent(70, "Du überstehst die Firmenfeier ohne Probleme.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow,new Skills(0,0,3,0));
        StoryEvent storyelement22option3 = new StoryEvent(70, "Du sorgst für gute Stimmung aber fällst negativ auf.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow,new Skills(-2,0,3,0));

        decision9option2.addChild(storyelement22option1);
        decision9option2.addChild(storyelement22option2);
        decision9option2.addChild(storyelement22option3);

        StoryEvent storyelement23 = new StoryEvent(70, "Die Firmenfeier ist zu Ende und du gehts nach Hause.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement22option1.addChild(storyelement23);
        storyelement22option2.addChild(storyelement23);
        storyelement22option3.addChild(storyelement23);

        // training course

        StoryEvent storyelement17 = new StoryEvent(56, "Eine Weiterbildung steht an.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement16.addChild(storyelement17);
        storyelement23.addChild(storyelement17);

        // Deadline should be mentined here as well
        StoryEvent decision10 = new StoryEvent(71, "Willst du an der Weiterbildung teilnehmen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement17.addChild(decision10);

        StoryEvent decision10option1 = new StoryEvent(72, "Klar warum nicht.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision10option2 = new StoryEvent(73, "Ich kenne das Thema schon von der Uni und arbeite an meinem Projekt weiter.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision10option3 = new StoryEvent(74, "Nein, ich bracuhe erstmal Urlaub", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        // only if dialog option was ulocked previously
        StoryEvent decision10option4 = new StoryEvent(75, "Dein Chef bietet dir an nach der Weiterbildung Urlaub zu machen", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision10.addChild(decision10option1);
        decision10.addChild(decision10option2);
        decision10.addChild(decision10option3);
        decision10.addChild(decision10option4);

        StoryEvent storyelement24option1 = new StoryEvent(75, "Du kannst deine Deadline nicht einhalten aber deinem Chef gefällt dein Engagement deine Fähigkeiten zu erweitern", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0,0,0,1));
        StoryEvent storyelement24option2 = new StoryEvent(76, "Du arbeitest an dem Projekt weiter und schaffst es deine Deadline einzuhalten.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 0, 0));
        StoryEvent storyelement24option3 = new StoryEvent(77, "Da du Urlaub genommen hast schaffst du es nicht die Deadline inzuhalten", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-2, -2, 0, 0));
        StoryEvent storyelement24option4 = new StoryEvent(78, "Du genießt deinen Urlaub und bekommst keine Probleme mit der Deadline weil dein Chef sich um eine Vertretung gekümmert hat.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 2, 0, 2));

        decision10option1.addChild(storyelement24option1);
        decision10option2.addChild(storyelement24option2);
        decision10option3.addChild(storyelement24option3);
        decision10option4.addChild(storyelement24option4);

        StoryEvent storyelement25 = new StoryEvent(79, "WizzBook hat sich bei dir gemeldet und möchte Änderungen an WizzApp.",new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement24option1.addChild(storyelement25);
        storyelement24option2.addChild(storyelement25);
        storyelement24option3.addChild(storyelement25);
        storyelement24option4.addChild(storyelement25);

    }

}
