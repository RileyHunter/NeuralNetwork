using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Structure.Neurons
{
    class BiasNeuron : Neuron
    {
        public readonly string Name;
        public BiasNeuron() : base()
        {
            Activity = 1;
            Name = "Bias";
            ActivationFunction = new Functions.ActivationFunctions.Linear();
        }

        override public void AddInput(Synapse newInput)
        {
            Input = new List<Synapse>();
        }
    }
}
