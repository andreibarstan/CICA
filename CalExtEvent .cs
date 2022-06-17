using System;
using System.Collections.Generic;
using System.Text;

namespace CICA
{
    class CalExtEvent : Event //derived class inheriting from base class Event
    {
        public int CustID { get; set; } //property

        public override void PrintEvent() //additional implementation to the base class PrintEvent
        {
            Console.WriteLine("\nCustomer ID: " + CustID); 
            base.PrintEvent();
        }
        public override void SaveEvent()//additional implementation to the base class SaveEvent
        {
            base.SaveEvent();
            Console.WriteLine("External event saved!");
        }
    }
}