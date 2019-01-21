using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NeuralNetwork.Config.Config;

namespace NeuralNetwork.Structure
{
    class Synapse
    {
        public double Weight;
        public double Activity { get; set; }
        public Neuron Pre;
        public Neuron Post;

        public Synapse(Neuron pre, Neuron post, double weight = 1)
        {
            Pre = pre;
            Post = post;
            post.AddInput(this);
            Weight = weight;
        }

        public void UpdateWeight(double error)
        {
            Weight += error * Pre.Activity;
        }

        public void RandomiseWeight()
        {
            Weight = Helpers.DoubleBetween(WeightMin, WeightMax);
        }


        public void Fire(double activity)
        {
            Activity = activity * Weight;
        }
    }
}
