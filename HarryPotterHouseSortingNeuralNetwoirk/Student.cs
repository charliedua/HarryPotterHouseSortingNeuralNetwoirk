using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarryPotterHouseSortingNeuralNetwoirk
{
    public class Student
    {
        public Student(string name, Trait trait)
        {
            Trait = trait;
            Name = name;
        }

        public Trait Trait { get; }

        public string Name { get; set; }

        public HogwartsHouse house;
    }
}