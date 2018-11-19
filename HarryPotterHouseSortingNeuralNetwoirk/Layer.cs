using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarryPotterHouseSortingNeuralNetwoirk
{
    public class Layer
    {
        public int ID { get; set; }

        public Layer(List<Neuron> neurons)
        {
            Neurons = neurons;
        }

        public List<Neuron> Neurons { get; }

        public void InitNeurons(Layer prevLayer)
        {
            Random random = new Random(DateTime.Today.Millisecond);

            // will store the incomming connections
            foreach (Neuron prevNeuron in prevLayer.Neurons)
            {
                foreach (Neuron thisNeuron in Neurons)
                {
                    // add a connection to neuron from previous layer
                    thisNeuron.AddOrUpdateConnection(prevNeuron, (float)random.NextDouble());
                }
            }
        }
    }
}