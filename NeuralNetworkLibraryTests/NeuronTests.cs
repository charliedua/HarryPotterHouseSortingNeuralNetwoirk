using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetworkLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary.Tests
{
    [TestClass()]
    public class NeuronTests
    {
        private Neuron neuron;
        private Neuron neuronFrom;

        /// <summary>
        /// Setups the varibles so that each function has a fresh set of neurons.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            neuron = new InputNeuron();
            neuronFrom = new InputNeuron();
        }

        /// <summary>
        /// Test for checking if the connection from one neuron can be established with a specified weight.
        /// </summary>
        [TestMethod()]
        public void AddConnectionTest()
        {
            neuron.AddConnection(neuronFrom, 0.4f);
            var expected = 0.4f;
            var actual = neuron.Connections[0].Weight;

            // checks if the weight has been updated.
            Assert.AreEqual(expected, actual);

            var expectedNeuron = neuronFrom;
            var actualNeuron = neuron.Connections[0].NeuronFrom;

            // checks if the neuron is stored.
            Assert.AreEqual(expectedNeuron, actualNeuron);
        }

        /// <summary>
        /// tries to add a conneciton when there is another connection to the same neuron.
        /// </summary>
        [TestMethod]
        public void AddConnectionAlreadyExists()
        {
            neuron.Connections.Add(new Connection(neuronFrom, 0.4f));

            neuron.AddConnection(neuronFrom, 0.8f);

            // checks if there is only one connection. If more than one connection then the
            // connection is being added to the list which is wrong
            Assert.IsTrue(neuron.Connections.Count == 1);

            var expected = 0.4f;
            var actual = neuron.Connections[0].Weight;

            // checks if the weight has not been updated.
            Assert.AreEqual(expected, actual);
        }
    }
}

/*
    var expected;
    var actual;
*/