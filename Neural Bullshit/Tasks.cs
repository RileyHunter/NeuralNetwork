using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetwork.Config;
using NeuralNetwork.Structure;

namespace NeuralNetwork
{
    static class Tasks
    {
        public static double TestRegressionAccuracy(Structure.NeuralNetwork network, IEnumerable<IEnumerable<double>> testData, IEnumerable<IEnumerable<double>> testLabels)
        {
            var sumError = 0d;
            foreach (var dataLabelsPair in testData.Zip(testLabels, (data, labels) => (data, labels)))
            {
                network.InputLayer.FeedForward(dataLabelsPair.data);
                var readErrors = network.OutputLayer.Read().Zip(dataLabelsPair.labels, (read, label) => (read - label));
                foreach (var error in readErrors)
                {
                    sumError += Math.Abs(error);
                }
            }
            var avgError = sumError / (testData.Count() * testLabels.First().Count());
            return avgError;
        }

        /// <summary>
        /// Method stub for testing classification accuracy
        /// </summary>
        /// <param name="network"></param>
        /// <param name="testData"></param>
        /// <param name="testLabels"></param>
        /// <returns></returns>
        public static double TestClassificationAccuracy(Structure.NeuralNetwork network, IEnumerable<IEnumerable<double>> testData, IEnumerable<IEnumerable<double>> testLabels)
        {
            throw new NotImplementedException();
        }

        public static void Train(Structure.NeuralNetwork network, IEnumerable<IEnumerable<double>> trainingData, IEnumerable<IEnumerable<double>> trainingLabels, TrainConfig trainConfig)
        {
            var epochs = trainConfig.Epochs;
            for (int epoch = 0; epoch < epochs; epoch++)
            {
                if (epoch % Math.Ceiling(epochs / 10d) == 0)
                {
                    Console.WriteLine($"Trained for {epoch} epochs");
                }
                foreach (var dataLabelPair in trainingData.Zip(trainingLabels, (data, labels) => (data, labels)))
                {
                    network.InputLayer.FeedForward(dataLabelPair.data);
                    network.OutputLayer.Backpropagate(dataLabelPair.labels);
                }
            }
        }
    }
}
