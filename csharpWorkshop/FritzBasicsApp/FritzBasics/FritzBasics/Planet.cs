using System;
using System.Collections.Generic;
using System.Text;

namespace FritzBasics
{
    public class Planet
    {
        public string Name { get; set; }
        public double Mass { get; set; }
        public Planet()
        {
            // default constructor - so that we can say "new Planet()"
            // Note that we are not passing in any arguments (values).
            // The default constructors has 0 parameters.
        }

        public Planet(string Name)
        {
            // If we know a planet's name but not its mass, we
            // can create a Planet instance and set this new instance's
            // Name property to the value we are passing in
            // Example: Planet earth = new Planet("Earth");

            // we use the "this" keyword to be more clear as to which "Name" we are working with
            // "this" instance's Name property is getting set to the Name variable (parameter)'s value
            this.Name = Name; 

        }

        public Planet(string planetName, double planetMass)
        {
            // If we know a planet's name and its mass, we
            // can create a Planet instance and set this new instance's
            // properties to the value we are passing in
            // Example: Planet earth = new Planet("Earth");

            // We are not using the "this" keyword, because it is a bit more clear
            // as to which variable is a property and which variable is the parameter passed in

            // Either way, use meaningful names to make your code readable by others

            // Example: Planet mars = new Planet("Mars", 0.642);
            Name = planetName;
            Mass = planetMass;
        }

    }
}
