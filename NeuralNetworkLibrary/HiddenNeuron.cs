using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
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
        public float CurrentTarget { get; set; }

        public void CalcError()
        {
            CurrentError = (float)Math.Pow(Math.Abs(CurrentTarget) - Math.Abs(Activation), 2);
        }

        public void CalcTarget(Layer nextLayer)
        {
            // All outgoing Connections from this neuron.
            List<Connection> conns = new List<Connection>();
            foreach (Neuron neuron in nextLayer.Neurons)
            {
                conns.Add(neuron.Connections.Find(x => x.NeuronFrom == this));
            }

            // calculate the sum of all weights
            var sum = 0f;
            foreach (var conn in conns)
            {
                if (conn != null)
                {
                    sum += conn.Weight;
                }
            }

            // Calculates the Total error
            float totalError = 0f;
            for (int i = 0; i < nextLayer.Neurons.Count; i++)
            {
                if (conns[i] != null)
                {
                    totalError += (conns[i].Weight / sum) * (nextLayer.Neurons[i] as ICalculateError).CurrentError;
                }
            }

            CurrentError = totalError;
        }
    }
}