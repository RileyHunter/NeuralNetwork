using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Functions.ActivationFunctions
{
    abstract class NonDerivableActivationFunction : AbstractActivationFunction
    {
        public override double Derivative(double input)
        {
            throw new NotImplementedException("[Error] Attempted to use derivative of non-derivable function");
        }
    }
}
