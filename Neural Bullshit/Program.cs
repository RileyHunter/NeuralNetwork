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
        // Currently, this program creates a 3-layer network with 10 hidden nodes.
        // The network accepts binary training data to learn [A XOR B] and ignore input C.
        // With this structure and about 2000+ training epochs, the network slowly learns
        // to perform binary logic, and scores full accuracy on the training data.
        // In principle, there's nothing stopping this network from being used for all 
        // sorts of fun new data analysis now.
        static void Main(string[] args)
        {
            // Create a new 3-10-1 NN
            var network = new Structure.NeuralNetwork(new [] { 3, 10, 1 });

            // Init. training data and labels
            // Note: BinaryLow/High are premade values corresponding to 0.1d and 0.9d respectively,
            // to avoid nasty sigmoid gradient problems.
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

            // TrainConfig is a class used to store important training hyperparameters
            var myTrainConfig = new TrainConfig();
            myTrainConfig.Epochs = 5000;

            // Train the network using the config, network and data above
            Tasks.Train(network, trainingData, labels, myTrainConfig);
            
            // Without further training, feed each data sample through the network and output to console
            foreach (var data in trainingData)
            {
                network.InputLayer.FeedForward(data);
                Console.Write(Helpers.DataToString(data));
                Console.Write(": ");
                var output = Helpers.DataToString(network.OutputLayer.Read());
                Console.WriteLine(output);
            }

            // Use the prebuilt regression accuracy task to get an avg error.
            Console.WriteLine("Avg error: " + Tasks.TestRegressionAccuracy(network, trainingData, labels));
            Console.ReadKey();
        }


    }
}
