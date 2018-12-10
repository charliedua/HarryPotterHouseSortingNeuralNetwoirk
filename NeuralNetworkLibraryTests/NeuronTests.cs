using Microsoft.VisualStudio.TestTools.UnitTesting;
using HarryPotterHouseSortingNeuralNetwoirk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarryPotterHouseSortingNeuralNetwoirk.Tests
{
    [TestClass()]
    public class NeuronTests
    {
        private Neuron neuron = new Neuron(0.5f, 0);
        private Neuron neuronTo = new Neuron(0.75f, 1);

        [TestInitialize]
        public void Setup()
        {
            neuron.ClearConnections();
        }

        [TestMethod()]
        public void AddOrUpdateConnectionTest()
        {
            neuron.AddOrUpdateConnection(neuronTo, 0.4f);
            Assert.Fail();
        }
    }
}