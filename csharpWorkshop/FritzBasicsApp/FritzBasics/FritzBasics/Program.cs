using System;

namespace FritzBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            // This calls to the FritzApp constructor
            // which creates a new instance of the FritzApp class.            
            FritzApp myApp = new FritzApp();
            // With this instance, we can access any methods in FritzApp with the 
            // "public" access modifier.
            myApp.Run();
            Console.WriteLine("Closing....");
            Console.ReadKey();
        }
    }
}
