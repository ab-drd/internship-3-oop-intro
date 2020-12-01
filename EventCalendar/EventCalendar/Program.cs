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
            var menuStopCondition = -1; var counter = 1; int caseSwitch;
            Console.WriteLine("Dobrodosli na pocetak programa\n\n======================================");
            //Action menu
            while (menuStopCondition != 0)
            {
                Console.WriteLine(counter + ". akcija\n======================================\n");
                Console.WriteLine("Unesite akciju:\n1 - Dodavanje eventa\n2 - Brisanje eventa\n3 - Edit eventa\n4 - Dodavanje osobe na event" +
                    "\n5 - Uklanjanje osobe s eventa\n6 - Ispis detalja eventa\n0 - izlazak iz aplikacije\n");
                var tempCaseSwitch = Console.ReadLine();

                bool caseSwitchSuccess = int.TryParse(tempCaseSwitch, out caseSwitch);
                while(!caseSwitchSuccess || caseSwitch < 0 || caseSwitch > 6)
                {
                    if(!caseSwitchSuccess)
                    {
                        Console.WriteLine("Neispravan unos broja - ponovite unos");
                    }
                    else
                    {
                        Console.WriteLine("Unesena opcija nije validna - ponovite unos");
                    }
                    tempCaseSwitch = Console.ReadLine();
                    caseSwitchSuccess = int.TryParse(tempCaseSwitch, out caseSwitch);
                }
                switch(caseSwitch)
                {
                    case 1:
                        AddNewEvent(allEvents);
                        break;
                    case 2:
                        DeleteEvent(allEvents);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 0:
                        menuStopCondition = 0;
                        break;
                }
                Console.WriteLine("======================================");
                counter++;
            }
            
        }

        static void AddNewEvent(Dictionary<Event, List<Person>> eventList)
        {
            int eventTypeNumberConfirmed; var startTimeConfirmed = new DateTime(); var endTimeConfirmed = new DateTime();

            Console.WriteLine(">>>>>>>>>>>>>>>>>\nUnesite ime eventa (bilo koji string):");
            var eventName = Console.ReadLine();
            while (string.IsNullOrEmpty(eventName))
            {
                Console.WriteLine("Ime eventa ne moze biti prazno - ponovite unos");
                eventName = Console.ReadLine();
            }

            Console.WriteLine("\nOdaberite tip eventa unosom jednog od rednih brojeva ispred ponudjenih tipova" +
                "\nNa raspolaganju su sljedeci tipovi eventa:\n1 - Coffee\n2 - Lecture\n3 - Concert\n4 - StudySession");
            var tempEventTypeNumber = Console.ReadLine();
            bool eventParseSuccess = int.TryParse(tempEventTypeNumber, out eventTypeNumberConfirmed);
            while (!eventParseSuccess || eventTypeNumberConfirmed > 4 || eventTypeNumberConfirmed < 1) //provjera ispravnog unosa
            {
                Console.WriteLine("Neispravan unos broja - ponovite unos");
                tempEventTypeNumber = Console.ReadLine();
                eventParseSuccess = int.TryParse(tempEventTypeNumber, out eventTypeNumberConfirmed);
            }
            Console.WriteLine("Odabrali ste {0}. tip eventa", eventTypeNumberConfirmed);

            Console.WriteLine("\nUnesite pocetno vrijeme eventa u formatu mm/dd/yyyy hh:mm:ss");
            var startTimeCheck = -1; var startTimeStopper = 0;
            while (startTimeCheck != 0)
            {
                var tempStartTime = Console.ReadLine();
                bool startTimeSuccess = DateTime.TryParse(tempStartTime, out startTimeConfirmed);
                while (!startTimeSuccess)
                {
                    Console.WriteLine("Neispravan unos broja - molimo ponovite unos");
                    tempStartTime = Console.ReadLine();
                    startTimeSuccess = DateTime.TryParse(tempStartTime, out startTimeConfirmed);
                }

                foreach (var keyValue in eventList.Keys)
                {
                    if (DateTime.Compare(startTimeConfirmed, keyValue.StartTime) > 0 && DateTime.Compare(startTimeConfirmed, keyValue.EndTime) < 0)
                    {
                        Console.WriteLine("Uneseno se pocetno vrijeme preklapa s eventom {0}, {1}-{2}",
                            keyValue.Name, keyValue.StartTime.ToString(System.Globalization.CultureInfo.InvariantCulture),
                            keyValue.EndTime.ToString(System.Globalization.CultureInfo.InvariantCulture));
                        startTimeStopper++;
                        break;
                    }
                }

                if (startTimeStopper == 0)
                {
                    startTimeCheck = 0;
                }
                startTimeStopper = 0;
            }
            Console.WriteLine("\nUspjesno uneseno pocetno vrijeme eventa: " + startTimeConfirmed.ToString(System.Globalization.CultureInfo.InvariantCulture));

            Console.WriteLine("\nUnesite zavrsno vrijeme eventa u formatu mm/dd/yyyy hh:mm");
            var endTimeCheck = -1; var endTimeStopper = 0;
            while (endTimeCheck != 0)
            {
                var tempEndTime = Console.ReadLine();
                bool endTimeSuccess = DateTime.TryParse(tempEndTime, out endTimeConfirmed);
                while (!endTimeSuccess)
                {
                    Console.WriteLine("Neispravan unos broja - molimo ponovite unos");
                    tempEndTime = Console.ReadLine();
                    endTimeSuccess = DateTime.TryParse(tempEndTime, out endTimeConfirmed);
                }

                if (DateTime.Compare(endTimeConfirmed, startTimeConfirmed) < 0)
                {
                    Console.WriteLine("Event ne moze zavrsiti prije pocetka eventa ");
                    endTimeStopper++;
                }
                else
                {
                    foreach (var keyValue in eventList.Keys)
                    {

                        if (DateTime.Compare(endTimeConfirmed, keyValue.StartTime) > 0 && DateTime.Compare(endTimeConfirmed, keyValue.EndTime) < 0)
                        {
                            Console.WriteLine("Uneseno se vrijeme zavrsetka eventa preklapa s eventom {0}, {1}-{2}",
                                keyValue.Name, keyValue.StartTime.ToString(System.Globalization.CultureInfo.InvariantCulture),
                                keyValue.EndTime.ToString(System.Globalization.CultureInfo.InvariantCulture));
                            endTimeStopper++;
                            break;
                        }
                        else if (DateTime.Compare(endTimeConfirmed, keyValue.EndTime) > 0 && DateTime.Compare(startTimeConfirmed, keyValue.StartTime) < 0)
                        {
                            Console.WriteLine("Event koji upisujete mora zavrsiti prije {0}, inace ce doci do preklapanja s eventom {1}",
                                keyValue.StartTime.ToString(System.Globalization.CultureInfo.InvariantCulture), keyValue.Name);
                            endTimeStopper++;
                            break;
                        }
                    }
                }

                if (endTimeStopper == 0)
                {
                    endTimeCheck = 0;
                }
                else
                {
                    Console.WriteLine("Ponovno unesite zavrsno vrijeme eventa u formatu mm/dd/yyyy hh:mm");
                }
                endTimeStopper = 0;
            }

            Console.WriteLine("\nUspjesno uneseno zavrsno vrijeme eventa: " + endTimeConfirmed.ToString(System.Globalization.CultureInfo.InvariantCulture));

            var newEvent = new Event(eventName, eventTypeNumberConfirmed, startTimeConfirmed, endTimeConfirmed);
            eventList[newEvent] = new List<Person>();
            Console.WriteLine("<<<<<<<<<<<<<<<<<\n\n");
        }

        static void DeleteEvent(Dictionary<Event, List<Person>> eventList)
        {
            int deleteIndex;  var counter = 1; var repeatCondition = -1;
            if(eventList.Count == 0)
            {
                Console.WriteLine("Trenutno nema upisanih eventova - povratak na menu");
            }
            else
            {
                Console.WriteLine("Ovo su vasi trenutni eventovi:");
                foreach (var value in eventList.Keys)
                {
                    Console.WriteLine("{0}. event - {1}", counter++, value.Name);
                }
                counter = 1;
                while (repeatCondition != 0)
                {
                    Console.WriteLine("Unesite redni broj eventa koji zelite izbrisati:");
                    var tempDeleteIndex = Console.ReadLine();

                    bool deleteIndexSuccess = int.TryParse(tempDeleteIndex, out deleteIndex);
                    while (!deleteIndexSuccess || deleteIndex > eventList.Count || deleteIndex < 1)
                    {
                        if (!deleteIndexSuccess)
                        {
                            Console.WriteLine("Neispravan unos broja - ponovni unos");
                        }
                        else
                        {
                            Console.WriteLine("Uneseni redni broj je van ranga - ponovni unos");
                        }
                        tempDeleteIndex = Console.ReadLine();
                        deleteIndexSuccess = int.TryParse(tempDeleteIndex, out deleteIndex);
                    }

                    foreach (var value in eventList.Keys)
                    {
                        if (counter == deleteIndex)
                        {
                            int whatToDo;

                            Console.WriteLine("Jeste li sigurni da zelite izbrisati event na rednom broju {0} imena {1}?\n1 - da, izbrisi\n2 - ne, " +
                                "odvedi me na ponovni unos\n0 - ne, vrati me u menu", deleteIndex, value.Name);

                            var tempWhatToDo = Console.ReadLine();
                            bool whatToDoSuccess = int.TryParse(tempWhatToDo, out whatToDo);
                            while (!whatToDoSuccess || whatToDo < 0 || whatToDo > 2)
                            {
                                if (!whatToDoSuccess)
                                {
                                    Console.WriteLine("Neispravan unos broja - ponovni unos");
                                }
                                else
                                {
                                    Console.WriteLine("Uneseni redni broj je van ranga - ponovni unos");
                                }
                                tempWhatToDo = Console.ReadLine();
                                whatToDoSuccess = int.TryParse(tempWhatToDo, out whatToDo);
                            }

                            if (whatToDo == 1)
                            {
                                eventList.Remove(value);
                                Console.WriteLine("Uspjesno izbrisan event - povratak na menu\n");
                                repeatCondition = 0;
                            }
                            else if (whatToDo == 2)
                            {
                                Console.WriteLine("Povratak na ponovni unos\n");
                            }
                            else
                            {
                                Console.WriteLine("Povratak na menu\n");
                                repeatCondition = 0;
                            }

                            break;
                        }
                        counter++;
                    }
                }
            }
        }
    }
}
