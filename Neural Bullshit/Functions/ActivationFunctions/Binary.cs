using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NeuralNetwork.Config.Config;

namespace NeuralNetwork.Functions.ActivationFunctions
{
    class Binary : NonDerivableActivationFunction
    {
        override public double Base(double input)
        {
            return (input > BinaryThreshold ? 1 : 0);
        }

    }
}
