using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
{
    public class Connection
    {
        public Connection(Neuron neuronFrom, float weight)
        {
            NeuronFrom = neuronFrom;
            Weight = weight;
        }

        /// <summary>
        /// Gets or sets the neuron to.
        /// </summary>
        /// <value>
        /// The neuron to.
        /// </value>
        public Neuron NeuronFrom { get; set; }

        public float Weight { get; set; }
    }
}