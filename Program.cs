/*
 * Hotel event management application: Retro Calendar
 * Programmer: Andrei Barstan
 * Date: 2021
 */
using System;
using System.Data.Entity;
using CICA.Migrations; //Code First Migrations used to update database schema

namespace CICA
{
    class Program
    {
        static void Main(string[] args) //main method
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EventDatabase, Configuration>()); //latest database schema is used for each runtime
            string menuChoice;
            Menu mainMenu = new Menu(); // instance of the "Menu" object
            
            do //menu is prompted until exit
            {
                mainMenu.calendarMenu(); //display menu
                menuChoice = Console.ReadLine(); 

                switch (menuChoice) //switch statement used to access each menu option
                {
                    case "1":                        //query and display all events
                        DisplayEvents allEvents = new DisplayEvents();
                        allEvents.allEvents();
                        break;

                    case "2":                        // view internal events
                        DisplayEvents intEvents = new DisplayEvents();
                        intEvents.internalEvents();
                        break;

                    case "3":                        // view external events
                        DisplayEvents extEvents = new DisplayEvents();
                        extEvents.externalEvents();
                        break;

                    case "4":                        // create new event 
                        CreateEvent newEvent = new CreateEvent();
                        newEvent.newEvent();
                        break;

                    case "5":                        // update existing event
                        UpdateEvent updateEvent = new UpdateEvent();
                        updateEvent.updateEvent();
                        break;

                    case "6":                       // delete event
                        DeleteEvent deleteEvent = new DeleteEvent();
                        deleteEvent.deleteEvent();
                        break;

                    case "7":                       // clashing event
                        DisplayEvents clashEvent = new DisplayEvents();
                        clashEvent.clashingEvents();
                        break;

                    case "0":                       // exit
                        Console.WriteLine("Thank you for using Retro Calendar Application!\nPress any key to exit!");
                        break;

                    default:
                        Console.WriteLine("\nPlease enter a value from 0 to 7!\n"); 
                        break;
                }
            } while (menuChoice != "0");
            Console.ReadKey();
        }
    }
}