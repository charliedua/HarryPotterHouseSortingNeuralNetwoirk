﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
{
    public class OutputNeuron : Neuron, ICalculateError
    {
        public OutputNeuron(string id) : base(id)
        {
        }

        public OutputNeuron() : base("Output Neuron")
        {
        }

        public float CurrentError { get; set; }

        public float CurrentTarget { get; set; }

        /// <summary>
        /// Calculates the error.
        /// </summary>
        /// <param name="prevLayer">The previous layer.</param>
        public void CalcError()
        {
            CurrentError = (float)Math.Pow(CurrentTarget - Activation, 2);
        }
    }
}