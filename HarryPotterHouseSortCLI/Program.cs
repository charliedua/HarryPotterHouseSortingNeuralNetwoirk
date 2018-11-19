using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarryPotterHouseSortingNeuralNetwoirk;

namespace HarryPotterHouseSortCLI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Layer inputLayer = new Layer(new List<Neuron>()
            {
                new Neuron(0.5f, 0),
                new Neuron(0.5f, 1),
                new Neuron(1.0f, 2){ IsBiased = true }
            });

            Layer hiddenLayer = new Layer(new List<Neuron>()
            {
                new Neuron(0.5f, 3),
                new Neuron(0.5f, 4),
                new Neuron(0.5f, 5),
                new Neuron(1.0f, 6){ IsBiased = true }
            });

            Layer outputLayer = new Layer(new List<Neuron>()
            {
                new Neuron(0.5f, 7), // Hupplepuff
                new Neuron(0.5f, 8), // Grifindor
                new Neuron(0.5f, 9), // Slythrin
                new Neuron(0.5f, 10) // Ravenclaw
            });

            Network network = new Network(new List<Layer>() { inputLayer, hiddenLayer, outputLayer });

            Student student = new Student("harry", new Trait() { courage = 1, height = 5.75f, humor = 0.65f });

            network.FeedForeward(student);
        }
    }
}