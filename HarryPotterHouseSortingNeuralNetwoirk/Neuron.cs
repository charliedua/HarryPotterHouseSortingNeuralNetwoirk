using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarryPotterHouseSortingNeuralNetwoirk
{
    public class Neuron
    {
        public Neuron(float value, int id)
        {
            Value = value;
            ID = id;
        }

        public int ID { get; }

        private float _value;

        public float Bias { get; set; }

        public bool IsBiased { get; set; } = false;

        public float Value { get => _value; set => _value = value; }
        public List<Connection> Connections { get; set; } = new List<Connection>();

        public void AddOrUpdateConnection(Neuron neuron, float weight)
        {
            if (Connections.Find(x => x.NeuronTo == neuron) == null) // couldn't find
            {
                Connections.Add(new Connection(neuron, weight));
            }
            else if (Connections.Find(x => x.NeuronTo.ID == neuron.ID).Weight != weight) // Found
            {
                Connections.Find(x => x.NeuronTo == neuron).Weight = weight;
            }
        }

        public void ClearConnections()
        {
            Connections = new List<Connection>();
        }
    }
}