using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
{
    public class NeuralNetwork
    {
        public NeuralNetwork(List<Layer> layers)
        {
            Layers = layers;
        }

        /// <summary>
        /// Gets or sets the layers.
        /// </summary>
        /// <value>
        /// The layers.
        /// </value>
        public List<Layer> Layers { get; set; }

        /// <summary>
        /// Gets or sets the learning rate.
        /// </summary>
        /// <value>The learning rate.</value>
        public float LearningRate { get; set; } = 0.01f;

        /// <summary>
        /// Initializes this neural Network.
        /// </summary>
        /// <remarks>
        /// This Should be called when the Neural network is created, else you will get errors
        /// </remarks>
        public void Init()
        {
            Random random = new Random(DateTime.Today.Millisecond);

            // init Biases
            /*
                foreach (Neuron neuron in Layers[1].Neurons)
                {
                    neuron.Bias = (float)random.NextDouble();
                }
            */

            // init weights
            for (int i = 1; i < Layers.Count - 1; i++)
            {
                Layers[i].InitNeuronsRandWeight(Layers[i]);
                Layers[i].NextLayer = Layers[i + 1];
            }
        } // Init

        /// <summary>
        /// Trains the Neural Network with the specified data.
        /// </summary>
        /// <param name="data">The data to train with. (For input layer)</param>
        /// <param name="targets">The expected values in the output layer.</param>
        public void Train(float[] data, float[] targets)
        {
            FeedForeward(data);

            BackProp(targets);

            UpdateWeights();
        }

        /// <summary>
        /// Back propagates the error.
        /// </summary>
        /// <param name="targets">The target values of each neuron in output layer.</param>
        private void BackProp(float[] targets)
        {
            Layer outputLayer = Layers.Last();

            // Specify the targets for the output neurons
            for (int i = 0; i < outputLayer.Neurons.Count; i++)
            {
                (outputLayer.Neurons[i] as OutputNeuron).CurrentTarget = targets[i];
            }

            // Calculate error and store it in the neuron
            foreach (Neuron neuron in outputLayer.Neurons)
            {
                (neuron as ICalculateError).CalcError();
            }

            // For all the hidden layers calculate the targets
            // and store them in the neurons itself.
            for (int i = 1; i < Layers.Count - 1; i++)
            {
                foreach (Neuron hiddenNeuron in Layers[i].Neurons)
                {
                    (hiddenNeuron as HiddenNeuron).CalcTarget(Layers[i].NextLayer);
                }
            }

            // For all the hidden layers calculate the errors
            for (int i = 1; i < Layers.Count - 1; i++)
            {
                foreach (Neuron hiddenNeuron in Layers[i].Neurons)
                {
                    (hiddenNeuron as ICalculateError).CalcError();
                }
            }
        }

        /// <summary>
        /// Feeds the data to the first layer and that feeds to the next layer
        /// untill ouput layer is reached.
        /// </summary>
        /// <param name="data">The input data for the NN.</param>
        /// <returns>The activations in last layer (output layer)</returns>
        private float[] FeedForeward(float[] data)
        {
            var i = 0;
            foreach (Neuron neuron in Layers.First().Neurons)
            {
                if (!neuron.IsBiased)
                {
                    neuron.Activation = data[i];
                    i++;
                }
            }
            for (Layer layer = Layers[1]; layer != null; layer = layer.NextLayer)
            {
                layer.FeedForeward();
            }
            var outputLayer = Layers.Last();
            var returnData = new float[outputLayer.Neurons.Count];
            for (i = 0; i < outputLayer.Neurons.Count; i++)
            {
                returnData[i] = outputLayer.Neurons[i].Activation;
            }
            return returnData;
        }

        /// <summary>
        /// Updates the weights with the back propagated error.
        /// </summary>
        private void UpdateWeights()
        {
            for (int i = 1; i < Layers.Count; i++)
            {
                for (int j = Layers[i].Neurons.Count - 1; j >= 1; j--)
                {
                    ICalculateError neuron = Layers[i].Neurons[j] as ICalculateError;
                    if (!Layers[i].Neurons[j].IsBiased)
                    {
                        Layer previousLayer = Layers[i - 1];
                        var a = (-2) * (neuron.CurrentTarget - neuron.CurrentError);
                        foreach (Connection conn in (neuron as Neuron).Connections)
                        {
                            // This is complicated maths. 😅
                            // Be advised this just works. 😀
                            double b = (1 - Math.Pow(Math.Tanh(conn.NeuronFrom.Activation * conn.Weight), 2)) * conn.NeuronFrom.Activation;
                            double derivative = a * b;
                            double toAdd = (-1) * (LearningRate * derivative);
                            conn.Weight += (float)toAdd;
                        }
                    }
                }
            }
        }

        public Neuron Predict(float[] data)
        {
            FeedForeward(data);
            Neuron max = Layers.Last().Neurons[0];
            foreach (Neuron neuron in Layers.Last().Neurons)
            {
                if (neuron.Activation >= max.Activation)
                {
                    max = neuron;
                }
            }
            return max;
        }
    }
}