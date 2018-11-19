using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarryPotterHouseSortingNeuralNetwoirk
{
    public class Network
    {
        public Network(List<Layer> layers)
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

        public void FeedForeward(Student student)
        {
            var Neurons = Layers.First().Neurons;
            Neurons[0].Value = student.Trait.courage;
            Neurons[1].Value = student.Trait.height;
            Neurons[2].Value = student.Trait.humor;
        }

        public void Train(List<Student> students)
        {
            foreach (Student student in students)
            {
                Layers[0].Neurons[0].Value = student.Trait.courage;
                Layers[0].Neurons[1].Value = student.Trait.humor;
                Layers[0].Neurons[2].Value = student.Trait.height;
                Layers[0].Neurons[3].Value = 1;

                GiveInputToNext(1);
                GiveInputToNext(2);

                Neuron maxNeuron = null;
                float max = 0.0f;
                foreach (Neuron neuron in Layers[2].Neurons)
                {
                    if (max < neuron.Value)
                    {
                        max = neuron.Value;
                        maxNeuron = neuron;
                    }
                }

                // actual != expected
                if (maxNeuron.ID != (int)student.house)
                {
                    float target = 1.0f;
                    Neuron outputNeuron = Layers[2].Neurons.Find(x => x.ID == (int)student.house);
                    float output = outputNeuron.Value;
                    float error = (float)Math.Pow(target - output, 2) / 2;
                    foreach (Neuron neuron in Layers[2].Neurons)
                    {
                        if (neuron.ID != outputNeuron.ID)
                        {
                            error += (float)Math.Pow(0 - output, 2) / 2;
                        }
                    }
                }
            }
        }

        private void GiveInputToNext(int layerNumber)
        {
            foreach (Neuron neuron in Layers[layerNumber].Neurons)
            {
                float val = 0.0f;

                foreach (var connection in neuron.Connections)
                {
                    val += connection.Weight * connection.NeuronTo.Value;
                }

                val += neuron.Bias;

                neuron.Value = Activate(val);
            }
        }

        public void Init()
        {
            Random random = new Random(DateTime.Today.Millisecond);

            // init Biases
            foreach (Neuron neuron in Layers[1].Neurons)
            {
                neuron.Bias = (float)random.NextDouble();
            }

            // init weights
            for (int i = 1; i < Layers.Count; i++)
            {
                Layers[i].InitNeurons(Layers[i - 1]);
            }
        }

        private static float Activate(float input)
        {
            return (float)Math.Tanh(input);
        }
    }
}