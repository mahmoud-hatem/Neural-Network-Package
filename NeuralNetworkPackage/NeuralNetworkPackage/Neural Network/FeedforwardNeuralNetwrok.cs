using System;
using System.Collections.Generic;


namespace NeuralNetworkPackage
{
    public class FeedforwardNeuralNetwrok
    {
        private int numOfLayers;
        private int numOfInput;
        private List<List<Neuron>> network;
        private List<int> numOfNeuronsPerLayer;


        //		private List <double> input;
        //		private List <double> output;
        //		public List <double> Input 
        //		{
        //			get { return this.input; }
        //		}
        //		public List <double> Output 
        //		{
        //			get { return this.output; }
        //		}

        public FeedforwardNeuralNetwrok(int numOfLayers)
        {
            if (numOfLayers < 2)
                throw new Exception("Can't Initiate Network with lower than 2 layers");
            this.numOfLayers = numOfLayers;
            this.network = new List<List<Neuron>>();
        }

        public void setNetwork(List<int> numOfNeuronsPerLayer)
        {
            if (this.numOfLayers != numOfNeuronsPerLayer.Count)
                throw new Exception("Wrong List size for numOfNeuronsPerLayer");

            this.numOfNeuronsPerLayer = numOfNeuronsPerLayer;

            this.numOfInput = this.numOfNeuronsPerLayer[0];

            for (int i = 1; i < this.numOfLayers; ++i)
            {
                this.network.Add(new List<Neuron>());
            }

        }

        public void setLayer(int layerIndex, mathFunction activationFunction)
        {
            if (layerIndex == 0)
                throw new Exception("Can't set Input Layer");

            for (int i = 0; i < this.numOfNeuronsPerLayer[layerIndex]; ++i)
            {
                Neuron neuron = new Neuron(this.numOfNeuronsPerLayer[layerIndex - 1], activationFunction);
                this.network[layerIndex - 1].Add(neuron);
            }
        }

        public void setNeuron(int layerIndex, int neuronIndex, List<double> weights, double bias)
        {
            if (weights.Count != this.numOfNeuronsPerLayer[layerIndex - 1])
                throw new Exception("Invalid weights size");

            this.network[layerIndex - 1][neuronIndex].update(weights, bias);
        }

        public List<double> feedforward(List<double> input)
        {
            if (this.numOfInput != input.Count)
                throw new Exception("Invalid input size");

            List<double> currentInput = input;
            List<double> nextInput = new List<double>();	// nextInput = currentOutput

            for (int i = 1; i < this.numOfLayers; ++i)
            {
                for (int j = 0; j < this.numOfNeuronsPerLayer[i]; ++j)
                {
                    nextInput.Add(this.network[i - 1][j].feedforward(currentInput));
                }
                // Prepare input to next layer
                currentInput = nextInput;
                nextInput = new List<double>();
            }

            List<double> output = currentInput;
            return output;
        }

        public void train(List<List<double>> trainingSamples, List<List<double>> trainingLabels, double learningRate, LearningAlgorithm learningAlgorithm)
        {
            for (int i = 0; i < trainingSamples.Count; ++i)
            {
                List<double> output = this.feedforward(trainingSamples[i]);
                this.network = learningAlgorithm.learn(learningRate, trainingSamples[i], trainingLabels[i], this.network);
            }
        }
    }
}

