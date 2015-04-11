using System;
using System.Collections.Generic;

namespace NeuralNetworkPackage
{
    public abstract class LearningAlgorithm
    {
        public LearningAlgorithm() { }

        public abstract List<List<Neuron>> learn(double learningRate, List<double> input, List<double> output, List<List<Neuron>> network);
    }
}

