using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
{
    public class InputNeuron : Neuron
    {
        public InputNeuron(string id) : base(id)
        {
        }

        public InputNeuron() : base("Input Neuron")
        {
        }
    }
}