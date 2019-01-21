using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Functions.ActivationFunctions
{
    class Sigmoid : AbstractActivationFunction
    {
        override public double Base(double input)
        {
            return (1d / (1d + Math.Exp(-input)));
        }
        
        override public double Derivative(double input)
        {
            return input * (1d - input);
        }
    }
}
