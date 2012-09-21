using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace FirstGame
{
    class Node
    {

        public double [] inputs;
        public double [] weight;
        int numInputs;

        public double output;
        

        public Node(int n)
        {
            inputs = new double[n];
            numInputs = n;
            weight = new double[numInputs];
        }

        public void setInputs(List<double> inputs)
        {
            this.inputs = inputs.ToArray();
        }

        public void randWeights(Random r){

            for (int i = 0; i < numInputs; i++)
            {

                weight[i] = (r.Next(0, 1000) - 500) * .002;
            }
        }

        public double getOutput()
        {
            double sum = 0;
            
            for(int i = 0; i < numInputs; i++){

                sum += weight[i] * inputs[i];
            }

            output = sigmoid(sum);
            return output;
        }

        public double sigmoid(double sum)
        {
           // return 1/(1+Math.Pow(Math.E, -sum));
            return Math.Tanh(sum);
        }


    }
}
