using System;
using System.Collections.Generic;
using System.Text;

namespace CICA
{
    class CalIntEvent : Event
    {
        public int RoomNumber { get; set; } //auto-implemented property

        public override void PrintEvent() //new implementation of the method inherited from the print base class
        {   
            Console.WriteLine("\nRoom Number: " + RoomNumber);
            base.PrintEvent();
        }
        public override void SaveEvent() //new implementation of the method inherited from the SaveEvent base class
        {
            base.SaveEvent();
            Console.WriteLine("Internal event saved!");
        }
    }
}