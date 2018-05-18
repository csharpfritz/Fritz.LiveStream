using System;
using System.Collections.Generic;
using System.Text;

namespace FritzBasics
{
    class FritzApp
    {
        public FritzApp()
        {

        }

        public void Run()
        {
            string userInput = "";
            do
            {
                DisplayMenu();
                userInput = Console.ReadLine();
                ProcessMenu(userInput);

            } while (userInput.ToUpper() != "Q");
            Console.WriteLine("Happy coding! Press any key to quit!");
            Console.ReadKey();
        }

        public void ProcessMenu(string menuChoice)
        {
            switch (menuChoice)
            {
                case "1":
                    HelloYou("Sadukie");
                    break;
                case "2":
                    GetLunchIfElse();
                    break;
                case "3":
                    GetLunchSwitch();
                    break;
                case "4":
                    FlipCoin();
                    break;
                case "5":
                    GoHide();
                    break;
                case "6":
                    AllHid();
                    break;
                case "7":
                    LaunchRocket();
                    break;
                case "8":
                    GetSpaceCollections();
                    break;
                default:
                    if (menuChoice.ToUpper() != "Q")
                    {
                        Console.WriteLine("Not a valid menu option.\nPlease enter a valid choice.\n");
                    }
                    break;
            }
        }

        public void DisplayMenu()
        {
            Console.WriteLine("Fritz Basics");
            Console.WriteLine("*************");
            Console.WriteLine("1. Hello World");
            Console.WriteLine("2. What's For Lunch - If...Else");
            Console.WriteLine("3. What's For Lunch - Switch");
            Console.WriteLine("4. Football Flip - If...Else with Random");
            Console.WriteLine("5. Hide and Seek - Loops");
            Console.WriteLine("6. All Hid - Loops");
            Console.WriteLine("7. Rocket Launch - Loops");
            Console.WriteLine("8. Space Adventures - Collections");
            Console.WriteLine("Enter a number or Q to quit:");
        }

        public void HelloYou(string name)
        {
            // String concatenation
            Console.WriteLine("Hello, " + name + "!");
            // String interpolation
            Console.WriteLine($"Hello {name}!");
        }

        public void GetLunchIfElse()
        {
            // Put the if ... else code here
            // If it's Tuesday - tacos
            // If it's Friday - pizza
            // If it's any other day - bacon
        }

        public void GetLunchSwitch()
        {
            // Put the switch code here          
            // If it's Tuesday - tacos
            // If it's Friday - pizza
            // If it's any other day - bacon
        }

        public void FlipCoin()
        {
            // Put the randomness here
            // Who will go first - Eagles (Jeff's team) or Browns (Sadukie's team)?
        }

        public void GoHide()
        {
            // In Hide-And-Seek, the counter has to count to a number to give people time to hide
            // Create a for-loop to do the counting to 20
        }

        public void AllHid()
        {
            // In Hide-And-Seek, there may be a song checking if they're all hid
            // "All hid, all hid, all hid... 5, 10, 15, 20 all hid"
            // Create a for-loop to show how to count up by 5s
        }

        public void LaunchRocket()
        {
            // When rockets launch, there's a countdown
            // Create a for-loop to show how to count down from 10
        }

        public void GetSpaceCollections()
        {
            // Let's talk about planet objects and values
            // Lots of statistics at https://nssdc.gsfc.nasa.gov/planetary/factsheet/
        }

    }
}

