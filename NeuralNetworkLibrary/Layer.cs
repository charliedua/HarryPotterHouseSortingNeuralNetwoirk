// This Code was written by Anmol Dua,
// For fun 👨🏻‍💻

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extreme.Mathematics;

namespace NeuralNetworkLibrary
{
    public class Layer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Layer"/> class with the provided neurons.
        /// </summary>
        /// <param name="neurons">The neurons.</param>
        public Layer(List<Neuron> neurons)
        {
            Neurons = neurons;
        }

        private static float Activate(float input)
        {
            return (float)Math.Tanh(input);
        }

        /// <summary>
        /// Represents the next layer
        /// </summary>
        public Layer NextLayer { get; set; }

        /// <summary>
        /// Feeds the data foreward.
        /// </summary>
        public void FeedForeward()
        {
            foreach (Neuron neuron in Neurons)
            {
                float WeightedSum = 0f;

                // For each incomming connection in this neuron take its weight and multiply it with
                // the associated neuron's activation to calculate the Weighted Sum.
                foreach (Connection connection in neuron.Connections)
                {
                    WeightedSum += connection.Weight * connection.NeuronFrom.Activation;
                }

                // Set the activation of this neuron to the activated Weighted Sum calculated above.
                neuron.Activation = 1 - Math.Abs(Activate(WeightedSum));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Layer"/> class with an empty set of neurons.
        /// </summary>
        public Layer()
        {
            Neurons = new List<Neuron>();
        }

        public List<Neuron> Neurons { get; }

        /// <summary>
        /// Adds connections from the layer to this layer with random weights.
        /// 🤞 it works
        /// </summary>
        /// <param name="prevLayer">The previous layer.</param>
        public void InitNeuronsRandWeight(Layer prevLayer)
        {
            Random random = new Random(DateTime.Today.Millisecond);

            // will save the incomming connections from all the neurons from previous layer.
            foreach (Neuron thisNeuron in Neurons)
            {
                // The previous layer neurons.
                foreach (Neuron prevNeuron in prevLayer.Neurons)
                {
                    // add a connection to neuron from previous layer with random weight.
                    if (!thisNeuron.IsBiased)
                    {
                        thisNeuron.AddConnection(prevNeuron, (float)random.NextDouble() * 2 - 1);
                    }
                }
            }
        }
    }
}