using System;
using System.Collections.Generic;

namespace NeuralNetworkPackage
{
	public class Normalization
	{
		private List<double> mean;
		private List<double> stdDev;
		private List<List<double>> data;

		public Normalization(List<List<double>> data)
		{
			this.data = data;
			this.mean = new List<double> ();
			this.stdDev = new List<double> ();

			calculateMean ();
			calculateStdDev ();
		}

		private void calculateMean()
		{
			List<double> sum = new List<double> ();

			for (int i = 0; i < this.data.Count; ++i) {
				for (int j = 0; j < this.data[i].Count; ++j) {
					if (i == 0) {
						sum.Add (this.data [i] [j]);
					} else {
						sum [j] += this.data [i] [j];
					}
				}
			}

			for (int i = 0; i < sum.Count; ++i)
				this.mean.Add (sum [i] / this.data [i].Count);

		}

		private void calculateStdDev ()
		{
			List<double> sum = new List<double> ();

			for (int i = 0; i < this.data.Count; ++i) {
				for (int j = 0; j < this.data[i].Count; ++j) {
					if (i == 0) {
						sum.Add (Math.Pow (this.data [i] [j] - this.mean [j], 2));
					} else {
						sum [j] += Math.Pow (this.data [i] [j] - this.mean [j], 2);
					}
				}
			}

			for (int i = 0; i < sum.Count; ++i)
				this.stdDev.Add (sum [i] / this.data [i].Count);

		}

		public List<List<double>> getNormalizedData()
		{
			List<List<double>> newData = new List<List<double>> ();

			for (int i = 0; i < this.data.Count; ++i) {
				List<double> sample = new List<double> ();
				for (int j = 0; j < this.data[i].Count; ++j) {
					sample.Add ((this.data [i] [j] - this.mean [j]) / this.stdDev [j]);
				}
				newData.Add (sample);
			}

			return newData;
		}
		public List<double> normalize(List<double> sample)
		{
			for (int i = 0; i < sample.Count; ++i) {
				sample [i] = (sample [i] - this.mean [i]) / this.stdDev [i];
			}

			return sample;
		}
	}
}