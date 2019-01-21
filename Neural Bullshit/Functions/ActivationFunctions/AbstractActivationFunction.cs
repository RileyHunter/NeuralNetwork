using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Functions.ActivationFunctions
{
    abstract class AbstractActivationFunction
    {
        public abstract double Base(double input);

        public abstract double Derivative(double input);
    }
}
