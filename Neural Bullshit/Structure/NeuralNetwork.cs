using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetwork.Functions;

namespace NeuralNetwork.Structure
{
    class NeuralNetwork
    {
        private List<Layer> Network;
        public Layer InputLayer { get; set; }
        public Layer OutputLayer { get; set; }
        public NeuralNetwork(IEnumerable<int> topology)
        {
            Network = new List<Layer>();
            InputLayer = new Layer(topology.First());
            Network.Add(InputLayer);
            var prevLayer = InputLayer;
            {
                foreach (int size in topology.Skip(1).Take(topology.Count() - 2))
                {
                    Network.Add(new Layer(size));
                    prevLayer.FullyConnect(Network.Last());
                    prevLayer = Network.Last();
                }
            }
            OutputLayer = new Layer(topology.Last(), true);
            prevLayer.FullyConnect(OutputLayer);
            Network.Add(OutputLayer);
        }

        public void AddBinaryOutput()
        {
            var newOutputLayer = new Layer();
            foreach (Neuron neuron in OutputLayer.Neurons)
            {
                var newBinaryOutputNeuron = new Neurons.BinaryOutputNeuron();
                newOutputLayer.AddNeuron(newBinaryOutputNeuron);
                neuron.AddOutput(newBinaryOutputNeuron);
            }
            OutputLayer.NextLayer = newOutputLayer;
            newOutputLayer.PrevLayer = OutputLayer;
            OutputLayer = newOutputLayer;
        }
    }
}
