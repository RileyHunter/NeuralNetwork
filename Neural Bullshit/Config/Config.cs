using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Config
{
    static class Config
    {
        //Global stuff to deal with binarisation etc.
        static public double BinaryLow = 0.1;
        static public double BinaryHigh = 0.9;
        static public double BinaryThreshold = (BinaryLow + BinaryHigh) / 2d;

        static public double WeightMin = -10d;
        static public double WeightMax = 10d;
    }
}
