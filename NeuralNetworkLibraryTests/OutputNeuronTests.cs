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
    public class OutputNeuronTests
    {
        private OutputNeuron neuron;
        private OutputNeuron neuron2;

        /// <summary>
        /// Setups the varibles so that each function has a fresh set of neurons.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            neuron = new OutputNeuron("A");
            neuron2 = new OutputNeuron("B");
        }

        [TestMethod()]
        public void CalcErrorTest()
        {
            neuron.Activation = 0.8f;
            neuron.CurrentTarget = 1.0f;
            neuron.CalcError();
            float expected = 0.04f;
            float actual = neuron.CurrentError;

            Assert.AreEqual(expected, actual);
        }
    }
}