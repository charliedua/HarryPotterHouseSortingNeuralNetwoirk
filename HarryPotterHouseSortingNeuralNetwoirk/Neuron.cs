using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarryPotterHouseSortingNeuralNetwoirk
{
    public class Neuron
    {
        public Neuron(float bias)
        {
            Bias = bias;
        }

        public float Bias { get; set; }
    }
}