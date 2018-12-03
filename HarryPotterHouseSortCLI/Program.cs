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
                new InputNeuron(),
                new InputNeuron(),
                new InputNeuron(),
                new InputNeuron(){ IsBiased = true }
            });

            Layer hiddenLayer = new Layer(new List<Neuron>()
            {
                new HiddenNeuron(),
                new HiddenNeuron(),
                new HiddenNeuron(),
                new HiddenNeuron(),
                new HiddenNeuron(){ IsBiased = true }
            });

            Layer outputLayer = new Layer(new List<Neuron>()
            {
                new OutputNeuron("Hupplepuff"), // Hupplepuff
                new OutputNeuron("Grifindor"),  // Grifindor
                new OutputNeuron("Slythrin"),   // Slythrin
                new OutputNeuron("Ravenclaw")   // Ravenclaw
            });

            inputLayer.NextLayer = hiddenLayer;
            hiddenLayer.NextLayer = outputLayer;

            NeuralNetwork network = new NeuralNetwork(new List<Layer>() { inputLayer, hiddenLayer, outputLayer });

            Student student = new Student("harry", new Trait() { courage = 1, height = 5.75f, humor = 0.65f }) { house = HogwartsHouse.Griffindor };

            network.Init();

            Console.WriteLine(network.FeedForeward(new float[] { student.Trait.courage, student.Trait.height, student.Trait.humor }));
        }
    }
}