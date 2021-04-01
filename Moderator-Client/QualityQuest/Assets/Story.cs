using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Story:MonoBehaviour
{

    public static Story current;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
    }


    public static StoryGraph playThrough;
    

    public static void InitializeStoryGraph()
    {

        StoryEvent root = new StoryEvent(Guid.NewGuid(), "Mit welchem Charakter m�chtest du das Spiel spielen", new HashSet<StoryEvent>(),StoryEventType.StoryDecision);

        StoryEvent character1 = new StoryEvent(Guid.NewGuid(), "Noruso \n Programming: 1 \n Analytics: 4 \n Communication: 3 \n Partying: 2", new HashSet<StoryEvent>(),StoryEventType.StoryDecisionOption);
        StoryEvent character2 = new StoryEvent(Guid.NewGuid(), "Lumati \n Programming: 4 \n Analytics: 3 \n Communication: 1 \n Partying: 0", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent character3 = new StoryEvent(Guid.NewGuid(), "Turgal \n Programming: 2 \n Analytics: 2 \n Communication: 2 \n Partying: 2", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent character4 = new StoryEvent(Guid.NewGuid(), "Kirogh \n Programming: 1 \n Analytics: 0 \n Communication: 2 \n Partying: 5", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);


        root.AddChild(character1);
        root.AddChild(character2);
        root.AddChild(character3);
        root.AddChild(character4);

        StoryEvent storyelement1 = new StoryEvent(Guid.NewGuid(), "Dir fehlt noch ein Anwendungsfach am Ende deines Studiums", new HashSet<StoryEvent>(),StoryEventType.StoryFlow);

        character1.AddChild(storyelement1);
        character2.AddChild(storyelement1);
        character3.AddChild(storyelement1);
        character4.AddChild(storyelement1);

        StoryEvent decision2 = new StoryEvent(Guid.NewGuid(), "Welches Anwednungsfach m�chtest du ausw�hlen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement1.AddChild(decision2);

        StoryEvent decision2option1 = new StoryEvent(Guid.NewGuid(), "Einf�hrung in Bali", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision2option2 = new StoryEvent(Guid.NewGuid(), "Anforderungsanalyse", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision2option3 = new StoryEvent(Guid.NewGuid(), "Einf�hrung in die Softwarearchitektur", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision2option4 = new StoryEvent(Guid.NewGuid(), "Usability als Erfolgsfaktor", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision2.AddChild(decision2option1);
        decision2.AddChild(decision2option2);
        decision2.AddChild(decision2option3);
        decision2.AddChild(decision2option4);

        StoryEvent storyelemnt2option1 = new StoryEvent(Guid.NewGuid(), "Du hast dein Anwendungsfach erfolgreich bestanden", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 1, 0, 2));
        StoryEvent storyelemnt2option2 = new StoryEvent(Guid.NewGuid(), "Du hast dein Anwendungsfach erfolgreich bestanden", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 2, 0, 1));
        StoryEvent storyelemnt2option3 = new StoryEvent(Guid.NewGuid(), "Du hast dein Anwendungsfach erfolgreich bestanden", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 3, 0, 0));
        StoryEvent storyelemnt2option4 = new StoryEvent(Guid.NewGuid(), "Du hast dein Anwendungsfach erfolgreich bestanden", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(2, 1, 0, 0));

        decision2option1.AddChild(storyelemnt2option1);
        decision2option2.AddChild(storyelemnt2option2);
        decision2option3.AddChild(storyelemnt2option3);
        decision2option4.AddChild(storyelemnt2option4);

        StoryEvent storyelement3 = new StoryEvent(Guid.NewGuid(), "Nach dem du dein Anwendungsfach bestanden hast kannst du deine Freizeit genie�en oder an einer au�erschulischen Aktivit�t teilnehemn.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelemnt2option1.AddChild(storyelement3);
        storyelemnt2option2.AddChild(storyelement3);
        storyelemnt2option3.AddChild(storyelement3);
        storyelemnt2option4.AddChild(storyelement3);

        StoryEvent decision3 = new StoryEvent(Guid.NewGuid(), "Was willst du tun?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement3.AddChild(decision3);

        StoryEvent decision3option1 = new StoryEvent(Guid.NewGuid(), "Du nimmst an einem Hackathon an der Universit�t teil.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision3option2 = new StoryEvent(Guid.NewGuid(), "Du nutzt deine Zeit um eine dir nicht bekannte Programmiersprache zu lernen", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision3option3 = new StoryEvent(Guid.NewGuid(), "Du genie�t deine Freizeit und machst Party", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision3.AddChild(decision3option1);
        decision3.AddChild(decision3option2);
        decision3.AddChild(decision3option3);

        StoryEvent storyelement4option1 = new StoryEvent(Guid.NewGuid(), "Du lernst viele neue Leute kennen und erweiterst deine Programmierkenntnisse.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(1, 0, 0, 2));
        StoryEvent storyelement4option2 = new StoryEvent(Guid.NewGuid(), "Du lernst die Programmiersprache CSharp kennen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 1, 0, 2));
        StoryEvent storyelement4option3 = new StoryEvent(Guid.NewGuid(), "Du genie�t das Leben und machst Party, aber daf�r rosten deine Programmierkenntnisse etwas ein.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 4, -1));

        decision3option1.AddChild(storyelement4option1);
        decision3option2.AddChild(storyelement4option2);
        decision3option3.AddChild(storyelement4option3);

        StoryEvent storyelement5 = new StoryEvent(Guid.NewGuid(), "Du hast dein Studium erfolgreich abgeschlossen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement4option1.AddChild(storyelement5);
        storyelement4option2.AddChild(storyelement5);
        storyelement4option3.AddChild(storyelement5);

        StoryEvent storyelement6 = new StoryEvent(Guid.NewGuid(), "... ein halbes Jahr vergeht ...", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement5.AddChild(storyelement6);

        StoryEvent storyelement7 = new StoryEvent(Guid.NewGuid(), "Du hast einen Job bei NewTec GmbH angenommen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement6.AddChild(storyelement7);
        
        StoryEvent storyelement8 = new StoryEvent(Guid.NewGuid(), "Dein Chef hat einen neuen Auftrag von WizzBook an Land gezogen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement7.AddChild(storyelement8);

        StoryEvent storyelement9 = new StoryEvent(Guid.NewGuid(), "Deine Aufgabe ist es das User Interface zu designen und zu implementieren.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
       
        storyelement8.AddChild(storyelement9);

        StoryEvent decision4 = new StoryEvent(Guid.NewGuid(), "Wie willst du mit deiner Aufgabe anfangen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement9.AddChild(decision4);

        StoryEvent decision4option1 = new StoryEvent(Guid.NewGuid(), "Du fr�gst deinen Mentor.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision4option2 = new StoryEvent(Guid.NewGuid(), "Du suchst nach einer L�sung auf HeapOverflow.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision4option3 = new StoryEvent(Guid.NewGuid(), "Du f�ngst einfach mit der Arbeit an.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision4.AddChild(decision4option1);
        decision4.AddChild(decision4option2);
        decision4.AddChild(decision4option3);

        StoryEvent storyelement10option1 = new StoryEvent(Guid.NewGuid(), "Du lernst deinen Mentor Yaggaya kennen. Yaggaya hilft dir bei deiner Aufgabe.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow,new Skills(1,0,0,1));
        StoryEvent storyelement10option2 = new StoryEvent(Guid.NewGuid(), "Du l�st das problem, verschwendest aber eine Menge Zeit.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow,new Skills(0,0,0,1));
        StoryEvent storyelement10option3 = new StoryEvent(Guid.NewGuid(), "Du verschwendest eine Menge Zeit ohne das Problem zu l�sen. Du ben�tigst f�r deine Aufgabe einen extra Tag.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0,-1,0,0));

        decision4option1.AddChild(storyelement10option1);
        decision4option2.AddChild(storyelement10option2);
        decision4option3.AddChild(storyelement10option3);

        StoryEvent storyelement11 = new StoryEvent(Guid.NewGuid(), "Nach deiner ersten Aufgabe musst du nun die dazugeh�rigen Tests implementieren.",new HashSet<StoryEvent>(),StoryEventType.StoryFlow);

        storyelement10option1.AddChild(storyelement11);
        storyelement10option2.AddChild(storyelement11);
        storyelement10option3.AddChild(storyelement11);

        StoryEvent storyelement12 = new StoryEvent(Guid.NewGuid(), "Du hast nur wenige Tage Zeit die Tests zu implementieren, da die Deadline des Projekts sicher immer weiter n�hert.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement11.AddChild(storyelement12);

        StoryEvent decision5 = new StoryEvent(Guid.NewGuid(), "Wie willst du vorgehen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement12.AddChild(decision5);

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        StoryEvent decision5option1 = new StoryEvent(Guid.NewGuid(), "Du schreibst umfangreichge Tests bis sie fertig sind ohne auf die Deadline zu schauen", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision5option2 = new StoryEvent(Guid.NewGuid(), "Du schreibst Tests bis die Deadline erreicht ist, was du bis dahin nicht schaffst bleibt ungetestet.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        
        decision5.AddChild(decision5option1);
        decision5.AddChild(decision5option2);

        StoryEvent storyelement11option1 = new StoryEvent(Guid.NewGuid(), "Durch deine guten Programmierkenntnisse kannst du trotzdem die Deadline einhalten.", new HashSet<StoryEvent>(),StoryEventType.StoryFlow, true);
        StoryEvent storyelement11option2 = new StoryEvent(Guid.NewGuid(), "Leider wirst du nicht rechtzeitig fertig aber du hast sehr umfangreiche Tests geschrieben.", new HashSet<StoryEvent>(),StoryEventType.StoryFlow, false);

        decision5option1.AddChild(storyelement11option1);
        decision5option1.AddChild(storyelement11option2);

        StoryEvent storyelement11option3 = new StoryEvent(Guid.NewGuid(), "Durch deine guten Programmierkenntnisse hast du es trotz der kurzen Zeit geschafft umfangreiche Tests zu schreiben", new HashSet<StoryEvent>(),StoryEventType.StoryFlow, true);
        StoryEvent storyelement11option4 = new StoryEvent(Guid.NewGuid(), "Die Deadline wurde erreicht aber du hast es nicht geschafft alles umfangreich zu testen.", new HashSet<StoryEvent>(),StoryEventType.StoryFlow, false);

        decision5option2.AddChild(storyelement11option3);
        decision5option2.AddChild(storyelement11option4);

        StoryEvent storyelement12option1 = new StoryEvent(Guid.NewGuid(), "Yaggaya ist sehr zufrieden mit deiner herangehensweise und deinen Tests.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 3, 0, 1));
        StoryEvent storyelement12option2 = new StoryEvent(Guid.NewGuid(), "Yaggaya ist sehr zufrieden mit deiner herangehensweise und deinen Tests, obwohl du nicht rechtzeitig fertig geworden bist.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 2, 0, 2));

        storyelement11option1.AddChild(storyelement12option1);
        storyelement11option2.AddChild(storyelement12option2);

        StoryEvent storyelement12option3 = new StoryEvent(Guid.NewGuid(), "Yaggaya ist sehr zufrieden mit deiner Leistung.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 0, 2));
        StoryEvent storyelement12option4 = new StoryEvent(Guid.NewGuid(), "Yaggaya ist nicht zufrieden mit deiner Leistung. Du musst die Tests nochmal �berarbeiten und �berschreitest die Deadline um ein paar Tage.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, -2, 0, 1));

        storyelement11option3.AddChild(storyelement12option3);
        storyelement11option4.AddChild(storyelement12option4);

        StoryEvent storyelement13 = new StoryEvent(Guid.NewGuid(), "Das schreiben der Tests ist abgeschlossen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement12option1.AddChild(storyelement13);
        storyelement12option2.AddChild(storyelement13);
        storyelement12option3.AddChild(storyelement13);
        storyelement12option4.AddChild(storyelement13);

        // company party

        StoryEvent storyelement14 = new StoryEvent(Guid.NewGuid(), "Es steht eine Firmenfeier an.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement13.AddChild(storyelement14);

        StoryEvent storyelement15 = new StoryEvent(Guid.NewGuid(), "Die Tests haben ein paar Fehler aufgedeckt, wenn du sie behebst kannst du nicht zu Firmenfeier.",new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement14.AddChild(storyelement15);
        
        StoryEvent decision6 = new StoryEvent(Guid.NewGuid(), "Gehst du zur Firmenparty?",new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement15.AddChild(decision6);

        StoryEvent decision6option1 = new StoryEvent(Guid.NewGuid(), "Du gehst zu Firmenfeier.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision6option2 = new StoryEvent(Guid.NewGuid(), "Du arbeitest am Projekt weiter.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision6.AddChild(decision6option1);
        decision6.AddChild(decision6option2);

        StoryEvent storyelement16 = new StoryEvent(Guid.NewGuid(), "Du arbeitest weiter am Projekt und behebst die Fehler.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 0, 2));

        decision6option2.AddChild(storyelement16);

        // at the company party

        StoryEvent storyelement18 = new StoryEvent(Guid.NewGuid(), "Du bist auf der Firmenfeier", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        decision6option1.AddChild(storyelement18);

        StoryEvent decision7 = new StoryEvent(Guid.NewGuid(), "Was willst du auf der Firmenfeier machen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement18.AddChild(decision7);

        StoryEvent decision7option1 = new StoryEvent(Guid.NewGuid(), "Du plauderst mit Kollegen.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision7option2 = new StoryEvent(Guid.NewGuid(), "Du redest mit deinem Mentor und deinem Chef.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision7option3 = new StoryEvent(Guid.NewGuid(), "Du suchst dir alleine einen Sitzplatz um etwas zu essen.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision7.AddChild(decision7option1);
        decision7.AddChild(decision7option2);
        decision7.AddChild(decision7option3);

        StoryEvent storyelement19option1 = new StoryEvent(Guid.NewGuid(), "Du bekommst von Kollegen ein paar Tipps.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 0, 2));

        // how to influences later decisions ???
        StoryEvent storyelement19option2 = new StoryEvent(Guid.NewGuid(), "Es wurden zus�tzliche Dialogoptionen f�r sp�tere Entscheidungen freigeschaltet ", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        decision7option1.AddChild(storyelement19option1);
        decision7option2.AddChild(storyelement19option2);

        StoryEvent storyelement20 = new StoryEvent(Guid.NewGuid(), "Du hast durst und holst dir ein Getr�nk.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement19option1.AddChild(storyelement20);
        storyelement19option2.AddChild(storyelement20);
        decision7option3.AddChild(storyelement20);

        StoryEvent decision8 = new StoryEvent(Guid.NewGuid(), "Was m�chtest du trinken?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement20.AddChild(decision8);

        StoryEvent decision8option1 = new StoryEvent(Guid.NewGuid(), "Ein Bier reicht f�r den Abend.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision8option2 = new StoryEvent(Guid.NewGuid(), "Eine Cola tuts auch.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision8option3 = new StoryEvent(Guid.NewGuid(), "Ein paar Shots sind ok, es kostet ja nichts.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision8.AddChild(decision8option1);
        decision8.AddChild(decision8option2);
        decision8.AddChild(decision8option3);

        StoryEvent storyelement21option1 = new StoryEvent(Guid.NewGuid(), "Du hast die Firmenfeier gut �berstanden", new HashSet<StoryEvent>(), StoryEventType.StoryFlow,new Skills(0,0,1,0));

        decision8option1.AddChild(storyelement21option1);
        decision8option2.AddChild(storyelement21option1);

        StoryEvent storyelement21option2 = new StoryEvent(Guid.NewGuid(), "Du trinkst zwei Shots.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        decision8option3.AddChild(storyelement21option2);

        StoryEvent decision9 = new StoryEvent(Guid.NewGuid(), "Willst du noch mehr Shots trinken?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement21option2.AddChild(decision9);

        StoryEvent decision9option1 = new StoryEvent(Guid.NewGuid(), "Mehr Shots trinken.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision9option2 = new StoryEvent(Guid.NewGuid(), "Nein, zwei sind genug.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision9.AddChild(decision9option1);
        decision9.AddChild(decision9option2);

        StoryEvent storyelement22option1 = new StoryEvent(Guid.NewGuid(), "Du sorgst f�r gute Stimmung auf der Party.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow,new Skills(0,0,2,0));


        // affected by dice roll
        StoryEvent storyelement22option2 = new StoryEvent(Guid.NewGuid(), "Du �berstehst die Firmenfeier ohne f�r Aufsehen zu sorgen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow,new Skills(0,0,3,0), true);
        StoryEvent storyelement22option3 = new StoryEvent(Guid.NewGuid(), "Du sorgst f�r gute Stimmung aber f�llst negativ auf, weil du ziemlich stark betrunken bist.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow,new Skills(-2,0,3,0), false);

        decision9option2.AddChild(storyelement22option1);
        decision9option2.AddChild(storyelement22option2);
        decision9option2.AddChild(storyelement22option3);

        StoryEvent storyelement23 = new StoryEvent(Guid.NewGuid(), "Die Firmenfeier ist zu Ende und du gehts nach Hause.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement22option1.AddChild(storyelement23);
        storyelement22option2.AddChild(storyelement23);
        storyelement22option3.AddChild(storyelement23);

        // training course

        StoryEvent storyelement17 = new StoryEvent(Guid.NewGuid(), "Eine Weiterbildung steht an.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement16.AddChild(storyelement17);
        storyelement23.AddChild(storyelement17);

        // Deadline should be mentioned here as well
        StoryEvent decision10 = new StoryEvent(Guid.NewGuid(), "Willst du an der Weiterbildung teilnehmen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement17.AddChild(decision10);

        StoryEvent decision10option1 = new StoryEvent(Guid.NewGuid(), "Klar, warum nicht.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision10option2 = new StoryEvent(Guid.NewGuid(), "Du kennst das Thema schon von der Uni und arbeitst lieber an dem Projekt weiter.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision10option3 = new StoryEvent(Guid.NewGuid(), "Nein, du brauchst erstmal Urlaub", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        // only if dialog option was ulocked previously
        StoryEvent decision10option4 = new StoryEvent(Guid.NewGuid(), "Dein Chef bietet dir an nach der Weiterbildung Urlaub zu machen.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision10.AddChild(decision10option1);
        decision10.AddChild(decision10option2);
        decision10.AddChild(decision10option3);
        decision10.AddChild(decision10option4);

        StoryEvent storyelement24option1 = new StoryEvent(Guid.NewGuid(), "Du kannst deine Deadline nicht einhalten aber deinem Chef gef�llt dein Engagement deine F�higkeiten zu erweitern.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0,0,0,1));
        StoryEvent storyelement24option2 = new StoryEvent(Guid.NewGuid(), "Du arbeitest an dem Projekt weiter und schaffst es deine Deadline einzuhalten.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 0, 0, 0));
        StoryEvent storyelement24option3 = new StoryEvent(Guid.NewGuid(), "Da du Urlaub genommen hast schaffst du es nicht die Deadline einzuhalten, dein Chef ist deshalb ziemlich sauer.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-2, -2, 0, 0));
        StoryEvent storyelement24option4 = new StoryEvent(Guid.NewGuid(), "Nach der Weiterbildung genie�t du eine Woche Urlaub und da dein Chef sich um eine Vertetung gek�mmert hat, hast du keine Probleme die Deadline einzuhalten.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, 2, 0, 2));

        decision10option1.AddChild(storyelement24option1);
        decision10option2.AddChild(storyelement24option2);
        decision10option3.AddChild(storyelement24option3);
        decision10option4.AddChild(storyelement24option4);

        StoryEvent storyelement25 = new StoryEvent(Guid.NewGuid(), "Deine Telefon klingelt. WizzBook ruft an und m�chte �nderungen an der WizzApp vornehmen.",new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement24option1.AddChild(storyelement25);
        storyelement24option2.AddChild(storyelement25);
        storyelement24option3.AddChild(storyelement25);
        storyelement24option4.AddChild(storyelement25);

        StoryEvent storyelement26 = new StoryEvent(Guid.NewGuid(), "Du nimmst den Anruf entgegen und h�rst dir WizzApps �nderungsw�nsche an.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement25.AddChild(storyelement26);

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        StoryEvent storyelement27 = new StoryEvent(Guid.NewGuid(), "Wizzbook will das du die �nderungen umsetzt.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement26.AddChild(storyelement27);

        StoryEvent decision11 = new StoryEvent(Guid.NewGuid(), "Was machst du?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement27.AddChild(decision11);

        StoryEvent decision11option1 = new StoryEvent(Guid.NewGuid(), "Du hast keine Lust auf zus�tzliche Arbeit und legst deshalb einfach w�hrend dem Telefonat auf.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision11option2 = new StoryEvent(Guid.NewGuid(), "Nach dem Telefonat setzt du die gew�nschten �nderungen um.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision11option3 = new StoryEvent(Guid.NewGuid(), "Nach dem Telefonat f�llt dir auf, dass die �nderungsw�nsche technisch nicht umsetzbar sind.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision11.AddChild(decision11option1);
        decision11.AddChild(decision11option2);
        decision11.AddChild(decision11option3);

        StoryEvent storyelement28option1 = new StoryEvent(Guid.NewGuid(), "Du hast aufgelegt.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption, new Skills(-3, 0, 0, 0));
        StoryEvent storyelement28option2 = new StoryEvent(Guid.NewGuid(), "Du hast die gew�nschten �nderungen umgesetzt.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption, new Skills(0, 0, 0, 2));
        StoryEvent decision12 = new StoryEvent(Guid.NewGuid(), "Wie willst du weiter vorgehen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        decision11option1.AddChild(storyelement28option1);
        decision11option2.AddChild(storyelement28option2);
        decision11option3.AddChild(decision12);

        StoryEvent decision12option1 = new StoryEvent(Guid.NewGuid(), "Du informierst den Kunden.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision12option2 = new StoryEvent(Guid.NewGuid(), "Du setzt die �nderungen nicht um ohne den Kunden dar�ber zu informieren.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision12.AddChild(decision12option1);
        decision12.AddChild(decision12option2);

        StoryEvent storyelement29option1 = new StoryEvent(Guid.NewGuid(), "Du rufst WizzBook zur�ck und erkl�rst, dass die gew�nschte �nderung nicht umsetzbar ist.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(1, 0, 0, 0));
        StoryEvent storyelement29option2 = new StoryEvent(Guid.NewGuid(), "Du hast WizzBook nicht informiert.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-2, 0, 0, 0));

        decision12option1.AddChild(storyelement29option1);
        decision12option2.AddChild(storyelement29option2);

        //bug has surfaced

        StoryEvent storyelement30 = new StoryEvent(Guid.NewGuid(), "Du hast einen neuen kritischen Bug entdeckt.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement28option1.AddChild(storyelement30);
        storyelement28option2.AddChild(storyelement30);
        storyelement29option1.AddChild(storyelement30);
        storyelement29option2.AddChild(storyelement30);

        StoryEvent storyelement31 = new StoryEvent(Guid.NewGuid(), "Bis jetzt wei�t nur du von dem Bug.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement30.AddChild(storyelement31);

        StoryEvent decision13 = new StoryEvent(Guid.NewGuid(), "Was machst du?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement31.AddChild(decision13);

        StoryEvent decision13option1 = new StoryEvent(Guid.NewGuid(), "Du schreibst ein Bug Ticket.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision13option2 = new StoryEvent(Guid.NewGuid(), "Einfach ingorieren wird schon keinem auffallen.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision13.AddChild(decision13option1);
        decision13.AddChild(decision13option2);

        StoryEvent storyelement32 = new StoryEvent(Guid.NewGuid(), "Yaggaya sieht dein Ticket und leitet es weiter. Yaggaya lobt deine Arbeitsweise.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption, new Skills(0, 2, 0, 0));

        decision13option1.AddChild(storyelement32);

        //affected by dice roll
        StoryEvent storyelement33option1 = new StoryEvent(Guid.NewGuid(), "Der Bug wurde gefunden und es kommt raus, dass du den Code mit dem Bug als letztes bearbeitet hast. Yaggaya ist sauer, da dir der Bug h�tte auffallen m�ssen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-3, -3, 0, 0), true);
        StoryEvent storyelement33option2 = new StoryEvent(Guid.NewGuid(), "Der Bug wurde nicht gefunden.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, -3, 0, 0), false);

        decision13option2.AddChild(storyelement33option1);
        decision13option2.AddChild(storyelement33option2);

        //customer wants new feature

        StoryEvent storyelement34 = new StoryEvent(Guid.NewGuid(), "WizzBook meldet sich nochmals und dieses mal will WizzBook eine neue Funktion zu WizzApp hinzuf�gen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement32.AddChild(storyelement34);
        storyelement33option1.AddChild(storyelement34);
        storyelement33option2.AddChild(storyelement34);

        StoryEvent storyelement35 = new StoryEvent(Guid.NewGuid(), "Dir kommt das etwas seltsam vor, da dies Probleme mit anderen Funktionen der App verursachen k�nnte.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement34.AddChild(storyelement35);

        StoryEvent decision14 = new StoryEvent(Guid.NewGuid(), "Was machst du?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement35.AddChild(decision14);

        StoryEvent decision14option1 = new StoryEvent(Guid.NewGuid(), "Der Kunde wird schon wissen was er da tut.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision14option2 = new StoryEvent(Guid.NewGuid(), "Du versuchst herauszufinden ob du den Kunden richtig verstanden hast und ob er unbedingt das neue Feature haben m�chte.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision14.AddChild(decision14option1);
        decision14.AddChild(decision14option2);

        StoryEvent storyelement36option1 = new StoryEvent(Guid.NewGuid(), "Du setzt die neue Funktion alleine um.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-2, 0, 0, 2));
        StoryEvent storyelement36option2 = new StoryEvent(Guid.NewGuid(), "Du sprichst mit dem Kunden �ber die neue Funktion.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(2, 0, 0, 0));

        decision14option1.AddChild(storyelement36option1);
        decision14option1.AddChild(storyelement36option2);

        StoryEvent storyelement37= new StoryEvent(Guid.NewGuid(), "Der Kunde hat sich dazu entschieden mit der neuen Funktion den �nderungsprozess zu druchlaufen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement36option2.AddChild(storyelement37);

        StoryEvent decision15= new StoryEvent(Guid.NewGuid(), "Wie gehst du vor?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement37.AddChild(decision15);

        StoryEvent decision15option1 = new StoryEvent(Guid.NewGuid(), "Du entscheidest dich die �nderung mit dem Kunden alleine umzusetzten.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision15option2 = new StoryEvent(Guid.NewGuid(), "Du organisierst ein Meeting.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision15.AddChild(decision15option1);
        decision15.AddChild(decision15option2);

        StoryEvent storyelement38option1 = new StoryEvent(Guid.NewGuid(), "Du hast die neue Funktion alleine umgesetzt.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-2, 0, 0, 2));
        StoryEvent storyelement38option2 = new StoryEvent(Guid.NewGuid(), "Dein Chef is sehr zufrieden mit deiner Herangehensweise.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(1, 2, 0, 0));

        decision15option1.AddChild(storyelement38option1);
        decision15option2.AddChild(storyelement38option2);

        StoryEvent storyelement39 = new StoryEvent(Guid.NewGuid(), "Die neue Funktion wurde implementiert.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement36option1.AddChild(storyelement39);
        storyelement38option1.AddChild(storyelement39);
        storyelement38option2.AddChild(storyelement39);

        // colleague needs help

        StoryEvent storyelement40 = new StoryEvent(Guid.NewGuid(), "Dein Kollege Trummu kommt auf dich zu da er drigend deine Hilfe ben�tigt.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement39.AddChild(storyelement40);

        StoryEvent decision16 = new StoryEvent(Guid.NewGuid(), "Willst du Trummu helfen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement40.AddChild(decision16);

        StoryEvent decision16option1 = new StoryEvent(Guid.NewGuid(), "Du hast gerade viel zu tun und k�mmerst dich lieber um deine eigene Arbeit.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision16option2 = new StoryEvent(Guid.NewGuid(), "Du hilfst Trummu auch wenn du dadurch vielleicht deine Deadline verpasst.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision16.AddChild(decision16option1);
        decision16.AddChild(decision16option2);

        //dice roll
        StoryEvent storyelement41option1 = new StoryEvent(Guid.NewGuid(), "Trummu ist zufrieden und du schafft es deine Deadline einzuhalten.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(2, 0, 0, 0), true);
        StoryEvent storyelement41option2= new StoryEvent(Guid.NewGuid(), "Trummu ist zufrieden aber du wirst es nicht schaffen deine Deadline zu halten.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(2, 0, 0, 0), false);

        decision16option2.AddChild(storyelement41option1);
        decision16option2.AddChild(storyelement41option2);

        StoryEvent decision17= new StoryEvent(Guid.NewGuid(), "Frags du um Hilfe um deine Deadline noch halten zu k�nnen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement41option2.AddChild(decision17);

        StoryEvent decision17option1 = new StoryEvent(Guid.NewGuid(), "Ja, du fragst Yaggaya um Hilfe.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision17option2 = new StoryEvent(Guid.NewGuid(), "Nein.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision17.AddChild(decision17option1);
        decision17.AddChild(decision17option2);

        //high communication skills required
        StoryEvent storyelement42option1 = new StoryEvent(Guid.NewGuid(), "Yaggaya hilft dir und du schafst deine Deadline.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, true);

        decision17option1.AddChild(storyelement42option1);

        StoryEvent storyelement42option2 = new StoryEvent(Guid.NewGuid(), "Yaggaya wird dir nicht helfen und du verpasst deine Deadline.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, -2, 0, 0), false);

        decision17option1.AddChild(storyelement42option2);

        StoryEvent storyelement43 = new StoryEvent(Guid.NewGuid(), "Du verpasst deine Deadline.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(0, -2, 0, 0));

        decision17option2.AddChild(storyelement43);

        StoryEvent storyelement44 = new StoryEvent(Guid.NewGuid(), "Trummu ist ver�rgert das du im nicht geholfen hast.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-2, 0, 0, 0));

        decision16option1.AddChild(storyelement44);

        //code review

        StoryEvent storyelement45 = new StoryEvent(Guid.NewGuid(), "Das Projekt ist fast abgeschlossen. Es fehlt nur noch die Code Review.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement44.AddChild(storyelement45);
        storyelement42option1.AddChild(storyelement45);
        storyelement42option2.AddChild(storyelement45);
        storyelement43.AddChild(storyelement45);

        StoryEvent decision18 = new StoryEvent(Guid.NewGuid(), "Willst du an der Code Review teilnehmen?", new HashSet<StoryEvent>(), StoryEventType.StoryDecision);

        storyelement45.AddChild(decision18);

        StoryEvent decision18option1 = new StoryEvent(Guid.NewGuid(), "Du nimmst an der Code Review teil.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);
        StoryEvent decision18option2 = new StoryEvent(Guid.NewGuid(), "Alles l�uft gut, wird schon passen, eine Review ist nicht notwendig.", new HashSet<StoryEvent>(), StoryEventType.StoryDecisionOption);

        decision18.AddChild(decision18option1);
        decision18.AddChild(decision18option2);

        StoryEvent storyelement46option1 = new StoryEvent(Guid.NewGuid(), "Du nimmst an der Code Review teil und erh�lst Tipps die sp�ter mal sehr hilfreich sein werden.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(2, 2, 0, 0));
        StoryEvent storyelement46option2 = new StoryEvent(Guid.NewGuid(), "Dein Chef findet es garnicht gut das du nicht an der Code Review teilgenommen hast.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow, new Skills(-2, -2, 0, 0));

        decision18option1.AddChild(storyelement46option1);
        decision18option2.AddChild(storyelement46option2);

        StoryEvent storyelement47 = new StoryEvent(Guid.NewGuid(), "Das Projekt ist abgeschlossen", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement46option1.AddChild(storyelement47);
        storyelement46option2.AddChild(storyelement47);

        //workshop

        StoryEvent storyelement48 = new StoryEvent(Guid.NewGuid(), "Die besten Mitarbeiter werden zu einem Workshop auf Hawaii eingeladen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement47.AddChild(storyelement48);

        //sufficient amount of skill points required
        StoryEvent storyelement49option1 = new StoryEvent(Guid.NewGuid(), "Gl�ckwunsch du wurdest zum Workshop eingeladen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        StoryEvent storyelement49option2 = new StoryEvent(Guid.NewGuid(), "Leider warst du nicht gut genug, vielleicht wird dein n�chstes Projekt erfolgreicher.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);
        StoryEvent storyelement49option3 = new StoryEvent(Guid.NewGuid(), "Leider musst du die Firma verlassen.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement48.AddChild(storyelement49option1);
        storyelement48.AddChild(storyelement49option2);
        storyelement48.AddChild(storyelement49option3);

        StoryEvent end = new StoryEvent(Guid.NewGuid(), "Ende.", new HashSet<StoryEvent>(), StoryEventType.StoryFlow);

        storyelement49option1.AddChild(end);
        storyelement49option2.AddChild(end);
        storyelement49option3.AddChild(end);

        playThrough = new StoryGraph(null, root, storyelement1);

        Debug.Log("StoryGraph initialized.");
    }

    public void SetCurrentEvent(StoryEvent e)
    {
        playThrough.setCurrentEvent(e);
        PlayGame();
    }

    public void PlayGame()
    {

        Debug.Log(playThrough.getCurrentEvent().GetStoryType());

        if (playThrough.getCurrentEvent().GetStoryType().Equals(StoryEventType.StoryDecision))
        {
            CharacterSelection.current.ShowDecision(playThrough.getCurrentEvent().GetChildren());
            Debug.Log("StoryDecision: " + playThrough.getCurrentEvent().GetDescription());
        }

        else if (playThrough.getCurrentEvent().GetStoryType().Equals(StoryEventType.StoryDecisionOption))
        {
            if (playThrough.getCurrentEvent().GetChildren().Count() > 0)
            {
                playThrough.setCurrentEvent(playThrough.getCurrentEvent().GetChildren().First());
                Debug.Log("Option: " + playThrough.getCurrentEvent().GetDescription());
                this.PlayGame();
            }
            else
            {
                Debug.Log("Story Event has no Children");
            }
        }

        else if (playThrough.getCurrentEvent().GetStoryType().Equals(StoryEventType.StoryFlow))
        {
            if (playThrough.getCurrentEvent().GetChildren().Count() > 0)
            {
                Debug.Log("StoryFlow: " + playThrough.getCurrentEvent().GetDescription());
                CharacterSelection.current.ShowStoryFlow(playThrough.getCurrentEvent(),playThrough.getCurrentEvent().GetChildren());
            }
            else
            {
                Debug.Log("Story Event has no Children");
            }
        }
    }
}
