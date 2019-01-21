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
    class Neuron
    {
        public double Activity;
        private double RawActivity;
        public double Gradient { get; set; }
        public List<Synapse> Input;
        private List<Synapse> Output;
        
        public AbstractInputFunction InputFunction;
        public AbstractActivationFunction ActivationFunction;

        public Neuron(AbstractInputFunction inputFunction = null, AbstractActivationFunction activationFunction = null)
        {
            Input = new List<Synapse>();
            Output = new List<Synapse>();

            InputFunction = inputFunction;

            if(activationFunction == null)
            {
                ActivationFunction = new Sigmoid();
            } else
            {
                ActivationFunction = activationFunction;
            }

            if(InputFunction == null)
            {
                InputFunction = new SimpleSum();
            }
            else
            {
                InputFunction = inputFunction;
            }
        }

        virtual public void AddInput(Synapse newInput)
        {
            Input.Add(newInput);
        }

        public void AddOutput(Neuron post, double weight = 1)
        {
            Synapse newOutput = new Synapse(this, post, weight);
            Output.Add(newOutput);
        }

        public void Activate()
        {
            RawActivity = InputFunction.Base(Input);
            Activity = ActivationFunction.Base(RawActivity);
            foreach (Synapse synapse in Output)
            {
                synapse.Fire(Activity);
            }
        }

        public void Activate(double inputActivity)
        {
            Activity = ActivationFunction.Base(inputActivity);
            foreach (Synapse synapse in Output)
            {
                synapse.Fire(Activity);
            }
        }

        public double Read()
        {
            Activate();
            return Activity;
        }

        public void RandomiseWeights()
        {
            foreach (Synapse synapse in Output)
            {
                synapse.RandomiseWeight();
            }
        }

        public void Backpropagate(double label)
        {
            Gradient = (ActivationFunction.Derivative(Activity) * (label - Activity));
            AdjustWeightsByError();
        }

        public void Backpropagate()
        {
            var sumGrads = 0d;
            foreach (Synapse synapse in Output)
            {
                sumGrads +=  synapse.Weight * synapse.Post.Gradient;
            }
            Gradient = (ActivationFunction.Derivative(Activity) * sumGrads);
            AdjustWeightsByError();
        }

        private void AdjustWeightsByError()
        {
            foreach (Synapse synapse in Input)
            {
                synapse.UpdateWeight(Gradient);
            }
        }
    }
}
