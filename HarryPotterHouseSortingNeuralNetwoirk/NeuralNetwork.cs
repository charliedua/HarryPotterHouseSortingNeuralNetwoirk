using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarryPotterHouseSortingNeuralNetwoirk
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
        /// Feeds the data to the first layer and that feeds to the next layer
        /// untill ouput layer is reached.
        /// </summary>
        /// <param name="data">The input data for the NN.</param>
        /// <returns>The activations in last layer (output layer)</returns>
        public float[] FeedForeward(float[] data)
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

        public void BackProp(float[] targets)
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

        public float LearningRate { get; set; } = 0.01f;

        public void Train()
        {
            for (int i = 1; i < Layers.Count; i++)
            {
                for (int j = Layers[i].Neurons.Count - 1; j >= 1; j--)
                {
                    ICalculateError neuron = Layers[i].Neurons[j] as ICalculateError;
                    if (!Layers[i].Neurons[j].IsBiased)
                    {
                        Layer previousLayer = Layers[i - 1];
                        var a = -2 * (neuron.CurrentTarget - neuron.CurrentError);
                        foreach (Connection conn in (neuron as Neuron).Connections)
                        {
                            double b = (1 - Math.Pow(Math.Tanh(conn.NeuronFrom.Activation * conn.Weight), 2)) * conn.NeuronFrom.Activation;
                            double derivative = a * b;
                            double toAdd = -1 * (LearningRate * derivative);
                            conn.Weight += (float)toAdd;
                        }
                    }
                }
            }
        }

        //{
        //    foreach (Student student in students)
        //    {
        //        // input layer stuff
        //        Layers[0].Neurons[0].Activation = student.Trait.courage;
        //        Layers[0].Neurons[1].Activation = student.Trait.humor;
        //        Layers[0].Neurons[2].Activation = student.Trait.height;
        //        Layers[0].Neurons[3].Activation = 1;

        //        GiveInputToNext(1);
        //        GiveInputToNext(2);

        //        Neuron maxNeuron = null;
        //        float max = 0.0f;
        //        foreach (Neuron neuron in Layers[2].Neurons)
        //        {
        //            if (max < neuron.Activation)
        //            {
        //                max = neuron.Activation;
        //                maxNeuron = neuron;
        //            }
        //        }

        //        OutputNeuron outMaxNeuron = maxNeuron as OutputNeuron;

        //        // actual != expected
        //        if (outMaxNeuron.TargetHouse != student.house)
        //        {
        //            float target = 1.0f;
        //            Neuron outputNeuron = Layers[2].Neurons.Find(x => (x as OutputNeuron).TargetHouse == student.house);
        //            float output = outputNeuron.Activation;
        //            List<float> errors = new List<float>();
        //            float error = target - output;
        //            errors.Add(target - output);
        //            foreach (Neuron neuron in Layers[2].Neurons)
        //            {
        //                if (neuron.ID != outputNeuron.ID)
        //                {
        //                    error += 0 - output;
        //                }
        //            }
        //        }
        //    }
        //}

        //private void GiveInputToNext(int layerNumber)
        //{
        //    foreach (Neuron neuron in Layers[layerNumber].Neurons)
        //    {
        //        float val = 0.0f;

        //        foreach (var connection in neuron.Connections)
        //        {
        //            val += connection.Weight * connection.NeuronFrom.Activation;
        //        }

        //        val += neuron.Bias;

        //        neuron.Activation = Activate(val);
        //    }
        //}

        public void Init()
        {
            Random random = new Random(DateTime.Today.Millisecond);

            // init Biases
            //foreach (Neuron neuron in Layers[1].Neurons)
            //{
            //    neuron.Bias = (float)random.NextDouble();
            //}

            // init weights
            for (int i = 1; i < Layers.Count; i++)
            {
                Layers[i].InitNeuronsRandWeight(Layers[i - 1]);
            }
        }
    }
}