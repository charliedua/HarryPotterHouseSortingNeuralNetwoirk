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
                new Neuron(0.5f),
                new Neuron(0.5f)
            });
            Layer hiddenLayer = new Layer(new List<Neuron>()
            {
                new Neuron(0.5f),
                new Neuron(0.5f),
                new Neuron(0.5f)
            });
            Layer outputLayer = new Layer(new List<Neuron>()
            {
                new Neuron(0.5f),
                new Neuron(0.5f),
                new Neuron(0.5f),
                new Neuron(0.5f)
            });
            Network network = new Network(new List<Layer>() { inputLayer, hiddenLayer, outputLayer });
        }
    }
}