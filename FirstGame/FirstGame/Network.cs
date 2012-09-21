using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirstGame
{
    class Network
    {

        public double [] output;
        double [] input;

        public int fitness = 0;
        public int life = 0;
        public int mutation = 0;
        public int timesDead = 0;
        public int index = 0;
        public int generation = 0;
        public int distance = 0;

        public Layer [] layer;
        Random r;

        public int done = 0;

        int numInputs = 0;

        public Network(int n, Random r)
        {
            this.r = r;
            numInputs = n;
            input = new double[n];
            layer = new Layer[3];
            layer[0] = new Layer(30, n, r);
            layer[1] = new Layer(2, 30, r);
        }

        public double [] update(double [] inputs)
        {
            this.input = inputs;
            life++;

            layer[0].setInputs(inputs, numInputs);
            layer[1].setInputs(layer[0].getOutput(), layer[1].numInputs);
            output = layer[1].getOutput();

            return output;
        }

        public void die()
        {
            if (done != 1)
            {
                generation++;
                timesDead++;
                layer[0].die(r);
                layer[1].die(r);
            }
        }

        public void mutate(double [] best)
        {
            int s = 0;
            mutation++;
            generation++;
            for (int i = 0; i < 2; i++)
            {

                for (int g = 0; g < layer[i].numNodes; g++)
                {
                    for (int j = 0; j < layer[i].numInputs; j++)
                    {
                        if (r.Next(0, 1000) * .001 < .25 && done != 1)
                        {
                            if (best != null)
                                layer[i].nodes[g].weight[j] += ((r.Next(0, 1000)) * .001) * (best[s] - layer[i].nodes[g].weight[j]);

                            else
                                layer[i].nodes[g].weight[j] += ((r.Next(0, 1000)-500) * .002);
                        }

                        s++;
                    }
                }
            }
        }

        public String debug()
        {
            String s = "";
            for (int i = 0; i < 2; i++)
            {

                for (int g = 0; g < layer[i].numNodes; g++)
                {
                    s += layer[i].nodes[g].output + ":";
                    for (int j = 0; j < layer[i].numInputs; j++)
                        s += layer[i].nodes[g].weight[j] + " ";
                }
            }

            return s;
        }

    }
}
