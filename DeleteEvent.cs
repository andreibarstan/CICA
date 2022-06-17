using System;
using System.Linq;

namespace CICA
{
    class DeleteEvent : Event 
    {
        public void deleteEvent() 
        {
            var db = new EventDatabase(); //database object
            using (db)
            {
                Console.WriteLine("\n\tDelete event ");
                Console.Write("\nPlease enter the event ID to delete: ");
                try //block of code to be tested for errors while it is being executed
                {
                    int eventID = int.Parse(Console.ReadLine()); //event ID input
                    var query = from b in db.Events //search database for matching results
                                where b.EventID == eventID //search criteria
                                select b;
                    if (query.Count() > 0) //check database
                    {
                        foreach (var selectedEvent in query)
                        {
                            selectedEvent.PrintEvent(); //print event using print interface
                            Console.Write("Are you sure you want to delete this event? Press y to delete or other key to cancel: ");
                            string deleteEvent = Console.ReadLine();
                            if (deleteEvent.ToLower() == "y") //confirm deletion
                            {
                                db.Events.Remove(selectedEvent);     //remove event
                                Console.WriteLine("\nRecord deleted!");
                            }
                            else
                            {
                                Console.WriteLine("\nSelected event was not deleted!");
                            }
                        }
                        db.SaveChanges(); //save changes
                    }
                    else
                    {
                        Console.WriteLine("\nNo matching events!");
                    }
                }
                catch (FormatException) //invalid input exception handling
                {
                    Console.WriteLine("Enter a valid Event ID! To find an Event ID, view all events and select an Event ID.");
                }
                catch (Exception excep) //general exception handling
                {
                    Console.WriteLine(excep.Message);
                    //Console.WriteLine(excep.ToString()); explicit error details for developer
                }
            }
        }
    }
}
