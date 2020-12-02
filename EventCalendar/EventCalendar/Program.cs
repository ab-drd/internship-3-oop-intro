using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCalendar
{
    class Program
    {
        static void Main(string[] args)
        {
            var allEvents = new Dictionary<Event, List<Person>>();
            var menuStopCondition = true;
            var actionCounter = 1;

            //TESTNI PODACI
            var newEvent1 = new Event("Mathematics studying session", 4, DateTime.Parse("1/14/2010 14:30"), DateTime.Parse("1/14/2010 15:30"));
            var newEvent2 = new Event("Programming, but with music", 3, DateTime.Parse("1/14/2010 16:00"), DateTime.Parse("1/14/2010 20:00"));

            allEvents[newEvent1] = new List<Person>();
            allEvents[newEvent2] = new List<Person>();

            allEvents[newEvent1].Add(new Person("Mate", "Matic", 562, 4568));
            allEvents[newEvent1].Add(new Person("Ante", "Antic", 56232, 41568));
            allEvents[newEvent1].Add(new Person("Iva", "Ivanic", 12523, 1294));

            allEvents[newEvent2].Add(new Person("Ana", "Anic", 124123, 45743));
            allEvents[newEvent2].Add(new Person("Marija", "Maric", 1234, 7654));
            allEvents[newEvent2].Add(new Person("Iva", "Ivanic", 12523, 1294));


            Console.WriteLine("\nDobrodosli na pocetak programa\n\n======================================");

            //Action menu
            while (menuStopCondition)
            {
                Console.WriteLine(actionCounter + ". akcija\n======================================\n");
                Console.WriteLine("Unesite akciju:\n" +
                    "1 - Dodavanje eventa\n" +
                    "2 - Brisanje eventa\n" +
                    "3 - Edit eventa\n" +
                    "4 - Dodavanje osobe na event\n" +
                    "5 - Uklanjanje osobe s eventa\n" +
                    "6 - Ispis detalja eventa\n" +
                    "0 - izlazak iz aplikacije\n");
                

                var caseSwitch = ValidIntInputAndCheck(0, 6);

                switch (caseSwitch)
                {
                    case 1:
                        AddNewEvent(allEvents);
                        break;

                    case 2:
                        DeleteEvent(allEvents);
                        break;

                    case 3:
                        MultifunctionalSubmenu(allEvents, 1);
                        break;

                    case 4:
                        MultifunctionalSubmenu(allEvents, 2);
                        break;

                    case 5:
                        MultifunctionalSubmenu(allEvents, 3);
                        break;

                    case 6:
                        MultifunctionalSubmenu(allEvents, 4);
                        break;

                    case 0:
                        menuStopCondition = false;
                        break;

                }

                Console.WriteLine("\n======================================");
                actionCounter++;
            }

        }



        static void AddNewEvent(Dictionary<Event, List<Person>> eventList)
        { 
            Console.WriteLine(">>>>>>>>>>>>>>>>>");

            var eventName = ChooseEventName(eventList); //funkcija odabira imena eventa

            var eventTypeNumber = ChooseEventTypeNumber(); //funkcija odabira tipa eventa

            var eventStartTime = ChooseEventStartTime(eventList, false); //funkcija odabira pocetnog vremena eventa

            var eventEndTime = ChooseEventEndTime(eventList, eventStartTime); //funkcija odabira zavrsnog vremena eventa

            var newEvent = new Event(eventName, eventTypeNumber, eventStartTime, eventEndTime);
            //stvaranje novog eventa preko konstruktora klase Event

            eventList[newEvent] = new List<Person>(); //dodavanje novog eventa u Dictionary

            Console.WriteLine("<<<<<<<<<<<<<<<<<\n\n");
        }



        static void DeleteEvent(Dictionary<Event, List<Person>> eventList)
        {
            

            if (eventList.Count == 0)
                Console.WriteLine("\nTrenutno nema upisanih eventova - povratak na menu");
            
            else
            {

                PrintEventsInNumberNameFormat(eventList);
                
                var repeatCondition = true;
                while (repeatCondition)
                {

                    Console.WriteLine("Unesite redni broj eventa koji zelite izbrisati:");

                    var deleteIndex = ValidIntInputAndCheck(1, eventList.Count);
                    var counter = 1;

                    foreach (var value in eventList.Keys)
                    {
                        if (counter == deleteIndex)
                        {
                            
                            Console.WriteLine("\nJeste li sigurni da zelite izbrisati event na rednom broju " +
                                "{0} imena '{1}'?\n" +
                                "1 - Da, izbrisi\n" +
                                "2 - Ne, odvedi me na ponovni unos\n" +
                                "0 - Ne, vrati me u menu", 
                                deleteIndex, value.Name);

                            var whatToDo = ValidIntInputAndCheck(0, 2);

                            switch (whatToDo)
                            {

                                case 1:
                                    eventList.Remove(value);
                                    Console.WriteLine("\nUspjesno izbrisan event - povratak na menu\n");
                                    repeatCondition = false;
                                    break;

                                case 2:
                                    Console.WriteLine("\nPovratak na ponovni unos\n");
                                    break;

                                case 0:
                                    Console.WriteLine("\nPovratak na menu\n");
                                    repeatCondition = false;
                                    break;

                            }

                            break;
                        }

                        counter++;
                    }
                }
            }
        }



        static void MultifunctionalSubmenu(Dictionary<Event, List<Person>> eventList, int editOrAddOrDelete)
        {

            if (eventList.Count == 0)
                Console.WriteLine("\nERROR: Trenutno nema upisanih eventova\n" +
                    "-> povratak na menu");
            
            else
            {

                PrintEventsInNumberNameFormat(eventList);

                var repeatCondition = true;
                while (repeatCondition)
                {
                    Console.WriteLine("");

                    if(editOrAddOrDelete == 1)
                        Console.WriteLine("Unesite redni broj eventa koji zelite izmijeniti:");

                    else if (editOrAddOrDelete == 2)
                        Console.WriteLine("Unesite redni broj eventa kojem zelite dodati osobu:");

                    else if (editOrAddOrDelete == 3)
                        Console.WriteLine("Unesite redni broj eventa kojem zelite izbrisati osobu:");

                    else
                        Console.WriteLine("Unesite redni broj eventa koji zelite ispisati:");



                    var editIndex = ValidIntInputAndCheck(1, eventList.Count);
                    var counter = 1;

                    foreach (var pair in eventList)
                    {
                        if (counter == editIndex)
                        {

                            Console.WriteLine("");

                            if (editOrAddOrDelete == 1)
                                Console.WriteLine("Jeste li sigurni da zelite uredjivati event" +
                                    " na rednom broju {0} imena '{1}'?", editIndex, pair.Key.Name);
                            
                            else if (editOrAddOrDelete == 2)
                                Console.WriteLine("Jeste li sigurni da zelite dodati osobu na event" +
                                    " na rednom broju {0} imena '{1}'?", editIndex, pair.Key.Name);

                            else if (editOrAddOrDelete == 3)
                                Console.WriteLine("Jeste li sigurni da zelite izbrisati osobu s eventa" +
                                    " na rednom broju {0} imena '{1}'?", editIndex, pair.Key.Name);
                            
                            else
                                Console.WriteLine("Jeste li sigurni da zelite ispisati event" +
                                    " na rednom broju {0} imena '{1}'?", editIndex, pair.Key.Name);



                            Console.WriteLine("1 - da\n" +
                                "2 - Ne, odvedi me na ponovni unos\n" +
                                "0 - Ne, vrati me u menu");

                            var whatToDo = ValidIntInputAndCheck(0, 2);

                            switch (whatToDo)
                            {
                                case 1:

                                    if (editOrAddOrDelete == 1)
                                        EditThisEvent(eventList, pair);

                                    else if (editOrAddOrDelete == 2)
                                        AddPersonToThisEvent(eventList, pair);

                                    else if (editOrAddOrDelete == 3)
                                        RemovePersonFromThisEvent(eventList, pair);

                                    else
                                        PrintEventDetails(pair);


                                    repeatCondition = false;
                                    break;

                                case 2:
                                    Console.WriteLine("\nPovratak na ponovni unos\n");
                                    break;

                                case 0:
                                    repeatCondition = false;
                                    break;

                            }

                            break;

                        }
                        counter++;
                    }

                }
            }
        }



        static void EditThisEvent(Dictionary<Event, List<Person>> eventList, 
            KeyValuePair<Event, List<Person>> givenPair)
        {
            Console.WriteLine("\nSto zelite promijeniti u eventu {0}?\n" +
                "1 - Promjena imena\n" +
                "2 - Promjena tipa\n" +
                "3 - Promjena vremena trajanja eventa\n" +
                "0 - Povratak na menu", givenPair.Key.Name);

            var editChoice = ValidIntInputAndCheck(0,3);

            switch(editChoice)
            {
                case 1:
                    EditThisEventsName(eventList, givenPair);
                    break;

                case 2:
                    EditThisEventsType(givenPair);
                    break;

                case 3:
                    EditThisEventsTimes(eventList, givenPair);
                    break;

                case 0:
                    break;

            }
        }



        static void EditThisEventsName(Dictionary<Event, List<Person>> eventList, 
            KeyValuePair<Event, List<Person>> givenPair)
        {

            Console.WriteLine("Unesite novo ime eventa {0}", givenPair.Key.Name);
            
            var stopCondition = true;

            while(stopCondition)
            {
                var newEventName = ValidStringInputAndCheck();


                if (newEventName == givenPair.Key.Name)
                {

                    Console.WriteLine("Upisali ste isto ime - " +
                        "nema promjene\n-> povratak na menu\n");
                    stopCondition = false;

                }

                else
                {

                    var stopArgument = IsNewNameAlreadyInNames(eventList, newEventName);

                    if (!stopArgument)
                    {
                        givenPair.Key.Name = newEventName;
                        stopCondition = false;
                        Console.WriteLine("\nIme eventa uspjesno promjenjeno u {0}\n", newEventName);

                    }
                }
            }
        }



        static void EditThisEventsType(KeyValuePair<Event, List<Person>> givenPair)
        {
            Console.WriteLine("Unesite novi tip eventa {0}\n" +
                "1 - Coffee\n" +
                "2 - Lecture\n" +
                "3 - Concert\n" +
                "4 - StudySession", givenPair.Key.Name);

            var stopCondition = true;

            while (stopCondition)
            {
                var newEventTypeNumber = ValidIntInputAndCheck(1,4);

                if(newEventTypeNumber == (int) givenPair.Key.EventType)
                {
                    Console.WriteLine("Upisali ste isti tip eventa - nema promjene\n" +
                        "->povratak na menu\n");
                    stopCondition = false;
                }

                else
                {
                    switch (newEventTypeNumber)
                    {
                        case 1:
                            givenPair.Key.EventType = Event.EventTypes.Coffee;
                            break;

                        case 2:
                            givenPair.Key.EventType = Event.EventTypes.Lecture;
                            break;

                        case 3:
                            givenPair.Key.EventType = Event.EventTypes.Concert;
                            break;

                        case 4:
                            givenPair.Key.EventType = Event.EventTypes.StudySession; 
                            break;

                    }
                    
                    Console.WriteLine("Tip eventa uspjesno promjenjen u " + 
                        StringOfEventTypeWhenGivenInteger(newEventTypeNumber));

                }
            }
        }



        static void EditThisEventsTimes(Dictionary<Event, List<Person>> eventList, 
            KeyValuePair<Event, List<Person>> givenPair)
        {
            Console.WriteLine("Unesite koje vrijeme zelite mijenjati\n" +
                "1 - Pocetno vrijeme eventa\n" +
                "2 - Zavrsno vrijeme eventa\n" +
                "3 - I pocetno i zavrsno vrijeme eventa\n" + 
                "0 - Povratak na menu");
            
            var eventTimeChoice = ValidIntInputAndCheck(0, 3);

            switch (eventTimeChoice)
            {
                case 1:
                    Console.WriteLine("Unesite novo pocetno vrijeme eventa:");
                    var newEventStartTime = ChooseEventStartTime(eventList, true);

                    break;

                case 2:
                    Console.WriteLine("Unesite novo zavrsno vrijeme eventa:");
                    var newEventEndTime = ChooseEventEndTime(eventList, givenPair.Key.StartTime);

                    break;

                case 4:
                    Console.WriteLine("Unesite novo pocetno vrijeme eventa:");
                    var newEventStartTime2 = ChooseEventStartTime(eventList, false);

                    Console.WriteLine("Unesite novo zavrsno vrijeme eventa:");
                    var newEventEndTime2 = ChooseEventEndTime(eventList, newEventStartTime2);

                    givenPair.Key.StartTime = newEventStartTime2;
                    givenPair.Key.EndTime = newEventEndTime2;

                    break;

                case 0:
                    break;

            }
            
        }
        


        static void AddPersonToThisEvent(Dictionary<Event, List<Person>> eventList, 
            KeyValuePair<Event, List<Person>> givenPair)
        {
            Console.WriteLine("Unesite ime osobe:");
            var firstName = ValidStringInputAndCheck();

            Console.WriteLine("Unesite prezime osobe:");
            var lastName = ValidStringInputAndCheck();

            Console.WriteLine("Unesite OIB osobe (11 znamenki):");
            var OIB = OIBInputAndCheck(givenPair);

            Console.WriteLine("Unesite broj mobitela osobe (10 znamenki):");
            var phoneNumber = ValidIntInputAndCheck(0, 9999999999);
            
            givenPair.Value.Add(new Person(firstName, lastName, OIB, phoneNumber));
            
        }



        static void RemovePersonFromThisEvent(Dictionary<Event, List<Person>> eventList, 
            KeyValuePair<Event, List<Person>> givenPair)
        {
            Console.WriteLine("Osobe na eventu {0}:", givenPair.Key.Name);

            PrintPeopleAtThisEvent(givenPair);
            
            Console.WriteLine("Unesite redni broj ispred osobe koju zelite maknuti s eventa");

            var deletePersonIndex = ValidIntInputAndCheck(1, givenPair.Value.Count);

            Console.WriteLine("Jeste li sigurni da zelite izbrisati " +
                "{0} {1} s eventa {2}?", givenPair.Value[deletePersonIndex-1].FirstName,
                givenPair.Value[deletePersonIndex - 1].LastName, givenPair.Key.Name);

            Console.WriteLine("1 - Da, izbrisi osobu\n" +
                "2 - Ne, nemoj izbrisati osobu (->povratak na menu)");

            var deleteChoice = ValidIntInputAndCheck(1, 2);

            switch(deleteChoice)
            {
                case 1:
                    givenPair.Value.Remove(givenPair.Value[deletePersonIndex - 1]);

                    Console.WriteLine("Brisanje osobe uspjesno, povratak na menu");

                    break;

                case 2:
                    break;
                    
            }
        }



        static void PrintEventDetails(KeyValuePair<Event, List<Person>> givenPair)
        {
            //format, all people in event, all details, exit submenu
            Console.WriteLine("Unesite opciju ispisa detalja eventa\n" +
                "1 - Ispis detalja eventa\n" +
                "2 - Ispis svih osoba na eventu\n" +
                "3 - Ispis svih detalja\n" +
                "4 - Izlazak iz podmenija (->povratak u glavni menu)\n");

            var printChoice = ValidIntInputAndCheck(1, 4);

            switch(printChoice)
            {
                case 1:
                    Console.WriteLine("Ispis detalja eventa:");
                    PrintEventDetailsFormatted(givenPair);
                    break;

                case 2:
                    Console.WriteLine("Ispis osoba na eventu:");
                    PrintPeopleAtThisEvent(givenPair);

                    break;

                case 3:
                    Console.WriteLine("Ispis detalja eventa:");
                    PrintEventDetailsFormatted(givenPair);

                    Console.WriteLine("");

                    Console.WriteLine("Ispis osoba na eventu:");
                    PrintPeopleAtThisEvent(givenPair);

                    break;

                case 4:
                    break;
                    
            }

            Console.WriteLine("");
        }

        

        static string ChooseEventName(Dictionary<Event, List<Person>> eventList)
        {
            Console.WriteLine("\nUnesite ime eventa:");
            var eventName = "";
            
            var stopCondition = true;

            while(stopCondition)
            {
                var chosenEventName = ValidStringInputAndCheck();

                var stopArgument = IsNewNameAlreadyInNames(eventList, chosenEventName);

                if (!stopArgument)
                {
                    stopCondition = false;
                    Console.WriteLine("\nIme eventa {0} uspjesno uneseno", chosenEventName);
                    eventName = chosenEventName;
                }
                
            }
            
            return eventName;
        }



        static int ChooseEventTypeNumber()
        {
            Console.WriteLine("\nOdaberite tip eventa unosom jednog od rednih brojeva ispred ponudjenih tipova\n" +
                "Na raspolaganju su sljedeci tipovi eventa:\n" +
                "1 - Coffee\n" +
                "2 - Lecture\n" +
                "3 - Concert\n" +
                "4 - StudySession");

            var chosenEventType = ValidIntInputAndCheck(1,4);
            
            Console.WriteLine("\nOdabrali ste {0}. tip eventa\n", chosenEventType);
            
            return chosenEventType;
        }



        static DateTime ChooseEventStartTime(Dictionary<Event, List<Person>> eventList, bool isUsedInEditAction)
        {
            var startTimeCheck = true;
            
            var startTime = new DateTime();

            Console.WriteLine("\nUnesite pocetno vrijeme eventa u formatu mm/dd/yyyy hh:mm:ss");

            while (startTimeCheck)
            {
                startTime = DateTimeInputAndCheck();
                var startTimeStopper = false;

                if (isUsedInEditAction)
                {
                    foreach (var value in eventList.Keys)
                    {
                        if (DateTime.Compare(startTime, value.StartTime) == 0 &&
                             DateTime.Compare(startTime, value.EndTime) > 0)
                        {

                            Console.WriteLine("ERROR: Novo pocetno vrijeme eventa ne " +
                                "smije biti nakon vremena zavrsetka eventa");
                            startTimeStopper = true;
                            break;

                        }
                    }
                }

                if(!startTimeStopper)
                {
                    foreach (var keyValue in eventList.Keys)
                    {

                        if (DateTime.Compare(startTime, keyValue.StartTime) >= 0 &&
                             DateTime.Compare(startTime, keyValue.EndTime) < 0)
                        {

                            Console.WriteLine("ERROR: Uneseno se pocetno vrijeme preklapa s eventom {0}, {1}-{2}",
                                keyValue.Name, 
                                keyValue.StartTime.ToString(System.Globalization.CultureInfo.InvariantCulture),
                                keyValue.EndTime.ToString(System.Globalization.CultureInfo.InvariantCulture));

                            startTimeStopper = true;
                            break;

                        }
                    }
                }
                
                if (!startTimeStopper)
                    startTimeCheck = false;
                else
                    Console.WriteLine("Ponovno unesite pocetno vrijeme eventa u formatu mm/dd/yyyy hh:mm");

                startTimeStopper = false;
            }

            Console.WriteLine("Uspjesno uneseno pocetno vrijeme eventa: " + 
                startTime.ToString(System.Globalization.CultureInfo.InvariantCulture));

            return startTime;
        }



        static DateTime ChooseEventEndTime(Dictionary<Event, 
            List<Person>> eventList, DateTime chosenStartTime)
        {
            var endTimeCheck = true;
            
            var endTime = new DateTime();

            Console.WriteLine("\nUnesite zavrsno vrijeme eventa u formatu mm/dd/yyyy hh:mm");
            

            while (endTimeCheck)
            {
                endTime = DateTimeInputAndCheck();

                var endTimeStopper = false;

                //provjera je li event zavrsava prije pocetka
                if (DateTime.Compare(endTime, chosenStartTime) <= 0)
                {
                    Console.WriteLine("ERROR: Event ne moze zavrsiti prije pocetka");
                    endTimeStopper = true;

                }

                else
                {
                    foreach (var keyValue in eventList.Keys)
                    {
                        //provjera je li event kojeg pokusavamo upisati zavrsava usred drugog eventa

                        if (DateTime.Compare(endTime, keyValue.StartTime) > 0 
                            && DateTime.Compare(endTime, keyValue.EndTime) < 0)
                        {

                            Console.WriteLine("ERROR: Uneseno se vrijeme zavrsetka eventa preklapa s eventom {0}, {1} - {2}",
                                keyValue.Name, 
                                keyValue.StartTime.ToString("HH:mm"),
                                keyValue.EndTime.ToString("HH:mm"));

                            endTimeStopper = true;
                            break;

                        }

                        //provjera je li trenutni event poceo prije nekog drugog, a zavrsava nakon njega

                        else if (DateTime.Compare(chosenStartTime, keyValue.StartTime) < 0 
                            && DateTime.Compare(endTime, keyValue.EndTime) > 0)
                        {

                            Console.WriteLine("ERROR: Event koji upisujete mora zavrsiti prije {0}, " +
                                "inace ce doci do preklapanja s eventom {1}",
                                keyValue.StartTime.ToString("HH:mm"), keyValue.Name);

                            endTimeStopper = true;
                            break;

                        }
                    }
                }

                if (!endTimeStopper)
                    endTimeCheck = false;
                else
                    Console.WriteLine("Ponovno unesite zavrsno vrijeme eventa u formatu mm/dd/yyyy hh:mm");
                
            }

            Console.WriteLine("\nUspjesno uneseno zavrsno vrijeme eventa: "
                + endTime.ToString(System.Globalization.CultureInfo.InvariantCulture));

            return endTime;
        }



        static DateTime DateTimeInputAndCheck()
        {
            var tempDateTime = Console.ReadLine();
            bool dateTimeSuccess = DateTime.TryParse(tempDateTime, out DateTime dateTimeValue);

            while (!dateTimeSuccess)
            {
                Console.WriteLine("ERROR: Neispravan unos datuma - molimo ponovite unos");

                tempDateTime = Console.ReadLine();
                dateTimeSuccess = DateTime.TryParse(tempDateTime, out dateTimeValue);
            }

            return dateTimeValue;
        }



        static int ValidIntInputAndCheck(int range1, long range2)
        {
            var tempInteger = Console.ReadLine();

            bool someSuccessBoolean = int.TryParse(tempInteger, out int someInteger);

            while (!someSuccessBoolean || someInteger < range1 || someInteger > range2)
            {
                if (!someSuccessBoolean)
                    Console.WriteLine("ERROR: Neispravan unos broja - ponovni unos");

                else
                    Console.WriteLine("ERROR: Uneseni redni broj je van ranga {0} - {1} -> ponovni unos",
                        range1, range2);

                tempInteger = Console.ReadLine();
                someSuccessBoolean = int.TryParse(tempInteger, out someInteger);
            }

            return someInteger;
        }



        static string ValidStringInputAndCheck()
        {
            var someString = Console.ReadLine();
            while (string.IsNullOrEmpty(someString))
            {
                Console.WriteLine("ERROR: Ime eventa ne moze biti prazno - ponovite unos");
                someString = Console.ReadLine();
            }

            return someString;
        }
        


        static bool IsNewNameAlreadyInNames(Dictionary<Event, List<Person>> eventList, 
            string chosenEventName)
        {
            foreach (var someEvent in eventList.Keys)
            {
                if (someEvent.Name == chosenEventName)
                {
                    Console.WriteLine("ERROR: Eventovi ne mogu imati isto ime - ponovite unos");

                    return true;
                }
            }

            return false;
        }
        
        
        
        static string StringOfEventTypeWhenGivenInteger(int someNumber)
        {
            var someEventString = "";

            switch(someNumber)
            {
                case 1:
                    someEventString = "Coffee";
                    break;
                case 2:
                    someEventString = "Lecture";
                    break;
                case 3:
                    someEventString = "Concert";
                    break;
                case 4:
                    someEventString = "StudySession";
                    break;
            }

            return someEventString;
        }



        static int OIBInputAndCheck(KeyValuePair<Event, 
            List<Person>> givenPair)
        {
            var chosenOIB = 0;

            var stopCondition = true;
            while (stopCondition)
            {
                chosenOIB = ValidIntInputAndCheck(0, 99999999999);
                var stopArgument = false;

                foreach (var person in givenPair.Value)
                {
                    if (chosenOIB == person.OIB)
                    {

                        Console.WriteLine("ERROR: Dvije osobe na istom eventu ne mogu imati isti OIB" +
                            " - ponovnite unos");
                        stopArgument = true;
                        break;

                    }
                    
                }

                if (!stopArgument)
                {
                    stopCondition = true;
                    Console.WriteLine("Uspjesno unesen OIB osobe");
                }

            }

            return chosenOIB;
        }



        static void PrintPeopleAtThisEvent(KeyValuePair<Event, 
            List<Person>> givenPair)
        {
            var counter = 1;
            foreach (var person in givenPair.Value)
            {
                Console.WriteLine("{0}. {1} - {2} - {3}", counter++,
                    person.FirstName, person.LastName, person.PhoneNumber);
            }
        }



        static void PrintEventsInNumberNameFormat(Dictionary<Event, List<Person>> eventList)
        {
            var counter = 1;

            Console.WriteLine("Ovo su vasi trenutni eventovi:");

            foreach (var value in eventList.Keys)
            {
                Console.WriteLine("{0} - {1}", counter++, value.Name);
            }

        }



        static void PrintEventDetailsFormatted(KeyValuePair<Event, List<Person>> givenPair)
        {
            var eventDuration = givenPair.Key.EndTime.Subtract(givenPair.Key.StartTime);

            Console.WriteLine("{0} - {1} - {2} - {3} - {4}", givenPair.Key.Name, 
                givenPair.Key.EventType, givenPair.Key.StartTime, 
                givenPair.Key.EndTime, eventDuration, givenPair.Value.Count);

        }

    }
}
