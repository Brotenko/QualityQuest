using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Game;
using UnityEngine;
using Random = System.Random;

public class StoryGraph 
{
    public Character Character { get; set; }
    public StoryEvent Root { get; }
    public StoryEvent CurrentEvent { get; set; }

    /// <summary>
    /// Constructor for the StoryGraph class. 
    /// </summary>
    public StoryGraph()
    {
        var root = new StoryEvent(Guid.NewGuid(), "Mit welchem Charakter möchtest du das Spiel spielen?", new HashSet<StoryEvent>(), StoryEventType.StoryRootEvent);

        var character1 = new StoryEvent(Guid.NewGuid(), "Noruso", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var character2 = new StoryEvent(Guid.NewGuid(), "Lumati", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var character3 = new StoryEvent(Guid.NewGuid(), "Turgal", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var character4 = new StoryEvent(Guid.NewGuid(), "Kirogh", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);


        root.AddChild(character1);
        root.AddChild(character2);
        root.AddChild(character3);
        root.AddChild(character4);

        var storyelement1 = new StoryEvent(Guid.NewGuid(), "Dir fehlt noch ein Anwendungsfach am Ende deines Studiums.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        character1.AddChild(storyelement1);
        character2.AddChild(storyelement1);
        character3.AddChild(storyelement1);
        character4.AddChild(storyelement1);

        var decision2 = new StoryEvent(Guid.NewGuid(), "Welches Anwendungsfach möchtest du auswählen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement1.AddChild(decision2);

        var decision2option1 = new StoryEvent(Guid.NewGuid(), "Einführung in Bali", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision2option2 = new StoryEvent(Guid.NewGuid(), "Anforderungsanalyse", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision2option3 = new StoryEvent(Guid.NewGuid(), "Einführung in die Softwarearchitektur", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision2option4 = new StoryEvent(Guid.NewGuid(), "Usability als Erfolgsfaktor", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision2.AddChild(decision2option1);
        decision2.AddChild(decision2option2);
        decision2.AddChild(decision2option3);
        decision2.AddChild(decision2option4);

        var storyelemnt2option1 = new StoryEvent(Guid.NewGuid(), "Du hast dein Anwendungsfach erfolgreich bestanden.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 1, 0, 2));
        var storyelemnt2option2 = new StoryEvent(Guid.NewGuid(), "Du hast dein Anwendungsfach erfolgreich bestanden.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 2, 0, 1));
        var storyelemnt2option3 = new StoryEvent(Guid.NewGuid(), "Du hast dein Anwendungsfach erfolgreich bestanden.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 3, 0, 0));
        var storyelemnt2option4 = new StoryEvent(Guid.NewGuid(), "Du hast dein Anwendungsfach erfolgreich bestanden.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(2, 1, 0, 0));

        decision2option1.AddChild(storyelemnt2option1);
        decision2option2.AddChild(storyelemnt2option2);
        decision2option3.AddChild(storyelemnt2option3);
        decision2option4.AddChild(storyelemnt2option4);

        var storyelement3 = new StoryEvent(Guid.NewGuid(), "Nach dem du dein Anwendungsfach bestanden hast kannst du deine Freizeit genießen oder an einer außerschulischen Aktivität teilnehmen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelemnt2option1.AddChild(storyelement3);
        storyelemnt2option2.AddChild(storyelement3);
        storyelemnt2option3.AddChild(storyelement3);
        storyelemnt2option4.AddChild(storyelement3);

        var decision3 = new StoryEvent(Guid.NewGuid(), "Was willst du tun?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement3.AddChild(decision3);

        var decision3option1 = new StoryEvent(Guid.NewGuid(), "Du nimmst an einem Hackathon an der Universität teil.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision3option2 = new StoryEvent(Guid.NewGuid(), "Du nutzt deine Zeit um eine dir nicht bekannte Programmiersprache zu lernen.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision3option3 = new StoryEvent(Guid.NewGuid(), "Du genießt deine Freizeit und machst Party.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision3.AddChild(decision3option1);
        decision3.AddChild(decision3option2);
        decision3.AddChild(decision3option3);

        var storyelement4option1 = new StoryEvent(Guid.NewGuid(), "Du lernst viele neue Leute kennen und erweiterst deine Programmierkenntnisse.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(1, 0, 0, 2));
        var storyelement4option2 = new StoryEvent(Guid.NewGuid(), "Du lernst die Programmiersprache CSharp kennen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 1, 0, 2));
        var storyelement4option3 = new StoryEvent(Guid.NewGuid(), "Du genießt das Leben und machst Party, aber dafür rosten deine Programmierkenntnisse etwas ein.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 4, -1));

        decision3option1.AddChild(storyelement4option1);
        decision3option2.AddChild(storyelement4option2);
        decision3option3.AddChild(storyelement4option3);

        var storyelement5 = new StoryEvent(Guid.NewGuid(), "Du hast dein Studium erfolgreich abgeschlossen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement4option1.AddChild(storyelement5);
        storyelement4option2.AddChild(storyelement5);
        storyelement4option3.AddChild(storyelement5);

        var background2 = new StoryEvent(BackgroundType.Internship, new HashSet<StoryEvent>(), StoryEventType.StoryBackground);

        storyelement5.AddChild(background2);

        var storyelement6 = new StoryEvent(Guid.NewGuid(), "... ein halbes Jahr vergeht ...", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        background2.AddChild(storyelement6);

        var storyelement7 = new StoryEvent(Guid.NewGuid(), "Du hast einen Job bei NewTec GmbH angenommen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement6.AddChild(storyelement7);

        var storyelement8 = new StoryEvent(Guid.NewGuid(), "Dein Chef hat einen neuen Auftrag von WizzBook an Land gezogen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement7.AddChild(storyelement8);

        var storyelement9 = new StoryEvent(Guid.NewGuid(), "Deine Aufgabe ist es das User Interface zu designen und zu implementieren.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement8.AddChild(storyelement9);

        var decision4 = new StoryEvent(Guid.NewGuid(), "Wie willst du mit deiner Aufgabe anfangen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement9.AddChild(decision4);

        var decision4option1 = new StoryEvent(Guid.NewGuid(), "Du frägst deinen Mentor.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision4option2 = new StoryEvent(Guid.NewGuid(), "Du suchst nach einer Lösung auf HeapOverflow.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision4option3 = new StoryEvent(Guid.NewGuid(), "Du fängst einfach mit der Arbeit an.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision4.AddChild(decision4option1);
        decision4.AddChild(decision4option2);
        decision4.AddChild(decision4option3);

        var storyelement10option1 = new StoryEvent(Guid.NewGuid(), "Du lernst deinen Mentor Yaggaya kennen. Yaggaya hilft dir bei deiner Aufgabe.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(1, 0, 0, 1));
        var storyelement10option2 = new StoryEvent(Guid.NewGuid(), "Du löst das Problem, verschwendest aber eine Menge Zeit.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 0, 1));
        var storyelement10option3 = new StoryEvent(Guid.NewGuid(), "Du verschwendest eine Menge Zeit ohne das Problem zu lösen. Du benötigst für deine Aufgabe einen extra Tag.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, -1, 0, 0));

        decision4option1.AddChild(storyelement10option1);
        decision4option2.AddChild(storyelement10option2);
        decision4option3.AddChild(storyelement10option3);

        var storyelement11 = new StoryEvent(Guid.NewGuid(), "Nach deiner ersten Aufgabe musst du nun die dazugehörigen Tests implementieren.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement10option1.AddChild(storyelement11);
        storyelement10option2.AddChild(storyelement11);
        storyelement10option3.AddChild(storyelement11);

        var storyelement12 = new StoryEvent(Guid.NewGuid(), "Du hast nur wenige Tage Zeit die Tests zu implementieren, da die Deadline des Projekts sich immer weiter nähert.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement11.AddChild(storyelement12);

        var decision5 = new StoryEvent(Guid.NewGuid(), "Wie willst du vorgehen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement12.AddChild(decision5);

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        var decision5option1 = new StoryEvent(Guid.NewGuid(), "Du schreibst umfangreiche Tests bis sie fertig sind, ohne die Deadline zu beachten.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision5option2 = new StoryEvent(Guid.NewGuid(), "Du schreibst Tests bis die Deadline erreicht ist, was du bis dahin nicht schaffst bleibt ungetestet.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision5.AddChild(decision5option1);
        decision5.AddChild(decision5option2);

        var storyelement11option1 = new StoryEvent(Guid.NewGuid(), "Durch deine guten Programmierkenntnisse kannst du trotzdem die Deadline einhalten.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, true, RandomType.RandomDecisionOne);
        var storyelement11option2 = new StoryEvent(Guid.NewGuid(), "Leider wirst du nicht rechtzeitig fertig, aber du hast sehr umfangreiche Tests geschrieben.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, false, RandomType.RandomDecisionOne);

        decision5option1.AddChild(storyelement11option1);
        decision5option1.AddChild(storyelement11option2);

        var storyelement11option3 = new StoryEvent(Guid.NewGuid(), "Durch deine guten Programmierkenntnisse hast du es trotz der kurzen Zeit geschafft umfangreiche Tests zu schreiben.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, true, RandomType.RandomDecisionTwo);
        var storyelement11option4 = new StoryEvent(Guid.NewGuid(), "Die Deadline wurde erreicht, du hast es nicht geschafft alles umfangreich zu testen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, false, RandomType.RandomDecisionTwo);

        decision5option2.AddChild(storyelement11option3);
        decision5option2.AddChild(storyelement11option4);

        var storyelement12option1 = new StoryEvent(Guid.NewGuid(), "Yaggaya ist sehr zufrieden mit deiner Herangehensweise und deinen Tests.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 3, 0, 1));
        var storyelement12option2 = new StoryEvent(Guid.NewGuid(), "Yaggaya ist sehr zufrieden mit deiner Herangehensweise und deinen Tests, obwohl du nicht rechtzeitig fertig geworden bist.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 2, 0, 2));

        storyelement11option1.AddChild(storyelement12option1);
        storyelement11option2.AddChild(storyelement12option2);

        var storyelement12option3 = new StoryEvent(Guid.NewGuid(), "Yaggaya ist sehr zufrieden mit deiner Leistung.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 0, 2));
        var storyelement12option4 = new StoryEvent(Guid.NewGuid(), "Yaggaya ist nicht zufrieden mit deiner Leistung. Du musst die Tests nochmal überarbeiten und überschreitest die Deadline um ein paar Tage.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, -2, 0, 1));

        storyelement11option3.AddChild(storyelement12option3);
        storyelement11option4.AddChild(storyelement12option4);

        var storyelement13 = new StoryEvent(Guid.NewGuid(), "Das schreiben der Tests ist abgeschlossen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement12option1.AddChild(storyelement13);
        storyelement12option2.AddChild(storyelement13);
        storyelement12option3.AddChild(storyelement13);
        storyelement12option4.AddChild(storyelement13);

        // company party

        var storyelement14 = new StoryEvent(Guid.NewGuid(), "Es steht eine Firmenfeier an.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement13.AddChild(storyelement14);

        var storyelement15 = new StoryEvent(Guid.NewGuid(), "Die Tests haben ein paar Fehler aufgedeckt, wenn du sie behebst hast du keine Zeit um an der Firmenfeier teilzunehmen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement14.AddChild(storyelement15);

        var decision6 = new StoryEvent(Guid.NewGuid(), "Gehst du zur Firmenfeier?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement15.AddChild(decision6);

        var decision6option1 = new StoryEvent(Guid.NewGuid(), "Du gehst zu Firmenfeier.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision6option2 = new StoryEvent(Guid.NewGuid(), "Du arbeitest am Projekt weiter.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision6.AddChild(decision6option1);
        decision6.AddChild(decision6option2);

        var storyelement16 = new StoryEvent(Guid.NewGuid(), "Du arbeitest weiter am Projekt und behebst die Fehler.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 0, 2));

        decision6option2.AddChild(storyelement16);

        // at the company party

        var background4 = new StoryEvent(BackgroundType.Party, new HashSet<StoryEvent>(), StoryEventType.StoryBackground);

        decision6option1.AddChild(background4);

        var storyelement18 = new StoryEvent(Guid.NewGuid(), "Du bist auf der Firmenfeier.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        background4.AddChild(storyelement18);

        var decision7 = new StoryEvent(Guid.NewGuid(), "Was willst du auf der Firmenfeier machen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement18.AddChild(decision7);

        var decision7option1 = new StoryEvent(Guid.NewGuid(), "Du plauderst mit Kollegen.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision7option2 = new StoryEvent(Guid.NewGuid(), "Du redest mit deinem Mentor und deinem Chef.", new HashSet<StoryEvent>(), StoryEventType.StoryUnlockDecisionOption);
        var decision7option3 = new StoryEvent(Guid.NewGuid(), "Du suchst dir alleine einen Sitzplatz, um etwas zu essen.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision7.AddChild(decision7option1);
        decision7.AddChild(decision7option2);
        decision7.AddChild(decision7option3);

        var storyelement19option1 = new StoryEvent(Guid.NewGuid(), "Du bekommst von Kollegen ein paar hilfreiche Tipps.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 0, 2));

        // how to influences later decisions ???
        var storyelement19option2 = new StoryEvent(Guid.NewGuid(), "Es wurden zusätzliche Dialogoptionen für spätere Entscheidungen freigeschaltet.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        decision7option1.AddChild(storyelement19option1);
        decision7option2.AddChild(storyelement19option2);

        var storyelement20 = new StoryEvent(Guid.NewGuid(), "Du hast Durst und holst dir ein Getränk.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement19option1.AddChild(storyelement20);
        storyelement19option2.AddChild(storyelement20);
        decision7option3.AddChild(storyelement20);

        var decision8 = new StoryEvent(Guid.NewGuid(), "Was möchtest du trinken?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement20.AddChild(decision8);

        var decision8option1 = new StoryEvent(Guid.NewGuid(), "Ein Bier reicht für den Abend.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision8option2 = new StoryEvent(Guid.NewGuid(), "Eine Cola tuts auch.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision8option3 = new StoryEvent(Guid.NewGuid(), "Ein paar Shots sind ok, es kostet ja nichts.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision8.AddChild(decision8option1);
        decision8.AddChild(decision8option2);
        decision8.AddChild(decision8option3);

        var storyelement21option1 = new StoryEvent(Guid.NewGuid(), "Du hast die Firmenfeier gut überstanden.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 1, 0));

        decision8option1.AddChild(storyelement21option1);
        decision8option2.AddChild(storyelement21option1);

        var storyelement21option2 = new StoryEvent(Guid.NewGuid(), "Du trinkst zwei Shots.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        decision8option3.AddChild(storyelement21option2);

        var decision9 = new StoryEvent(Guid.NewGuid(), "Willst du noch mehr Shots trinken?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement21option2.AddChild(decision9);

        var decision9option1 = new StoryEvent(Guid.NewGuid(), "Mehr Shots trinken.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision9option2 = new StoryEvent(Guid.NewGuid(), "Nein, zwei sind genug.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision9.AddChild(decision9option1);
        decision9.AddChild(decision9option2);

        var storyelement22option1 = new StoryEvent(Guid.NewGuid(), "Du sorgst für gute Stimmung auf der Party.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 2, 0));

        // affected by dice roll
        var storyelement22option2 = new StoryEvent(Guid.NewGuid(), "Du überstehst die Firmenfeier ohne für Aufsehen zu sorgen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 3, 0), true, RandomType.RandomDecisionThree);
        var storyelement22option3 = new StoryEvent(Guid.NewGuid(), "Du sorgst für gute Stimmung, aber fällst negativ auf, weil du ziemlich stark betrunken bist.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-2, 0, 3, 0), false, RandomType.RandomDecisionThree);

        decision9option2.AddChild(storyelement22option1);
        decision9option1.AddChild(storyelement22option2);
        decision9option1.AddChild(storyelement22option3);

        var storyelement23 = new StoryEvent(Guid.NewGuid(), "Die Firmenfeier ist zu Ende und du gehst nach Hause.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement21option1.AddChild(storyelement23);
        storyelement22option1.AddChild(storyelement23);
        storyelement22option2.AddChild(storyelement23);
        storyelement22option3.AddChild(storyelement23);

        // training course

        var background5 = new StoryEvent(BackgroundType.Meeting, new HashSet<StoryEvent>(), StoryEventType.StoryBackground);

        storyelement16.AddChild(background5);
        storyelement23.AddChild(background5);

        var storyelement17 = new StoryEvent(Guid.NewGuid(), "Eine Weiterbildung steht an.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        background5.AddChild(storyelement17);

        // Deadline should be mentioned here as well
        var decision10 = new StoryEvent(Guid.NewGuid(), "Willst du an der Weiterbildung teilnehmen?", new HashSet<StoryEvent>(), StoryEventType.StorySpecialDecision);

        storyelement17.AddChild(decision10);

        var decision10option1 = new StoryEvent(Guid.NewGuid(), "Klar, warum nicht.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision10option2 = new StoryEvent(Guid.NewGuid(), "Du kennst das Thema schon von der Uni und arbeitest lieber an dem Projekt weiter.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision10option3 = new StoryEvent(Guid.NewGuid(), "Nein, du brauchst erstmal Urlaub.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        // only if dialog option was ulocked previously
        var decision10option4 = new StoryEvent(Guid.NewGuid(), "Dein Chef bietet dir an, nach der Weiterbildung Urlaub zu machen.", new HashSet<StoryEvent>(), StoryEventType.StorySpecialOption);

        decision10.AddChild(decision10option1);
        decision10.AddChild(decision10option2);
        decision10.AddChild(decision10option3);
        decision10.AddChild(decision10option4);

        var storyelement24option1 = new StoryEvent(Guid.NewGuid(), "Du kannst deine Deadline nicht einhalten, aber deinem Chef gefällt dein Engagement deine Fähigkeiten zu erweitern.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 0, 1));
        var storyelement24option2 = new StoryEvent(Guid.NewGuid(), "Du arbeitest an dem Projekt weiter und schaffst es deine Deadline einzuhalten.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 0, 0));
        var storyelement24option3 = new StoryEvent(Guid.NewGuid(), "Da du Urlaub genommen hast schaffst du es nicht die Deadline einzuhalten, dein Chef ist deshalb ziemlich sauer.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-2, -2, 0, 0));
        var storyelement24option4 = new StoryEvent(Guid.NewGuid(), "Nach der Weiterbildung genießt du eine Woche Urlaub und da dein Chef sich um eine Vertetung gekümmert hat, hast du keine Probleme die Deadline einzuhalten.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 2, 0, 2));

        decision10option1.AddChild(storyelement24option1);
        decision10option2.AddChild(storyelement24option2);
        decision10option3.AddChild(storyelement24option3);
        decision10option4.AddChild(storyelement24option4);

        var background6 = new StoryEvent(BackgroundType.Office, new HashSet<StoryEvent>(), StoryEventType.StoryBackground);

        storyelement24option1.AddChild(background6);
        storyelement24option2.AddChild(background6);
        storyelement24option3.AddChild(background6);
        storyelement24option4.AddChild(background6);

        var storyelement25 = new StoryEvent(Guid.NewGuid(), "Dein Telefon klingelt. WizzBook ruft an und möchte Änderungen an der WizzApp vornehmen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        background6.AddChild(storyelement25);

        var storyelement26 = new StoryEvent(Guid.NewGuid(), "Du nimmst den Anruf entgegen und hörst dir WizzBooks Änderungswünsche an.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement25.AddChild(storyelement26);

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        var storyelement27 = new StoryEvent(Guid.NewGuid(), "Wizzbook will das du die Änderungen umsetzt.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement26.AddChild(storyelement27);

        var decision11 = new StoryEvent(Guid.NewGuid(), "Was machst du?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement27.AddChild(decision11);

        var decision11option1 = new StoryEvent(Guid.NewGuid(), "Du hast keine Lust auf zusätzliche Arbeit und legst deshalb einfach während dem Telefonat auf.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision11option2 = new StoryEvent(Guid.NewGuid(), "Nach dem Telefonat setzt du die gewünschten Änderungen um.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision11option3 = new StoryEvent(Guid.NewGuid(), "Nach dem Telefonat fällt dir auf, dass die Änderungswünsche technisch nicht umsetzbar sind.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision11.AddChild(decision11option1);
        decision11.AddChild(decision11option2);
        decision11.AddChild(decision11option3);

        var storyelement28option1 = new StoryEvent(Guid.NewGuid(), "Du hast aufgelegt.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-3, 0, 0, 0));
        var storyelement28option2 = new StoryEvent(Guid.NewGuid(), "Du hast die gewünschten Änderungen umgesetzt.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 0, 2));
        var decision12 = new StoryEvent(Guid.NewGuid(), "Wie willst du weiter vorgehen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        decision11option1.AddChild(storyelement28option1);
        decision11option2.AddChild(storyelement28option2);
        decision11option3.AddChild(decision12);

        var decision12option1 = new StoryEvent(Guid.NewGuid(), "Du informierst den Kunden.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision12option2 = new StoryEvent(Guid.NewGuid(), "Die Änderungen werden nicht umgesetzt, ohne den Kunden darüber zu informieren.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision12.AddChild(decision12option1);
        decision12.AddChild(decision12option2);

        var storyelement29option1 = new StoryEvent(Guid.NewGuid(), "Du rufst WizzBook zurück und erklärst, dass die gewünschte Änderung nicht umsetzbar ist.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(1, 0, 0, 0));
        var storyelement29option2 = new StoryEvent(Guid.NewGuid(), "Du hast WizzBook nicht informiert.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-2, 0, 0, 0));

        decision12option1.AddChild(storyelement29option1);
        decision12option2.AddChild(storyelement29option2);

        //bug has surfaced

        var storyelement30 = new StoryEvent(Guid.NewGuid(), "Du hast einen neuen kritischen Bug entdeckt.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement28option1.AddChild(storyelement30);
        storyelement28option2.AddChild(storyelement30);
        storyelement29option1.AddChild(storyelement30);
        storyelement29option2.AddChild(storyelement30);

        var storyelement31 = new StoryEvent(Guid.NewGuid(), "Bis jetzt weißt nur du von dem Bug.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement30.AddChild(storyelement31);

        var decision13 = new StoryEvent(Guid.NewGuid(), "Was machst du?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement31.AddChild(decision13);

        var decision13option1 = new StoryEvent(Guid.NewGuid(), "Du schreibst ein Bug Ticket.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision13option2 = new StoryEvent(Guid.NewGuid(), "Einfach ignorieren, wird schon keinem auffallen.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision13.AddChild(decision13option1);
        decision13.AddChild(decision13option2);

        var storyelement32 = new StoryEvent(Guid.NewGuid(), "Yaggaya sieht dein Ticket und leitet es weiter. Yaggaya lobt deine Arbeitsweise.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption, new Skills(0, 2, 0, 0));

        decision13option1.AddChild(storyelement32);

        //affected by dice roll
        var storyelement33option1 = new StoryEvent(Guid.NewGuid(), "Der Bug wurde gefunden und es kommt raus, dass du den Code mit dem Bug als letztes bearbeitet hast. Yaggaya ist sauer, da dir der Bug hätte auffallen müssen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-3, -3, 0, 0), true, RandomType.RandomDecisionFour);
        var storyelement33option2 = new StoryEvent(Guid.NewGuid(), "Der Bug wurde nicht gefunden.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, -3, 0, 0), false, RandomType.RandomDecisionFour);

        decision13option2.AddChild(storyelement33option1);
        decision13option2.AddChild(storyelement33option2);

        //customer wants new feature

        var storyelement34 = new StoryEvent(Guid.NewGuid(), "WizzBook meldet sich nochmals und dieses mal will WizzBook eine neue Funktion zu WizzApp hinzufügen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement32.AddChild(storyelement34);
        storyelement33option1.AddChild(storyelement34);
        storyelement33option2.AddChild(storyelement34);

        var storyelement35 = new StoryEvent(Guid.NewGuid(), "Dir kommt das etwas seltsam vor, da dies Probleme mit anderen Funktionen der App verursachen könnte.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement34.AddChild(storyelement35);

        var decision14 = new StoryEvent(Guid.NewGuid(), "Was machst du?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement35.AddChild(decision14);

        var decision14option1 = new StoryEvent(Guid.NewGuid(), "Der Kunde wird schon wissen was er da tut.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision14option2 = new StoryEvent(Guid.NewGuid(), "Du versuchst herauszufinden ob du den Kunden richtig verstanden hast und ob er unbedingt das neue Feature haben möchte.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision14.AddChild(decision14option1);
        decision14.AddChild(decision14option2);

        var storyelement36option1 = new StoryEvent(Guid.NewGuid(), "Du setzt die neue Funktion alleine um.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-2, 0, 0, 2));
        var storyelement36option2 = new StoryEvent(Guid.NewGuid(), "Du sprichst mit dem Kunden über die neue Funktion.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(2, 0, 0, 0));

        decision14option1.AddChild(storyelement36option1);
        decision14option2.AddChild(storyelement36option2);

        var storyelement37 = new StoryEvent(Guid.NewGuid(), "Der Kunde hat sich dazu entschieden mit der neuen Funktion den Änderungsprozess zu druchlaufen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement36option2.AddChild(storyelement37);

        var decision15 = new StoryEvent(Guid.NewGuid(), "Wie gehst du vor?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement37.AddChild(decision15);

        var decision15option1 = new StoryEvent(Guid.NewGuid(), "Du entscheidest dich die Änderung mit dem Kunden alleine umzusetzten.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision15option2 = new StoryEvent(Guid.NewGuid(), "Du organisierst ein Meeting.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision15.AddChild(decision15option1);
        decision15.AddChild(decision15option2);

        var storyelement38option1 = new StoryEvent(Guid.NewGuid(), "Du hast die neue Funktion alleine umgesetzt.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-2, 0, 0, 2));
        var storyelement38option2 = new StoryEvent(Guid.NewGuid(), "Dein Chef is sehr zufrieden mit deiner Herangehensweise.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(1, 2, 0, 0));

        decision15option1.AddChild(storyelement38option1);
        decision15option2.AddChild(storyelement38option2);

        var storyelement39 = new StoryEvent(Guid.NewGuid(), "Die neue Funktion wurde implementiert.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement36option1.AddChild(storyelement39);
        storyelement38option1.AddChild(storyelement39);
        storyelement38option2.AddChild(storyelement39);

        // colleague needs help

        var storyelement40 = new StoryEvent(Guid.NewGuid(), "Dein Kollege Trummu kommt auf dich zu, da er dringend deine Hilfe benötigt.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement39.AddChild(storyelement40);

        var decision16 = new StoryEvent(Guid.NewGuid(), "Willst du Trummu helfen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement40.AddChild(decision16);

        var decision16option1 = new StoryEvent(Guid.NewGuid(), "Du hast gerade viel zu tun und kümmerst dich lieber um deine eigene Arbeit.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision16option2 = new StoryEvent(Guid.NewGuid(), "Du hilfst Trummu auch wenn du dadurch vielleicht deine Deadline verpasst.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision16.AddChild(decision16option1);
        decision16.AddChild(decision16option2);

        //dice roll
        var storyelement41option1 = new StoryEvent(Guid.NewGuid(), "Trummu ist zufrieden und du schafft es deine Deadline einzuhalten.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(2, 0, 0, 0), true, RandomType.RandomDecisionFive);
        var storyelement41option2 = new StoryEvent(Guid.NewGuid(), "Trummu ist zufrieden, aber du wirst es nicht schaffen deine Deadline zu halten.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(2, 0, 0, 0), false, RandomType.RandomDecisionFive);

        decision16option2.AddChild(storyelement41option1);
        decision16option2.AddChild(storyelement41option2);

        var decision17 = new StoryEvent(Guid.NewGuid(), "Frägst du um Hilfe, um deine Deadline noch halten zu können?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement41option2.AddChild(decision17);

        var decision17option1 = new StoryEvent(Guid.NewGuid(), "Ja, du fragst Yaggaya um Hilfe.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision17option2 = new StoryEvent(Guid.NewGuid(), "Nein.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision17.AddChild(decision17option1);
        decision17.AddChild(decision17option2);

        //high communication skills required
        var storyelement42option1 = new StoryEvent(Guid.NewGuid(), "Yaggaya hilft dir und du schaffst es deine Deadline einzuhalten.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, true, RandomType.RandomDecisionSix);

        decision17option1.AddChild(storyelement42option1);

        var storyelement42option2 = new StoryEvent(Guid.NewGuid(), "Yaggaya wird dir nicht helfen und du verpasst deine Deadline.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, -2, 0, 0), false, RandomType.RandomDecisionSix);

        decision17option1.AddChild(storyelement42option2);

        var storyelement43 = new StoryEvent(Guid.NewGuid(), "Du verpasst deine Deadline.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, -2, 0, 0));

        decision17option2.AddChild(storyelement43);

        var storyelement44 = new StoryEvent(Guid.NewGuid(), "Trummu ist verärgert, da du im nicht geholfen hast.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-2, 0, 0, 0));

        decision16option1.AddChild(storyelement44);

        //code review

        var storyelement45 = new StoryEvent(Guid.NewGuid(), "Das Projekt ist fast abgeschlossen. Es fehlt nur noch die Code Review.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement41option1.AddChild(storyelement45);
        storyelement44.AddChild(storyelement45);
        storyelement42option1.AddChild(storyelement45);
        storyelement42option2.AddChild(storyelement45);
        storyelement43.AddChild(storyelement45);

        var decision18 = new StoryEvent(Guid.NewGuid(), "Willst du an der Code Review teilnehmen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement45.AddChild(decision18);

        var decision18option1 = new StoryEvent(Guid.NewGuid(), "Du nimmst an der Code Review teil.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        var decision18option2 = new StoryEvent(Guid.NewGuid(), "Alles läuft gut, wird schon passen, eine Review ist nicht notwendig.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision18.AddChild(decision18option1);
        decision18.AddChild(decision18option2);

        var storyelement46option1 = new StoryEvent(Guid.NewGuid(), "Du nimmst an der Code Review teil und erhälst Tipps die später mal sehr hilfreich sein werden.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(2, 2, 0, 0));
        var storyelement46option2 = new StoryEvent(Guid.NewGuid(), "Dein Chef ist ziemlich sauer, da eine Code Review sehr wichtig ist.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-2, -2, 0, 0));

        decision18option1.AddChild(storyelement46option1);
        decision18option2.AddChild(storyelement46option2);

        var storyelement47 = new StoryEvent(Guid.NewGuid(), "Das Projekt ist abgeschlossen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement46option1.AddChild(storyelement47);
        storyelement46option2.AddChild(storyelement47);

        //workshop

        var background3 = new StoryEvent(BackgroundType.Beach, new HashSet<StoryEvent>(), StoryEventType.StoryBackground);

        storyelement47.AddChild(background3);

        var storyelement48 = new StoryEvent(Guid.NewGuid(), "Die besten Mitarbeiter werden zu einem Workshop auf Hawaii eingeladen.", new HashSet<StoryEvent>(), StoryEventType.StoryWorkshop);

        background3.AddChild(storyelement48);

        //sufficient amount of skill points required
        var storyelement49option1 = new StoryEvent(Guid.NewGuid(), "Glückwunsch du wurdest zum Workshop eingeladen.", new HashSet<StoryEvent>(), StoryEventType.StoryWorkshopInvite);
        var storyelement49option2 = new StoryEvent(Guid.NewGuid(), "Leider warst du nicht gut genug, vielleicht wird dein nächstes Projekt erfolgreicher.", new HashSet<StoryEvent>(), StoryEventType.StoryWorkshopNoInvite);
        var storyelement49option3 = new StoryEvent(Guid.NewGuid(), "Leider musst du die Firma verlassen.", new HashSet<StoryEvent>(), StoryEventType.StoryFired);

        storyelement48.AddChild(storyelement49option1);
        storyelement48.AddChild(storyelement49option2);
        storyelement48.AddChild(storyelement49option3);

        var end = new StoryEvent(Guid.NewGuid(), "End.", new HashSet<StoryEvent>(), StoryEventType.StoryEnd);

        storyelement49option1.AddChild(end);
        storyelement49option2.AddChild(end);
        storyelement49option3.AddChild(end);

        this.Character = null;
        this.CurrentEvent = root;
        this.Root = root;

        // Sets the storyGraph with all the StoryEvents.
        Debug.Log("StoryGraph initialized.");
    }

    /// <summary>
    /// Constructor !only! for test purpose.
    /// </summary>
    /// <param name="character">The character.</param>
    /// <param name="root">The root event.</param>
    /// <param name="currentEvent">The current event.</param>
    public StoryGraph(Character character, StoryEvent root, StoryEvent currentEvent)
    {
        Character = character;
        Root = root;
        CurrentEvent = currentEvent;
    }

    /// <summary>
    /// Method to set a new current StoryEvent
    /// </summary>
    /// <param name="newCurrentEvent">The new current StoryEvent.</param>
    public void SetCurrentEvent(StoryEvent newCurrentEvent)
    {
        CurrentEvent = newCurrentEvent;
    }

    /// <summary>
    /// Method that is called on a RandomEvent after a StoryFlowDecision.
    /// The method determines the next StoryEvent based on the respective formula, the method also starts the dice animation.
    /// </summary>
    /// <param name="displayStatusBar">The statusBar to start and display the dice animation.</param>
    /// <returns>The next StoryEvent with which the game continues.</returns>
    public StoryEvent GetRandomOption()
    {
        var diceRoll = new Random();
        var rollTheDice = diceRoll.Next(1, 6);
        var children = CurrentEvent.Children.ToList();

        switch (CurrentEvent.Children.First().Random)
        {
            case RandomType.RandomDecisionOne:
                var randomEventOne = rollTheDice + Character.Abilities.Programming + 1 > 8;
                return randomEventOne == children[0].RandomOption ? children[0] : children[1];
            case RandomType.RandomDecisionTwo:
                var randomEventTwo = rollTheDice + Character.Abilities.Programming - 1 > 8;
                return randomEventTwo == children[0].RandomOption ? children[0] : children[1];
            case RandomType.RandomDecisionThree:
                var randomEventThree = rollTheDice + Character.Abilities.Partying > 8;
                return randomEventThree == children[0].RandomOption ? children[0] : children[1];
            case RandomType.RandomDecisionFour:
                var randomEventFour = rollTheDice > 3;
                return randomEventFour == children[0].RandomOption ? children[0] : children[1];
            case RandomType.RandomDecisionFive:
                var randomEventFive = rollTheDice <= 3;
                return randomEventFive == children[0].RandomOption ? children[0] : children[1];
            case RandomType.RandomDecisionSix:
                var randomEventSix = Character.Abilities.Communication > 6;
                return randomEventSix == children[0].RandomOption ? children[0] : children[1];
            default:
                return CurrentEvent.Children.First();
        }
    }
}
