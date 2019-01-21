using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Functions.ActivationFunctions
{
    class Linear : AbstractActivationFunction
    {
        override public double Base(double input)
        {
            return input;
        }

        override public double Derivative(double input)
        {
            return 1;
        }
    }
}
