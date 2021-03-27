using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Story
{

    public void main()
    {
        StoryEvent root = new StoryEvent(Guid.NewGuid(),"Charakter Auswahl",new HashSet<StoryEvent>(),StoryEventType.StoryFlow);

        StoryEvent decision1 = new StoryEvent(Guid.NewGuid(), "Mit welchem Charakter möchtest du das Spiel spielen", new HashSet<StoryEvent>(),StoryEventType.StoryDecision);

        root.addChild(decision1);


        StoryEvent character1 = new StoryEvent(Guid.NewGuid(), "Noruso \n Programming: 1 \n Analytics: 4 \n Communication: 3 \n Partying: 2", new HashSet<StoryEvent>(),StoryEventType.StoryDecisionOption);
        StoryEvent character2 = new StoryEvent(Guid.NewGuid(), "Lumati \n Programming: 4 \n Analytics: 3 \n Communication: 1 \n Partying: 0", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent character3 = new StoryEvent(Guid.NewGuid(), "Turgal \n Programming: 2 \n Analytics: 2 \n Communication: 2 \n Partying: 2", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent character4 = new StoryEvent(Guid.NewGuid(), "Kirogh \n Programming: 1 \n Analytics: 0 \n Communication: 2 \n Partying: 5", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);


        decision1.addChild(character1);
        decision1.addChild(character2);
        decision1.addChild(character3);
        decision1.addChild(character4);

        StoryEvent storyelement1 = new StoryEvent(Guid.NewGuid(), "Dir fehlt noch ein Anwendungsfach am Ende deines Studiums", new HashSet<StoryEvent>(),StoryEventType.StoryFlow);

        character1.addChild(storyelement1);
        character2.addChild(storyelement1);
        character3.addChild(storyelement1);
        character4.addChild(storyelement1);

        StoryEvent decision2 = new StoryEvent(Guid.NewGuid(), "Welches Anwednungsfach möchtest du auswählen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement1.addChild(decision2);

        StoryEvent decision2option1 = new StoryEvent(Guid.NewGuid(), "Einführung in Bali", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision2option2 = new StoryEvent(Guid.NewGuid(), "Anforderungsanalyse", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision2option3 = new StoryEvent(Guid.NewGuid(), "Einführung in die Softwarearchitektur", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision2option4 = new StoryEvent(Guid.NewGuid(), "Usability als Erfolgsfaktor", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision2.addChild(decision2option1);
        decision2.addChild(decision2option2);
        decision2.addChild(decision2option3);
        decision2.addChild(decision2option4);

        StoryEvent storyelemnt2option1 = new StoryEvent(Guid.NewGuid(), "Du hast dein Anwendungsfach erfolgreich bestanden", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 1, 0, 2));
        StoryEvent storyelemnt2option2 = new StoryEvent(Guid.NewGuid(), "Du hast dein Anwendungsfach erfolgreich bestanden", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 2, 0, 1));
        StoryEvent storyelemnt2option3 = new StoryEvent(Guid.NewGuid(), "Du hast dein Anwendungsfach erfolgreich bestanden", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 3, 0, 0));
        StoryEvent storyelemnt2option4 = new StoryEvent(Guid.NewGuid(), "Du hast dein Anwendungsfach erfolgreich bestanden", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(2, 1, 0, 0));

        decision2option1.addChild(storyelemnt2option1);
        decision2option2.addChild(storyelemnt2option2);
        decision2option3.addChild(storyelemnt2option3);
        decision2option4.addChild(storyelemnt2option4);

        StoryEvent storyelement3 = new StoryEvent(Guid.NewGuid(), "Nach dem du dein Anwendungsfach bestanden hast kannst du deine Freizeit genießen oder an einer außerschulischen Aktivität teilnehemn.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        decision2option1.addChild(storyelement3);
        decision2option2.addChild(storyelement3);
        decision2option3.addChild(storyelement3);
        decision2option4.addChild(storyelement3);

        StoryEvent decision3 = new StoryEvent(Guid.NewGuid(), "Was willst du tun?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement3.addChild(decision3);

        StoryEvent decision3option1 = new StoryEvent(Guid.NewGuid(), "Du nimmst an einem Hackathon an der Universität teil.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision3option2 = new StoryEvent(Guid.NewGuid(), "Du nutzt deine Zeit um eine dir nicht bekannte Programmiersprache zu lernen", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision3option3 = new StoryEvent(Guid.NewGuid(), "Du genießt deine Freizeit und machst Party", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision3.addChild(decision3option1);
        decision3.addChild(decision3option2);
        decision3.addChild(decision3option3);

        StoryEvent storyelement4option1 = new StoryEvent(Guid.NewGuid(), "Du lernst viele neue Leute kennen und erweiterst deine Programmierkenntnisse.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(1, 0, 0, 2));
        StoryEvent storyelement4option2 = new StoryEvent(Guid.NewGuid(), "Du lernst die Programmiersprache CSharp kennen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 1, 0, 2));
        StoryEvent storyelement4option3 = new StoryEvent(Guid.NewGuid(), "Du genießt das Leben und machst Party, aber dafür rosten deine Programmierkenntnisse etwas ein.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 4, -1));

        decision3option1.addChild(storyelement4option1);
        decision3option2.addChild(storyelement4option2);
        decision3option3.addChild(storyelement4option3);

        StoryEvent storyelement5 = new StoryEvent(Guid.NewGuid(), "Du hast dein Studium erfolgreich abgeschlossen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement4option1.addChild(storyelement5);
        storyelement4option2.addChild(storyelement5);
        storyelement4option3.addChild(storyelement5);

        StoryEvent storyelement6 = new StoryEvent(Guid.NewGuid(), "... ein halbes Jahr vergeht ...", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement5.addChild(storyelement6);

        StoryEvent storyelement7 = new StoryEvent(Guid.NewGuid(), "Du hast einen Job bei NewTec GmbH angenommen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement6.addChild(storyelement7);
        
        StoryEvent storyelement8 = new StoryEvent(Guid.NewGuid(), "Dein Chef hat einen neuen Auftrag von WizzBook an Land gezogen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement7.addChild(storyelement8);

        StoryEvent storyelement9 = new StoryEvent(Guid.NewGuid(), "Deine Aufgabe ist es das User Interface zu designen und zu implementieren.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
       
        storyelement8.addChild(storyelement9);

        StoryEvent decision4 = new StoryEvent(Guid.NewGuid(), "Wie willst du mit deiner Aufgabe anfangen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement9.addChild(decision4);

        StoryEvent decision4option1 = new StoryEvent(Guid.NewGuid(), "Du frägst deinen Mentor.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision4option2 = new StoryEvent(Guid.NewGuid(), "Du suchst nach einer Lösung auf HeapOverflow.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision4option3 = new StoryEvent(Guid.NewGuid(), "Du fängst einfach mit der Arbeit an.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision4.addChild(decision4option1);
        decision4.addChild(decision4option2);
        decision4.addChild(decision4option3);

        StoryEvent storyelement10option1 = new StoryEvent(Guid.NewGuid(), "Du lernst deinen Mentor Yaggaya kennen. Yaggaya hilft dir bei deiner Aufgabe.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow,new Skills(1,0,0,1));
        StoryEvent storyelement10option2 = new StoryEvent(Guid.NewGuid(), "Du löst das problem, verschwendest aber eine Menge Zeit.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow,new Skills(0,0,0,1));
        StoryEvent storyelement10option3 = new StoryEvent(Guid.NewGuid(), "Du verschwendest eine Menge Zeit ohne das Problem zu lösen. Du benötigst für deine Aufgabe einen extra Tag.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0,-1,0,0));

        decision4option1.addChild(storyelement10option1);
        decision4option2.addChild(storyelement10option2);
        decision4option3.addChild(storyelement10option3);

        StoryEvent storyelement11 = new StoryEvent(Guid.NewGuid(), "Nach deiner ersten Aufgabe musst du nun die dazugehörigen Tests implementieren.",new HashSet<StoryEvent>(),StoryEventType.StoryFlow);

        storyelement10option1.addChild(storyelement11);
        storyelement10option2.addChild(storyelement11);
        storyelement10option3.addChild(storyelement11);

        StoryEvent storyelement12 = new StoryEvent(Guid.NewGuid(), "Du hast nur wenige Tage Zeit die Tests zu implementieren, da die Deadline des Projekts sicher immer weiter nähert.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement11.addChild(storyelement12);

        StoryEvent decision5 = new StoryEvent(Guid.NewGuid(), "Wie willst du vorgehen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement12.addChild(decision5);

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        StoryEvent decision5option1 = new StoryEvent(Guid.NewGuid(), "Du schreibst umfangreichge Tests bis sie fertig sind ohne auf die Deadline zu schauen", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision5option2 = new StoryEvent(Guid.NewGuid(), "Du schreibst Tests bis die Deadline erreicht ist, was du bis dahin nicht schaffst bleibt ungetestet.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        
        decision5.addChild(decision5option1);
        decision5.addChild(decision5option2);

        StoryEvent storyelement11option1 = new StoryEvent(Guid.NewGuid(), "Durch deine guten Programmierkenntnisse kannst du trotzdem die Deadline einhalten.", new HashSet<StoryEvent>(),StoryEventType.StoryFlow, true);
        StoryEvent storyelement11option2 = new StoryEvent(Guid.NewGuid(), "Leider wirst du nicht rechtzeitig fertig aber du hast sehr umfangreiche Tests geschrieben.", new HashSet<StoryEvent>(),StoryEventType.StoryFlow, false);

        decision5option1.addChild(storyelement11option1);
        decision5option1.addChild(storyelement11option2);

        StoryEvent storyelement11option3 = new StoryEvent(Guid.NewGuid(), "Durch deine guten Programmierkenntnisse hast du es trotz der kurzen Zeit geschafft umfangreiche Tests zu schreiben", new HashSet<StoryEvent>(),StoryEventType.StoryFlow, true);
        StoryEvent storyelement11option4 = new StoryEvent(Guid.NewGuid(), "Die Deadline wurde erreicht aber du hast es nicht geschafft alles umfangreich zu testen.", new HashSet<StoryEvent>(),StoryEventType.StoryFlow, false);

        decision5option2.addChild(storyelement11option3);
        decision5option2.addChild(storyelement11option4);

        StoryEvent storyelement12option1 = new StoryEvent(Guid.NewGuid(), "Yaggaya ist sehr zufrieden mit deiner herangehensweise und deinen Tests.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 3, 0, 1));
        StoryEvent storyelement12option2 = new StoryEvent(Guid.NewGuid(), "Yaggaya ist sehr zufrieden mit deiner herangehensweise und deinen Tests, obwohl du nicht rechtzeitig fertig geworden bist.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 2, 0, 2));

        storyelement11option1.addChild(storyelement12option1);
        storyelement11option2.addChild(storyelement12option2);

        StoryEvent storyelement12option3 = new StoryEvent(Guid.NewGuid(), "Yaggaya ist sehr zufrieden mit deiner Leistung.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 0, 2));
        StoryEvent storyelement12option4 = new StoryEvent(Guid.NewGuid(), "Yaggaya ist nicht zufrieden mit deiner Leistung. Du musst die Tests nochmal überarbeiten und überschreitest die Deadline um ein paar Tage.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, -2, 0, 1));

        storyelement11option3.addChild(storyelement12option3);
        storyelement11option4.addChild(storyelement12option4);

        StoryEvent storyelement13 = new StoryEvent(Guid.NewGuid(), "Das schreiben der Tests ist abgeschlossen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement12option1.addChild(storyelement13);
        storyelement12option2.addChild(storyelement13);
        storyelement12option3.addChild(storyelement13);
        storyelement12option4.addChild(storyelement13);

        // company party

        StoryEvent storyelement14 = new StoryEvent(Guid.NewGuid(), "Es steht eine Firmenfeier an.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement13.addChild(storyelement14);

        StoryEvent storyelement15 = new StoryEvent(Guid.NewGuid(), "Die Tests haben ein paar Fehler aufgedeckt, wenn du sie behebst kannst du nicht zu Firmenfeier.",new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement14.addChild(storyelement15);
        
        StoryEvent decision6 = new StoryEvent(Guid.NewGuid(), "Gehst du zur Firmenparty?",new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement15.addChild(decision6);

        StoryEvent decision6option1 = new StoryEvent(Guid.NewGuid(), "Du gehst zu Firmenfeier.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision6option2 = new StoryEvent(Guid.NewGuid(), "Du arbeitest am Projekt weiter.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision6.addChild(decision6option1);
        decision6.addChild(decision6option2);

        StoryEvent storyelement16 = new StoryEvent(Guid.NewGuid(), "Du arbeitest weiter am Projekt und behebst die Fehler.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 0, 2));

        decision6option2.addChild(storyelement16);

        // at the company party

        StoryEvent storyelement18 = new StoryEvent(Guid.NewGuid(), "Du bist auf der Firmenfeier", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        decision6option1.addChild(storyelement18);

        StoryEvent decision7 = new StoryEvent(Guid.NewGuid(), "Was willst du auf der Firmenfeier machen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement18.addChild(decision7);

        StoryEvent decision7option1 = new StoryEvent(Guid.NewGuid(), "Du plauderst mit Kollegen.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision7option2 = new StoryEvent(Guid.NewGuid(), "Du redest mit deinem Mentor und deinem Chef.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision7option3 = new StoryEvent(Guid.NewGuid(), "Du suchst dir alleine einen Sitzplatz um etwas zu essen.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision7.addChild(decision7option1);
        decision7.addChild(decision7option2);
        decision7.addChild(decision7option3);

        StoryEvent storyelement19option1 = new StoryEvent(Guid.NewGuid(), "Du bekommst von Kollegen ein paar Tipps.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 0, 2));

        // how to influences later decisions ???
        StoryEvent storyelement19option2 = new StoryEvent(Guid.NewGuid(), "Es wurden zusätzliche Dialogoptionen für spätere Entscheidungen freigeschaltet ", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        decision7option1.addChild(storyelement19option1);
        decision7option2.addChild(storyelement19option2);

        StoryEvent storyelement20 = new StoryEvent(Guid.NewGuid(), "Du hast durst und holst dir ein Getränk.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement19option1.addChild(storyelement20);
        storyelement19option2.addChild(storyelement20);
        decision7option3.addChild(storyelement20);

        StoryEvent decision8 = new StoryEvent(Guid.NewGuid(), "Was möchtest du trinken?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement20.addChild(decision8);

        StoryEvent decision8option1 = new StoryEvent(Guid.NewGuid(), "Ein Bier reicht für den Abend.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision8option2 = new StoryEvent(Guid.NewGuid(), "Eine Cola tuts auch.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision8option3 = new StoryEvent(Guid.NewGuid(), "Ein paar Shots sind ok, es kostet ja nichts.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision8.addChild(decision8option1);
        decision8.addChild(decision8option2);
        decision8.addChild(decision8option3);

        StoryEvent storyelement21option1 = new StoryEvent(Guid.NewGuid(), "Du hast die Firmenfeier gut überstanden", new HashSet<StoryEvent>(), StoryEventType.StoryFlow,new Skills(0,0,1,0));

        decision8option1.addChild(storyelement21option1);
        decision8option2.addChild(storyelement21option1);

        StoryEvent storyelement21option2 = new StoryEvent(Guid.NewGuid(), "Du trinkst zwei Shots.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        decision8option3.addChild(storyelement21option2);

        StoryEvent decision9 = new StoryEvent(Guid.NewGuid(), "Willst du noch mehr Shots trinken?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement21option2.addChild(decision9);

        StoryEvent decision9option1 = new StoryEvent(Guid.NewGuid(), "Mehr Shots trinken.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision9option2 = new StoryEvent(Guid.NewGuid(), "Nein, zwei sind genug.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision9.addChild(decision9option1);
        decision9.addChild(decision9option2);

        StoryEvent storyelement22option1 = new StoryEvent(Guid.NewGuid(), "Du sorgst für gute Stimmung auf der Party.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow,new Skills(0,0,2,0));


        // affected by dice roll
        StoryEvent storyelement22option2 = new StoryEvent(Guid.NewGuid(), "Du überstehst die Firmenfeier ohne für Aufsehen zu sorgen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow,new Skills(0,0,3,0), true);
        StoryEvent storyelement22option3 = new StoryEvent(Guid.NewGuid(), "Du sorgst für gute Stimmung aber fällst negativ auf, weil du ziemlich stark betrunken bist.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow,new Skills(-2,0,3,0), false);

        decision9option2.addChild(storyelement22option1);
        decision9option2.addChild(storyelement22option2);
        decision9option2.addChild(storyelement22option3);

        StoryEvent storyelement23 = new StoryEvent(Guid.NewGuid(), "Die Firmenfeier ist zu Ende und du gehts nach Hause.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement22option1.addChild(storyelement23);
        storyelement22option2.addChild(storyelement23);
        storyelement22option3.addChild(storyelement23);

        // training course

        StoryEvent storyelement17 = new StoryEvent(Guid.NewGuid(), "Eine Weiterbildung steht an.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement16.addChild(storyelement17);
        storyelement23.addChild(storyelement17);

        // Deadline should be mentioned here as well
        StoryEvent decision10 = new StoryEvent(Guid.NewGuid(), "Willst du an der Weiterbildung teilnehmen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement17.addChild(decision10);

        StoryEvent decision10option1 = new StoryEvent(Guid.NewGuid(), "Klar, warum nicht.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision10option2 = new StoryEvent(Guid.NewGuid(), "Du kennst das Thema schon von der Uni und arbeitst lieber an dem Projekt weiter.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision10option3 = new StoryEvent(Guid.NewGuid(), "Nein, du brauchst erstmal Urlaub", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        // only if dialog option was ulocked previously
        StoryEvent decision10option4 = new StoryEvent(Guid.NewGuid(), "Dein Chef bietet dir an nach der Weiterbildung Urlaub zu machen.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision10.addChild(decision10option1);
        decision10.addChild(decision10option2);
        decision10.addChild(decision10option3);
        decision10.addChild(decision10option4);

        StoryEvent storyelement24option1 = new StoryEvent(Guid.NewGuid(), "Du kannst deine Deadline nicht einhalten aber deinem Chef gefällt dein Engagement deine Fähigkeiten zu erweitern.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0,0,0,1));
        StoryEvent storyelement24option2 = new StoryEvent(Guid.NewGuid(), "Du arbeitest an dem Projekt weiter und schaffst es deine Deadline einzuhalten.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 0, 0));
        StoryEvent storyelement24option3 = new StoryEvent(Guid.NewGuid(), "Da du Urlaub genommen hast schaffst du es nicht die Deadline einzuhalten, dein Chef ist deshalb ziemlich sauer.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-2, -2, 0, 0));
        StoryEvent storyelement24option4 = new StoryEvent(Guid.NewGuid(), "Nach der Weiterbildung genießt du eine Woche Urlaub und da dein Chef sich um eine Vertetung gekümmert hat, hast du keine Probleme die Deadline einzuhalten.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 2, 0, 2));

        decision10option1.addChild(storyelement24option1);
        decision10option2.addChild(storyelement24option2);
        decision10option3.addChild(storyelement24option3);
        decision10option4.addChild(storyelement24option4);

        StoryEvent storyelement25 = new StoryEvent(Guid.NewGuid(), "Deine Telefon klingelt. WizzBook ruft an und möchte Änderungen an der WizzApp vornehmen.",new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement24option1.addChild(storyelement25);
        storyelement24option2.addChild(storyelement25);
        storyelement24option3.addChild(storyelement25);
        storyelement24option4.addChild(storyelement25);

        StoryEvent storyelement26 = new StoryEvent(Guid.NewGuid(), "Du nimmst den Anruf entgegen und hörst dir WizzApps Änderungswünsche an.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement25.addChild(storyelement26);

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        StoryEvent storyelement27 = new StoryEvent(Guid.NewGuid(), "Wizzbook will das du die Änderungen umsetzt.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement26.addChild(storyelement27);

        StoryEvent decision11 = new StoryEvent(Guid.NewGuid(), "Was machst du?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement27.addChild(decision11);

        StoryEvent decision11option1 = new StoryEvent(Guid.NewGuid(), "Du hast keine Lust auf zusätzliche Arbeit und legst deshalb einfach während dem Telefonat auf.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision11option2 = new StoryEvent(Guid.NewGuid(), "Nach dem Telefonat setzt du die gewünschten Änderungen um.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision11option3 = new StoryEvent(Guid.NewGuid(), "Nach dem Telefonat fällt dir auf, dass die Änderungswünsche technisch nicht umsetzbar sind.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision11.addChild(decision11option1);
        decision11.addChild(decision11option2);
        decision11.addChild(decision11option3);

        StoryEvent storyelement28option1 = new StoryEvent(Guid.NewGuid(), "Du hast aufgelegt.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption, new Skills(-3, 0, 0, 0));
        StoryEvent storyelement28option2 = new StoryEvent(Guid.NewGuid(), "Du hast die gewünschten Änderungen umgesetzt.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption, new Skills(0, 0, 0, 2));
        StoryEvent decision12 = new StoryEvent(Guid.NewGuid(), "Wie willst du weiter vorgehen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        decision11option1.addChild(storyelement28option1);
        decision11option2.addChild(storyelement28option2);
        decision11option3.addChild(decision12);

        StoryEvent decision12option1 = new StoryEvent(Guid.NewGuid(), "Du informierst den Kunden.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision12option2 = new StoryEvent(Guid.NewGuid(), "Du setzt die Änderungen nicht um ohne den Kunden darüber zu informieren.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision12.addChild(decision12option1);
        decision12.addChild(decision12option2);

        StoryEvent storyelement29option1 = new StoryEvent(Guid.NewGuid(), "Du rufst WizzBook zurück und erklärst, dass die gewünschte Änderung nicht umsetzbar ist.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(1, 0, 0, 0));
        StoryEvent storyelement29option2 = new StoryEvent(Guid.NewGuid(), "Du hast WizzBook nicht informiert.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-2, 0, 0, 0));

        decision12option1.addChild(storyelement29option1);
        decision12option2.addChild(storyelement29option2);

        //bug has surfaced

        StoryEvent storyelement30 = new StoryEvent(Guid.NewGuid(), "Du hast einen neuen kritischen Bug entdeckt.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement28option1.addChild(storyelement30);
        storyelement28option2.addChild(storyelement30);
        storyelement29option1.addChild(storyelement30);
        storyelement29option2.addChild(storyelement30);

        StoryEvent storyelement31 = new StoryEvent(Guid.NewGuid(), "Bis jetzt weißt nur du von dem Bug.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement30.addChild(storyelement31);

        StoryEvent decision13 = new StoryEvent(Guid.NewGuid(), "Was machst du?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement31.addChild(decision13);

        StoryEvent decision13option1 = new StoryEvent(Guid.NewGuid(), "Du schreibst ein Bug Ticket.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision13option2 = new StoryEvent(Guid.NewGuid(), "Einfach ingorieren wird schon keinem auffallen.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision13.addChild(decision13option1);
        decision13.addChild(decision13option2);

        StoryEvent storyelement32 = new StoryEvent(Guid.NewGuid(), "Yaggaya sieht dein Ticket und leitet es weiter. Yaggaya lobt deine Arbeitsweise.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption, new Skills(0, 2, 0, 0));

        decision13option1.addChild(storyelement32);

        //affected by dice roll
        StoryEvent storyelement33option1 = new StoryEvent(Guid.NewGuid(), "Der Bug wurde gefunden und es kommt raus, dass du den Code mit dem Bug als letztes bearbeitet hast. Yaggaya ist sauer, da dir der Bug hätte auffallen müssen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-3, -3, 0, 0), true);
        StoryEvent storyelement33option2 = new StoryEvent(Guid.NewGuid(), "Der Bug wurde nicht gefunden.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, -3, 0, 0), false);

        decision13option2.addChild(storyelement33option1);
        decision13option2.addChild(storyelement33option2);

        //customer wants new feature

        StoryEvent storyelement34 = new StoryEvent(Guid.NewGuid(), "WizzBook meldet sich nochmals und dieses mal will WizzBook eine neue Funktion zu WizzApp hinzufügen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement32.addChild(storyelement34);
        storyelement33option1.addChild(storyelement34);
        storyelement33option2.addChild(storyelement34);

        StoryEvent storyelement35 = new StoryEvent(Guid.NewGuid(), "Dir kommt das etwas seltsam vor, da dies Probleme mit anderen Funktionen der App verursachen könnte.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement34.addChild(storyelement35);

        StoryEvent decision14 = new StoryEvent(Guid.NewGuid(), "Was machst du?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement35.addChild(decision14);

        StoryEvent decision14option1 = new StoryEvent(Guid.NewGuid(), "Der Kunde wird schon wissen was er da tut.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision14option2 = new StoryEvent(Guid.NewGuid(), "Du versuchst herauszufinden ob du den Kunden richtig verstanden hast und ob er unbedingt das neue Feature haben möchte.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision14.addChild(decision14option1);
        decision14.addChild(decision14option2);

        StoryEvent storyelement36option1 = new StoryEvent(Guid.NewGuid(), "Du setzt die neue Funktion alleine um.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-2, 0, 0, 2));
        StoryEvent storyelement36option2 = new StoryEvent(Guid.NewGuid(), "Du sprichst mit dem Kunden über die neue Funktion.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(2, 0, 0, 0));

        decision14option1.addChild(storyelement36option1);
        decision14option1.addChild(storyelement36option2);

        StoryEvent storyelement37= new StoryEvent(Guid.NewGuid(), "Der Kunde hat sich dazu entschieden mit der neuen Funktion den Änderungsprozess zu druchlaufen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement36option2.addChild(storyelement37);

        StoryEvent decision15= new StoryEvent(Guid.NewGuid(), "Wie gehst du vor?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement37.addChild(decision15);

        StoryEvent decision15option1 = new StoryEvent(Guid.NewGuid(), "Du entscheidest dich die Änderung mit dem Kunden alleine umzusetzten.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision15option2 = new StoryEvent(Guid.NewGuid(), "Du organisierst ein Meeting.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision15.addChild(decision15option1);
        decision15.addChild(decision15option2);

        StoryEvent storyelement38option1 = new StoryEvent(Guid.NewGuid(), "Du hast die neue Funktion alleine umgesetzt.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-2, 0, 0, 2));
        StoryEvent storyelement38option2 = new StoryEvent(Guid.NewGuid(), "Dein Chef is sehr zufrieden mit deiner Herangehensweise.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(1, 2, 0, 0));

        decision15option1.addChild(storyelement38option1);
        decision15option2.addChild(storyelement38option2);

        StoryEvent storyelement39 = new StoryEvent(Guid.NewGuid(), "Die neue Funktion wurde implementiert.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement36option1.addChild(storyelement39);
        storyelement38option1.addChild(storyelement39);
        storyelement38option2.addChild(storyelement39);

        // colleague needs help

        StoryEvent storyelement40 = new StoryEvent(Guid.NewGuid(), "Dein Kollege Trummu kommt auf dich zu da er drigend deine Hilfe benötigt.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement39.addChild(storyelement40);

        StoryEvent decision16 = new StoryEvent(Guid.NewGuid(), "Willst du Trummu helfen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement40.addChild(decision16);

        StoryEvent decision16option1 = new StoryEvent(Guid.NewGuid(), "Du hast gerade viel zu tun und kümmerst dich lieber um deine eigene Arbeit.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision16option2 = new StoryEvent(Guid.NewGuid(), "Du hilfst Trummu auch wenn du dadurch vielleicht deine Deadline verpasst.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision16.addChild(decision16option1);
        decision16.addChild(decision16option2);

        //dice roll
        StoryEvent storyelement41option1 = new StoryEvent(Guid.NewGuid(), "Trummu ist zufrieden und du schafft es deine Deadline einzuhalten.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(2, 0, 0, 0), true);
        StoryEvent storyelement41option2= new StoryEvent(Guid.NewGuid(), "Trummu ist zufrieden aber du wirst es nicht schaffen deine Deadline zu halten.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(2, 0, 0, 0), false);

        decision16option2.addChild(storyelement41option1);
        decision16option2.addChild(storyelement41option2);

        StoryEvent decision17= new StoryEvent(Guid.NewGuid(), "Frags du um Hilfe um deine Deadline noch halten zu können?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement41option2.addChild(decision17);

        StoryEvent decision17option1 = new StoryEvent(Guid.NewGuid(), "Ja, du fragst Yaggaya um Hilfe.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision17option2 = new StoryEvent(Guid.NewGuid(), "Nein.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision17.addChild(decision17option1);
        decision17.addChild(decision17option2);

        //high communication skills required
        StoryEvent storyelement42option1 = new StoryEvent(Guid.NewGuid(), "Yaggaya hilft dir und du schafst deine Deadline.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, true);

        decision17option1.addChild(storyelement42option1);

        StoryEvent storyelement42option2 = new StoryEvent(Guid.NewGuid(), "Yaggaya wird dir nicht helfen und du verpasst deine Deadline.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, -2, 0, 0), false);

        decision17option1.addChild(storyelement42option2);

        StoryEvent storyelement43 = new StoryEvent(Guid.NewGuid(), "Du verpasst deine Deadline.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, -2, 0, 0));

        decision17option2.addChild(storyelement43);

        StoryEvent storyelement44 = new StoryEvent(Guid.NewGuid(), "Trummu ist verärgert das du im nicht geholfen hast.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-2, 0, 0, 0));

        decision16option1.addChild(storyelement44);

        //code review

        StoryEvent storyelement45 = new StoryEvent(Guid.NewGuid(), "Das Projekt ist fast abgeschlossen. Es fehlt nur noch die Code Review.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement44.addChild(storyelement45);
        storyelement42option1.addChild(storyelement45);
        storyelement42option2.addChild(storyelement45);
        storyelement43.addChild(storyelement45);

        StoryEvent decision18 = new StoryEvent(Guid.NewGuid(), "Willst du an der Code Review teilnehmen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement45.addChild(decision18);

        StoryEvent decision18option1 = new StoryEvent(Guid.NewGuid(), "Du nimmst an der Code Review teil.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision18option2 = new StoryEvent(Guid.NewGuid(), "Alles läuft gut, wird schon passen, eine Review ist nicht notwendig.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision18.addChild(decision18option1);
        decision18.addChild(decision18option2);

        StoryEvent storyelement46option1 = new StoryEvent(Guid.NewGuid(), "Du nimmst an der Code Review teil und erhälst Tipps die später mal sehr hilfreich sein werden.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(2, 2, 0, 0));
        StoryEvent storyelement46option2 = new StoryEvent(Guid.NewGuid(), "Dein Chef findet es garnicht gut das du nicht an der Code Review teilgenommen hast.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-2, -2, 0, 0));

        decision18option1.addChild(storyelement46option1);
        decision18option2.addChild(storyelement46option2);

        StoryEvent storyelement47 = new StoryEvent(Guid.NewGuid(), "Das Projekt ist abgeschlossen", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement46option1.addChild(storyelement47);
        storyelement46option2.addChild(storyelement47);

        //workshop

        StoryEvent storyelement48 = new StoryEvent(Guid.NewGuid(), "Die besten Mitarbeiter werden zu einem Workshop auf Hawaii eingeladen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement47.addChild(storyelement48);

        //sufficient amount of skill points required
        StoryEvent storyelement49option1 = new StoryEvent(Guid.NewGuid(), "Glückwunsch du wurdest zum Workshop eingeladen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        StoryEvent storyelement49option2 = new StoryEvent(Guid.NewGuid(), "Leider warst du nicht gut genug, vielleicht wird dein nächstes Projekt erfolgreicher.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        StoryEvent storyelement49option3 = new StoryEvent(Guid.NewGuid(), "Leider musst du die Firma verlassen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement48.addChild(storyelement49option1);
        storyelement48.addChild(storyelement49option2);
        storyelement48.addChild(storyelement49option3);

        StoryEvent end = new StoryEvent(Guid.NewGuid(), "Ende.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement49option1.addChild(end);
        storyelement49option2.addChild(end);
        storyelement49option3.addChild(end);

    }

}
