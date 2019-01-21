using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Neurons
{
    class BinaryOutputNeuron : Neuron
    {
        public readonly string Name;
        public BinaryOutputNeuron() : base()
        {
            Name = "Bias";
            ActivationFunction = new Functions.ActivationFunctions.Binary();
        }
    }
}
