using System;
using System.Linq;

namespace CICA
{
    public class DisplayEvents : Event //class including multiple methods that display events
    {
        public void allEvents() //method displaying all events
        {
            var db = new EventDatabase(); //database object
            using (db)
            {
                // view all events
                Console.WriteLine("\n\tAll events\n");
                if (db.Events.Count() > 0) //check if database is populated
                {
                    var query = from b in db.Events //select all events
                                orderby b.StartTime //ordered by start time
                                select b;
                    foreach (var item in query) 
                    {
                        item.PrintEvent(); //interface is used to display each event
                    }
                }
                else
                {
                    Console.WriteLine("Events database is empty!");
                }
            }
        }
        public void internalEvents() //method displaying internal events
        {
            var db = new EventDatabase(); //database object
            using (db)
            {
                // view internal events
                Console.WriteLine("\n\tInternal events");
                var query = from b in db.Events.OfType<CalIntEvent>() //query database for internal events
                             orderby b.StartTime
                             select b;
                if (query.Count() > 0) //if event found, print
                {
                    foreach (var item in query) //print each row
                    {
                        item.PrintEvent();//interface is used to display each event
                    }
                }
                else
                {
                    Console.WriteLine("No internal events!");
                }
            }
        }
        public void externalEvents() //method displaying external events
        {
            var db = new EventDatabase();
            using (db)
            {
                // view external events
                Console.WriteLine("\n\tExternal events");
                var query = from b in db.Events.OfType<CalExtEvent>() //query database
                             orderby b.StartTime
                             select b;
                if (query.Count() > 0) //check database
                {
                    foreach (var item in query)
                    {
                        item.PrintEvent();//interface is used to display each event
                    }
                }
                else
                {
                    Console.WriteLine("No external events!");
                }
            }
        }
        public void clashingEvents() //method displaying clashing events
        {
            var db = new EventDatabase();
            using (db)
            {
                // view external events
                Console.WriteLine("\n\tClashing events");
                var query = db.Events.GroupBy(select => select.StartTime) //group events according to their StartTime
                     .Where(duplicate => duplicate.Count() > 1) //if more than one in the group means that they are duplicates
                     .SelectMany(duplicate => duplicate); //select all duplicates

                if(query.Count() > 0) 
                {
                    foreach (var item in query)
                    {
                        item.PrintEvent();//print the duplicates
                    }
                }
            }
        }
    }
}