using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
{
    /// <summary>
    /// The output neuron used to create neurons in the output layer.
    /// Note: must be used when creating the last layer in the neural network.
    /// </summary>
    /// <seealso cref="NeuralNetworkLibrary.Neuron"/>
    /// <seealso cref="NeuralNetworkLibrary.ICalculateError"/>
    public class OutputNeuron : Neuron, ICalculateError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OutputNeuron"/> class with custom identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public OutputNeuron(string id) : base(id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputNeuron"/> class.
        /// </summary>
        public OutputNeuron() : base("Output Neuron")
        {
        }

        /// <summary>
        /// Gets or sets the current error.
        /// </summary>
        /// <value>
        /// The current error.
        /// </value>
        public float CurrentError { get; set; }

        /// <summary>
        /// Gets or sets the current target.
        /// </summary>
        /// <value>The current target.</value>
        public float CurrentTarget { get; set; }

        /// <summary>
        /// Calculates the error.
        /// </summary>
        public void CalcError()
        {
            CurrentError = (float)Math.Round(Math.Pow(CurrentTarget - Activation, 2), 2);
        }
    }
}