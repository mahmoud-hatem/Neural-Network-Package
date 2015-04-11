using System;

namespace NeuralNetworkPackage
{
	public class SigmoidFunction : mathFunction
	{
		public SigmoidFunction ()
		{
		}


		public double function (double input)
		{
			double sigmoid = 1 / (1 + Math.Exp (-input));
			return sigmoid;
		}
		public double derivative (double input)
		{
			double sigmoid = this.function (input);
			return sigmoid * (1 - sigmoid);
		}


	}
}

