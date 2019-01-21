using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetwork.Structure;
using NeuralNetwork.Functions;
using NeuralNetwork.Config;
using static NeuralNetwork.Config.Config;

namespace NeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            var network = new Structure.NeuralNetwork(new [] { 3, 10, 1 });


            var trainingData = new[]
            {
                new [] { BinaryLow, BinaryLow, BinaryLow },
                new [] { BinaryLow, BinaryHigh, BinaryLow },
                new [] { BinaryHigh, BinaryLow, BinaryLow },
                new [] { BinaryHigh, BinaryHigh, BinaryLow },
                new [] { BinaryLow, BinaryLow, BinaryHigh },
                new [] { BinaryLow, BinaryHigh, BinaryHigh },
                new [] { BinaryHigh, BinaryLow, BinaryHigh },
                new [] { BinaryHigh, BinaryHigh, BinaryHigh }
            };

            var labels = new[]
            {
                new [] { BinaryLow },
                new [] { BinaryHigh },
                new [] { BinaryHigh },
                new [] { BinaryLow },
                new [] { BinaryLow },
                new [] { BinaryHigh },
                new [] { BinaryHigh },
                new [] { BinaryLow }
            };

            var myTrainConfig = new TrainConfig();
            myTrainConfig.Epochs = 5000;
            Tasks.Train(network, trainingData, labels, myTrainConfig);
            

            foreach (var data in trainingData)
            {
                network.InputLayer.FeedForward(data);
                Console.Write(Helpers.DataToString(data));
                Console.Write(": ");
                var output = Helpers.DataToString(network.OutputLayer.Read());
                Console.WriteLine(output);
            }

            Console.WriteLine("Avg error: " + Tasks.TestRegressionAccuracy(network, trainingData, labels));
            Console.ReadKey();
        }


    }
}
