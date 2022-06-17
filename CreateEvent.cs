using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace CICA
{
    public class CreateEvent : Event 
    {
        public void newEvent() //create event method
        { 
            var db = new EventDatabase(); //database object
            using (db) //database connection open only to run the script within the brackets terminating after the brackets
            {
                string createEvent = "y";
                int roomNumber = 0;
                int custID = 0;
                do //loop to create new event
                {
                    Console.WriteLine("\n\tAdd new event\n");

                    //input event type
                    Console.Write("\n 1 Create internal event \n 2 Create external event \n 0 Main menu\n\nEnter menu option: ");
                    string eventType = Console.ReadLine();
                    try //block of code to be tested for errors while it is being executed
                    {
                        if (eventType == "1") //internal event
                        {
                            Console.WriteLine("\n\tInternal event\n");
                            Console.Write("Enter room number: ");
                            roomNumber = Convert.ToInt16(Console.ReadLine());
                        }
                        else if (eventType == "2") //external event
                        {
                            Console.WriteLine("\n\tExternal event\n");
                            Console.Write("Enter Customer ID number: ");
                            custID = Convert.ToInt16(Console.ReadLine());
                        }
                        else if (eventType == "0")  //return to main menu
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input! Enter 1 or 2 to select the event type!");
                            break; //back to main menu
                        }
                        //title
                        Console.Write("Event title: ");
                        string title = Console.ReadLine();

                        //description
                        Console.Write("Event description: ");
                        string description = Console.ReadLine();

                        //location
                        Console.Write("Event location: ");
                        string location = Console.ReadLine();

                        //loop controling overlapping start time of events
                        bool isOverlaping;
                    
                        do  
                        {
                            isOverlaping = false;
                            //event start time/date 
                            Console.Write("\n\tEvent date \nEnter day: ");
                            int day = int.Parse(Console.ReadLine());
                            Console.Write("Enter month: ");
                            int month = int.Parse(Console.ReadLine());
                            Console.Write("Enter year: ");
                            int year = int.Parse(Console.ReadLine());
                            Console.Write("\n\tEvent time \nEnter hour: ");
                            int hour = int.Parse(Console.ReadLine());
                            Console.Write("Enter minutes: ");
                            int minute = int.Parse(Console.ReadLine());
                            DateTime startTime = new DateTime(year, month, day, hour, minute, 0);
                            if (startTime < DateTime.Now)
                            {
                                Console.WriteLine("\nThe date you inserted is in the past. Press y to continue with selected date or any key to cancel: ");
                                var opt = Console.ReadLine();
                                if (opt.ToLower() != "y")
                                {
                                    break;
                                }
                            }
                            //event duration  
                            Console.Write("\n\tEvent duration \nEnter hours: ");
                            int durationHours = int.Parse(Console.ReadLine());
                            Console.Write("Enter minutes: ");
                            int durationMinutes = int.Parse(Console.ReadLine());
                            TimeSpan duration = new TimeSpan(durationHours, durationMinutes, 0);

                            var queryOverlap = from b in db.Events //query the database
                                               where b.StartTime == startTime //look for matching start time
                                               select b;
                            if (queryOverlap.Count() > 0) //if start time overlaps
                            {
                                foreach (var item in queryOverlap)
                                {
                                    item.PrintEvent(); //print overlapping event
                                    Console.Write("The inserted dates are overlapping with the event above! \nPress y to change the dates or any other key to continue with selected dates! ");
                                    string newInput = Console.ReadLine();
                                    newInput.ToLower();
                                    if (newInput == "y") //repeat loop and modify overlapping values
                                    {
                                        isOverlaping = true;
                                        break;
                                    }
                                }
                            } 
                            if (eventType == "1" && isOverlaping == false) 
                            {
                                Event intEvent = new CalIntEvent { RoomNumber = roomNumber, Title = title, Description = description, Location = location, StartTime = startTime, Duration = duration };
                                db.Events.Add(intEvent); //add internal event to database
                                db.SaveChanges(); //save changes
                                intEvent.SaveEvent(); //interface SaveEvent is used
                            }
                            else if (eventType == "2" && isOverlaping == false)
                            {
                                Event extEvent = new CalExtEvent { CustID = custID, Title = title, Description = description, Location = location, StartTime = startTime, Duration = duration };
                                db.Events.Add(extEvent); //add internal event to database
                                db.SaveChanges(); //save changes
                                extEvent.SaveEvent();
                            }
                        }
                        while (isOverlaping == true);
                    }
                    catch (FormatException) //specific exceptions that could encounter during runtime
                    {
                        Console.WriteLine("Invalid format! Please enter valid details!");
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("Out of range input! Please enter valid date/time format! e.g: Maximum value for Hour is 23");
                    }
                    catch (DbUpdateException)
                    {
                        Console.WriteLine("Incorrect input! Please enter correct values!");
                    }
                    catch (Exception excep) //general exception handling
                    {
                        Console.WriteLine(excep.Message);
                        //Console.WriteLine(excep.ToString()); //detailed exception handling used for debugging
                    }
                    Console.Write("Press y to create another event or any key to exit to the main menu: ");
                    createEvent = Console.ReadLine();
                } while (createEvent.ToLower() == "y");
            }
        }
    }
}