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
                new OutputNeuron("0"),
                new OutputNeuron("1")
            });

            //Layer outputLayer = new Layer(new List<Neuron>()
            //{
            //    new OutputNeuron("Hupplepuff"), // Hupplepuff
            //    new OutputNeuron("Grifindor"),  // Grifindor
            //    new OutputNeuron("Slythrin"),   // Slythrin
            //    new OutputNeuron("Ravenclaw")   // Ravenclaw
            //});

            inputLayer.NextLayer = hiddenLayer;
            hiddenLayer.NextLayer = outputLayer;

            NeuralNetwork network = new NeuralNetwork(new List<Layer>() { inputLayer, hiddenLayer, outputLayer });

            network.Init();

            for (int i = 0; i < 5000; i++)
            {
                network.FeedForeward(new float[] { 0, 0 });
                network.BackProp(new float[] { 1, 0 });
                network.Train();

                network.FeedForeward(new float[] { 0, 1 });
                network.BackProp(new float[] { 0, 1 });
                network.Train();

                network.FeedForeward(new float[] { 1, 1 });
                network.BackProp(new float[] { 1, 0 });
                network.Train();

                // network.FeedForeward(new float[] { 1, 0 });
                // network.BackProp(new float[] { 0, 1 });
            }

            network.FeedForeward(new float[] { 1, 0 });

            var max = outputLayer.Neurons[0].Activation >= outputLayer.Neurons[1].Activation ? outputLayer.Neurons[0] : outputLayer.Neurons[1];

            Console.WriteLine("The network predicted {0}. Expected output 1", max.ID);
            /*
                Student student = new Student("harry", new Trait() { courage = 1, height = 5.75f, humor = 0.65f }) { house = HogwartsHouse.Griffindor };

                network.FeedForeward(new float[] { student.Trait.courage, student.Trait.height, student.Trait.humor });

                network.BackProp(new float[] { 0, 1, 0, 0 });
            */
        }
    }
}