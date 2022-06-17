using System;
using System.ComponentModel.DataAnnotations;

namespace CICA
{
    public abstract class Event : ICalEventPrint, ICalEventSave //class that introduces dependency through the interfaces
    {
        [Key]
        //auto-implemented properties 
        public int EventID { get; set; } //primary-key identified as it contains the key "ID"
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public virtual void PrintEvent() //Print
        {
            Console.WriteLine("----------------------------"); //event printing layout configuration
            DateTime endTime = StartTime + Duration; //event finish time
            Console.WriteLine("EventID: " + EventID +
                            "\nEvent title: " + Title + 
                            "\nDescription: " + Description + 
                            "\nLocation: " + Location + 
                            "\nStart time: " + StartTime + 
                            "\nDuration: " + Duration + 
                            "\nEnd time: " + endTime); 
            Console.WriteLine("----------------------------\n");
        }
        public virtual void SaveEvent() //base class of save interface 
        {
            Console.WriteLine("\nNew event is saving...");
        }       
    }
}