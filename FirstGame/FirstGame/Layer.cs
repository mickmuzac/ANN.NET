using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirstGame
{
    class Layer
    {

        public Node [] nodes;
        double [] output;
        public int numNodes = 0;
        public int numInputs = 0;

        public Layer(int numNodes, int numInputs, Random r)
        {
            this.numNodes = numNodes;
            this.numInputs = numInputs;

            nodes = new Node[numNodes];
            output = new double[numNodes];

            for (int i = 0; i < numNodes; i++)
            {
                nodes[i] = new Node(numInputs);
                nodes[i].randWeights(r);
            }
        }

        public void die(Random r)
        {

            for (int i = 0; i < numNodes; i++)
            {
                nodes[i].randWeights(r);
            }
        }

        public void setInputs(double [] inDoubles, int size)
        {

            for (int i = 0; i < numNodes; i++ )
            {

                nodes[i].inputs = inDoubles;
            }

        }

        public double[] getOutput()
        {
            for (int i = 0; i < numNodes; i++)
            {

                output[i] = nodes[i].getOutput();
            }

            return output;
        }
    }
}
