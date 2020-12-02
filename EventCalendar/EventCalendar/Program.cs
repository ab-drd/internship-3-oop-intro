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

            Console.WriteLine("Dobrodosli na pocetak programa\n\n======================================");
            //Action menu
            while (menuStopCondition)
            {
                Console.WriteLine(actionCounter + ". akcija\n======================================\n");
                Console.WriteLine("Unesite akciju:\n1 - Dodavanje eventa\n2 - Brisanje eventa\n3 - Edit eventa\n4 - Dodavanje osobe na event" +
                    "\n5 - Uklanjanje osobe s eventa\n6 - Ispis detalja eventa\n0 - izlazak iz aplikacije\n");
                

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
                        EditEvent(allEvents);
                        break;
                    case 4:
                        AddPersonToEvent(allEvents);
                        break;
                    case 5:
                        RemovePersonFromEvent(allEvents);
                        break;
                    case 6:
                        PrintEventDetails(allEvents);
                        break;
                    case 0:
                        menuStopCondition = false;
                        break;
                }

                Console.WriteLine("======================================");
                actionCounter++;
            }

        }

        static void AddNewEvent(Dictionary<Event, List<Person>> eventList)
        { 
            Console.WriteLine(">>>>>>>>>>>>>>>>>\n");

            var eventName = ChooseEventName(); //funkcija odabira imena eventa

            var eventTypeNumber = ChooseEventTypeNumber(); //funkcija odabira tipa eventa

            var eventStartTime = ChooseEventStartTime(eventList); //funkcija odabira pocetnog vremena eventa

            var eventEndTime = ChooseEventEndTime(eventList, eventStartTime); //funkcija odabira zavrsnog vremena eventa

            var newEvent = new Event(eventName, eventTypeNumber, eventStartTime, eventEndTime);
            //stvaranje novog eventa preko konstruktora klase Event

            eventList[newEvent] = new List<Person>(); //dodavanje novog eventa u Dictionary

            Console.WriteLine("<<<<<<<<<<<<<<<<<\n\n");
        }

        static void DeleteEvent(Dictionary<Event, List<Person>> eventList)
        {
            

            if (eventList.Count == 0)
            {
                Console.WriteLine("Trenutno nema upisanih eventova - povratak na menu");
            }
            else
            {
                var counter = 1;

                Console.WriteLine("Ovo su vasi trenutni eventovi:");
                foreach (var value in eventList.Keys)
                {
                    Console.WriteLine("{0} - {1}", counter++, value.Name);
                }

                counter = 1;

                var repeatCondition = true;
                while (repeatCondition)
                {

                    Console.WriteLine("Unesite redni broj eventa koji zelite izbrisati:");

                    var deleteIndex = ValidIntInputAndCheck(1, eventList.Count);

                    foreach (var value in eventList.Keys)
                    {
                        if (counter == deleteIndex)
                        {
                            
                            Console.WriteLine("Jeste li sigurni da zelite izbrisati event na rednom broju {0} imena {1}?\n1 - da, izbrisi\n2 - ne, " +
                                "odvedi me na ponovni unos\n0 - ne, vrati me u menu", deleteIndex, value.Name);

                            var whatToDo = ValidIntInputAndCheck(0, 2);

                            if (whatToDo == 1)
                            {
                                eventList.Remove(value);
                                Console.WriteLine("Uspjesno izbrisan event - povratak na menu\n");
                                repeatCondition = false;

                            }

                            else if (whatToDo == 2)
                                Console.WriteLine("Povratak na ponovni unos\n");
                            
                            else
                            {
                                Console.WriteLine("Povratak na menu\n");
                                repeatCondition = false;
                            }

                            break;
                        }
                        counter++;
                    }
                }
            }
        }

        static void EditEvent(Dictionary<Event, List<Person>> eventList)
        {

        }

        static void AddPersonToEvent(Dictionary<Event, List<Person>> eventList)
        {

        }

        static void RemovePersonFromEvent(Dictionary<Event, List<Person>> eventList)
        {

        }

        static void PrintEventDetails(Dictionary<Event, List<Person>> eventList)
        {
            //format, all people in event, all details, exit submenu
        }


        static string ChooseEventName()
        {
            Console.WriteLine("Unesite ime eventa(bilo koji string):");
            var chosenEventName = Console.ReadLine();

            while (string.IsNullOrEmpty(chosenEventName))
            {
                Console.WriteLine("Ime eventa ne moze biti prazno - ponovite unos");
                chosenEventName = Console.ReadLine();
            }

            return chosenEventName;
        }

        static int ChooseEventTypeNumber()
        {
            Console.WriteLine("\nOdaberite tip eventa unosom jednog od rednih brojeva ispred ponudjenih tipova" +
                "\nNa raspolaganju su sljedeci tipovi eventa:\n1 - Coffee\n2 - Lecture\n3 - Concert\n4 - StudySession");

            var chosenEventType = ValidIntInputAndCheck(1,4);

            Console.WriteLine("Odabrali ste {0}. tip eventa", chosenEventType);
            
            return chosenEventType;
        }

        static DateTime ChooseEventStartTime(Dictionary<Event, List<Person>> eventList)
        {
            var startTimeCheck = true;
            var startTimeStopper = false;
            var startTime = new DateTime();

            Console.WriteLine("\nUnesite pocetno vrijeme eventa u formatu mm/dd/yyyy hh:mm:ss");

            while (startTimeCheck)
            {
                startTime = DateTimeInputAndCheck();

                foreach (var keyValue in eventList.Keys)
                {

                    if (DateTime.Compare(startTime, keyValue.StartTime) > 0 
                        && DateTime.Compare(startTime, keyValue.EndTime) < 0)
                    {

                        Console.WriteLine("Uneseno se pocetno vrijeme preklapa s eventom {0}, {1}-{2}",
                            keyValue.Name, keyValue.StartTime.ToString(System.Globalization.CultureInfo.InvariantCulture),
                            keyValue.EndTime.ToString(System.Globalization.CultureInfo.InvariantCulture));

                        startTimeStopper = true;
                        break;

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

        static DateTime ChooseEventEndTime(Dictionary<Event, List<Person>> eventList, DateTime chosenStartTime)
        {
            var endTime = new DateTime();
            var endTimeCheck = true;
            var endTimeStopper = false;

            Console.WriteLine("\nUnesite zavrsno vrijeme eventa u formatu mm/dd/yyyy hh:mm");

            while (endTimeCheck)
            {
                //provjera je li event zavrsava prije pocetka

                if (DateTime.Compare(endTime, chosenStartTime) < 0)
                {

                    Console.WriteLine("Event ne moze zavrsiti prije pocetka eventa ");
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

                            Console.WriteLine("Uneseno se vrijeme zavrsetka eventa preklapa s eventom {0}, {1} - {2}",
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

                            Console.WriteLine("Event koji upisujete mora zavrsiti prije {0}, " +
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
                
                endTimeStopper = false;
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
                Console.WriteLine("Neispravan unos datuma - molimo ponovite unos");

                tempDateTime = Console.ReadLine();
                dateTimeSuccess = DateTime.TryParse(tempDateTime, out dateTimeValue);
            }

            return dateTimeValue;
        }

        static int ValidIntInputAndCheck(int range1, int range2)
        {
            var tempInteger = Console.ReadLine();

            bool someSuccessBoolean = int.TryParse(tempInteger, out int someInteger);

            while (!someSuccessBoolean || someInteger < range1 || someInteger > range2)
            {
                if(!someSuccessBoolean)
                    Console.WriteLine("Neispravan unos broja - ponovni unos");
                
                else
                    Console.WriteLine("Uneseni redni broj je van ranga {0} - {2} - ponovni unos",
                        range1, range2);

                tempInteger = Console.ReadLine();
                someSuccessBoolean = int.TryParse(tempInteger, out someInteger);
            }

            return someInteger;
        }

    }
}
