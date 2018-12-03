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

        //public void Train(List<Student> students)
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