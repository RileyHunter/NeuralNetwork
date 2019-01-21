using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NeuralNetwork.Config.Config;

namespace NeuralNetwork
{
    static class Helpers
    {
        static public Random Rand = new Random();

        static public double DoubleBetween(double min, double max)
        {
            return (Rand.NextDouble() * (max - min)) + min;
        }
        static public string DataToString(int[] input)
        {
            return DataToString(Array.ConvertAll(input, num => (double)num));
        }

        static public string DataToString(IEnumerable<double> input)
        {
            string output = "[";
            foreach (double num in input)
            {
                output += num;
                output += ", ";
            }
            return output.Substring(0, output.Length - 2) + "]";
        }
    }
}
