using System;
using Figgle; //ascii banner

namespace CICA
{
    public class Menu //calendar menu class
    {
        public void calendarMenu() //menu method
        {
            Console.Title = "Retro Calendar"; //Console title
            Console.BackgroundColor = ConsoleColor.Black; //console background colour
            Console.WriteLine("\n\n\n");
            Console.ForegroundColor = ConsoleColor.DarkGray; //text colour
            Console.WriteLine(FiggleFonts.CyberMedium.Render(" Retro  Calendar")); // ascii banner large font
            Console.ResetColor(); //reset text colour to white 
            string mainMenu = "\n\n\t\t Hotel Retro calendar\n\n";  //calendar menu 
            mainMenu += " 1 See all events\n";
            mainMenu += " 2 Internal events\n";
            mainMenu += " 3 External events\n";
            mainMenu += " 4 Create new event\n";
            mainMenu += " 5 Update event\n";
            mainMenu += " 6 Delete event\n";
            mainMenu += " 7 Overlapping events\n\n";
            mainMenu += " 0 Exit\n\n";
            Console.WriteLine(mainMenu); //display menu
            Console.Write(" Enter menu option: ");
        }
    }
}