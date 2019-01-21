using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetwork.Functions;
using NeuralNetwork.Functions.ActivationFunctions;
using NeuralNetwork.Functions.InputFunctions;

namespace NeuralNetwork.Structure
{
    class Layer
    {
        public List<Neuron> Neurons { get; set; }
        public Layer NextLayer { get; set; }
        public Layer PrevLayer { get; set; }
        public int Size { get; set; }
        private bool hasBias;

        public Layer(int? size = null, bool isOutput = false)
        {
            Neurons = new List<Neuron>();
            if (size != null)
            {
                if (isOutput)
                {
                    Populate((int)size);
                }
                else
                {
                    PopulateWithBias((int)size);
                }

            }
        }
        
        public void AddNeuron(Neuron neuron)
        {
            Neurons.Add(neuron);
            Size++;
        }

        public void Populate(int size, AbstractInputFunction inputFunction = null, AbstractActivationFunction activationFunction = null)
        {
            for(int toFill = size - Neurons.Count(); toFill > 0; toFill--)
            {
                Neurons.Add(new Neuron(inputFunction, activationFunction));
            }
            Size = Neurons.Count();
        }

        public void PopulateWithBias(int size, AbstractInputFunction inputFunction = null, AbstractActivationFunction activationFunction = null)
        {
            var bias = new Neurons.BiasNeuron();
            Populate(size, inputFunction, activationFunction);
            AddNeuron(bias);
            hasBias = true;
        }

        public void Activate()
        {
            foreach (Neuron neuron in Neurons)
            {
                neuron.Activate();
            }
        }

        public void Activate(double inputActivity)
        {
            foreach (Neuron neuron in Neurons)
            {
                neuron.Activate(inputActivity);
            }
        }

        public void Activate(IEnumerable<int> inputs)
        {
            Activate(inputs.Select(input => (double)input));
        }

        public void Activate(IEnumerable<double> inputs)
        {
            if (inputs.Count() != Size && !(inputs.Count() == Size + 1 || hasBias))
            {
                throw new Exception("[ERROR] Input vector must be identical in magnitude to layer size");
            }
            foreach (var neuronInputPair in Neurons.Zip(inputs, (neuron, input) => (neuron, input)))
            {
                neuronInputPair.neuron.Activate(neuronInputPair.input);
            }
        }

        public void FeedForward()
        {
            Activate();
            ActivateNextLayer();
        }

        public void FeedForward(IEnumerable<int> inputs)
        {
            FeedForward(inputs.Select(input => (double)input));
        }

        public void FeedForward(IEnumerable<double> inputs)
        {
            Activate(inputs);
            ActivateNextLayer();
        }

        public void ActivateNextLayer()
        {
            if(NextLayer != null)
            {
                NextLayer.FeedForward();
            }
        }

        public void FullyConnect(Layer outputLayer)
        {
            foreach (Neuron neuron in Neurons)
            {
                foreach (Neuron outputNeuron in outputLayer.Neurons)
                {
                    neuron.AddOutput(outputNeuron);
                }
            }
            NextLayer = outputLayer;
            NextLayer.PrevLayer = this;
            RandomiseWeights();
        }

        public void FullyConnect(Neuron outputNeuron)
        {
            foreach (Neuron neuron in Neurons)
            {
                neuron.AddOutput(outputNeuron);
            }
            RandomiseWeights();
        }

        public void RandomiseWeights()
        {
            foreach (Neuron neuron in Neurons)
            {
                neuron.RandomiseWeights();
            }
        }

        public void Backpropagate(IEnumerable<double> labels)
        {
            foreach (var neuronLabelPair in Neurons.Zip(labels, (neuron, label) => (neuron, label)))
            {
                neuronLabelPair.neuron.Backpropagate(neuronLabelPair.label);
            }
            BackpropPrevLayer();
        }

        public void Backpropagate()
        {
            foreach (Neuron neuron in Neurons)
            {
                neuron.Backpropagate();
            }
            BackpropPrevLayer();
        }

        public void BackpropPrevLayer()
        {
            if (PrevLayer != null)
            {
                PrevLayer.Backpropagate();
            }
        }

        public IEnumerable<double> Read()
        {
            return Neurons.Select(neuron => neuron.Read());
        }

    }
}
