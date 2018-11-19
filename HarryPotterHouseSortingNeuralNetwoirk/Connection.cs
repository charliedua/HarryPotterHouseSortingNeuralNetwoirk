using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarryPotterHouseSortingNeuralNetwoirk
{
    public class Connection
    {
        public Connection(Neuron neuronTo, float weight)
        {
            NeuronTo = neuronTo;
            Weight = weight;
        }

        /// <summary>
        /// Gets or sets the neuron to.
        /// </summary>
        /// <value>
        /// The neuron to.
        /// </value>
        public Neuron NeuronTo { get; set; }

        public float Weight { get; set; }
    }
}