using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarryPotterHouseSortingNeuralNetwoirk
{
    public class HiddenNeuron : Neuron, ICalculateError
    {
        public HiddenNeuron(string id) : base(id)
        {
        }

        public HiddenNeuron() : base("Hidden Neuron")
        {
        }

        public float CurrentError { get; set; }

        public float CalcError(float target)
        {
            return target - Activation;
            //
            //foreach (Neuron neuron in prevLayer.Neurons)
            //{
            //    var conns = neuron.Connections.FindAll(x => x.NeuronFrom == this);
            //foreach (var conn in conns)
            //{
            //    conn.Weight
            //    }
            //}
            //return 0;
        }

        public float CalcTarget(Layer nextLayer)
        {
            List<Connection> conns = new List<Connection>();
            foreach (Neuron neuron in nextLayer.Neurons)
            {
                conns.Add(neuron.Connections.Find(x => x.NeuronFrom == this));
            }

            // calculate the sum of all weights
            var sum = 0f;
            foreach (var conn in conns)
            {
                sum += conn.Weight;
            }

            float error1 = 0f;
            for (int i = 0; i < nextLayer.Neurons.Count; i++)
            {
                error1 = conns[i].Weight / sum * (nextLayer.Neurons[i] as ICalculateError).CurrentError;
            }

            return error1;
        }
    }
}