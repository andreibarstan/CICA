using System;
using System.Linq; //database 
using System.Globalization; //used to format patterns for dates

namespace CICA
{
    class UpdateEvent : Event 
    {
        public void updateEvent() //method used to update events
        {
            var db = new EventDatabase(); //database object
            using (db) //database connection
            {
                Console.WriteLine("\n\tUpdate event ");
                Console.Write("\nPlease enter the event ID to update: ");
                try //block of code to be tested for errors while it is being executed
                {
                    int eventID = int.Parse(Console.ReadLine());
                    //find event and display 
                    var query = from b in db.Events
                                 where b.EventID == eventID //search criteria
                                 select b;
                    if (query.Count() > 0) //if matching records found
                    {
                        foreach (var item in query) 
                        {
                            item.PrintEvent(); //display event using print interface
                        }
                        //modify event and submit changes
                        foreach (Event b in query) 
                        {
                            Console.Write("Is this the event you want to update? Press y to confirm: ");
                            var opt = Console.ReadLine();
                            if (opt.ToLower() != "y")
                            {
                                break;
                            }
                            foreach (var item in query.OfType<CalIntEvent>())
                            {
                                Console.Write("Update room number? \nPress y to update or any other key to skip! ");
                                string option = Console.ReadLine();
                                if (option.ToLower() == "y")
                                {
                                    Console.Write("\nUpdate room number: ");
                                    int roomNo = Convert.ToInt32(Console.ReadLine());
                                    var internalEvents = from evnt in db.Events.OfType<CalIntEvent>() //retrieve the internal event with the matching ID
                                                         where evnt.EventID == eventID 
                                                         select evnt;
                                    foreach (var evnt in internalEvents)
                                    {
                                        item.RoomNumber = roomNo; //update the room number for internal event 
                                    }
                                }
                            }
                            foreach (var item in query.OfType<CalExtEvent>())
                            {
                                Console.Write("\nUpdate customer ID? \nPress y to update or any other key to skip! ");
                                string option = Console.ReadLine();
                                if (option.ToLower() == "y")
                                {
                                    Console.Write("Update customer ID: ");
                                    int custID = Convert.ToInt32(Console.ReadLine());
                                    var externalEvents = from evnt in db.Events.OfType<CalExtEvent>() //retrieve the external event with the matching ID
                                                         where evnt.EventID == eventID
                                                         select evnt;
                                    foreach (var evnt in externalEvents)
                                    {
                                        item.CustID = custID; //update the room number for external event 
                                    }
                                }
                            }
                            Console.Write("Update title, description and location? \nPress y to update or any other key to skip! ");
                            string option2 = Console.ReadLine();
                            if (option2.ToLower() == "y")
                            {
                                Console.Write("Update title: ");
                                b.Title = Console.ReadLine(); //assign changes to the field

                                Console.Write("Update description: ");
                                b.Description = Console.ReadLine();

                                Console.Write("Update location: ");
                                b.Location = Console.ReadLine();
                            }
                            bool overlap; 
                            do //loop handling clashing events
                            {
                                overlap = false;
                                Console.Write("Update event date and time? \nPress y to update or any other key to skip! ");
                                string option = Console.ReadLine();
                                if (option.ToLower() == "y")
                                {
                                    Console.WriteLine("Update start time using this format: dd/MM/yyyy HH:mm\tExample: 20/02/2022 09:00");
                                    string dateFormat = "dd/MM/yyyy HH:mm"; //format of date and time
                                    DateTime newDate = DateTime.ParseExact(Console.ReadLine(), dateFormat, CultureInfo.InvariantCulture); //use parsing to setup manual format to match DateTime format
                                    b.StartTime = newDate; //assign changes to field

                                    Console.WriteLine("Update event duration using this format: hh:mm\t Example: 05:00");
                                    string durationFormat = "hh\\:mm"; //only lowercase hh is recognised as hour. The colon needs to be escaped using '\' as it is not recognised by TimeSpan
                                    var newTime = TimeSpan.ParseExact(Console.ReadLine(), durationFormat, CultureInfo.InvariantCulture); //use parsing to setup manual format to match TimeSpan format
                                    b.Duration = newTime;
                                
                                    var queryOverlap = from item in db.Events //check for overlapping start time/date when updating event
                                                   where item.StartTime == newDate && eventID != item.EventID
                                                   select item;
                                    if (queryOverlap.Count() > 0) //if start time overlaps
                                    {
                                        foreach (var item in queryOverlap)
                                        {
                                            item.PrintEvent(); //display overlapping event using print interface
                                            Console.Write("The inserted dates are overlapping with the event above! \nPress y to change the dates or any other key to continue with selected dates! ");
                                            string newInput = Console.ReadLine();
                                            if (newInput.ToLower() == "y") //
                                            {
                                                overlap = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            while (overlap == true);
                        }
                        db.SaveChanges();   //save changes in the database
                        Console.WriteLine("\nRecord updated!");
                    }
                    else
                    {
                        Console.WriteLine("No matching events! Please enter a valid Event ID! To find an Event ID, view all events and select an Event ID.");
                    }
                }
                catch (FormatException) //invalid input exception handling
                {
                    Console.WriteLine("Invalid format! Changes were not saved! Please enter only valid data!");
                }
                catch (ArgumentOutOfRangeException) //input is not in the range allocated by the data type declared 
                {
                    Console.WriteLine("Please enter in range date/time format! e.g: Maximum value for Hour is 23");
                }
                catch (Exception excep) //general exception handler
                {
                    Console.WriteLine(excep.Message);
                    //Console.WriteLine(excep.ToString()); //detailed exception information used for debugging
                }
            }
        }
    }
}