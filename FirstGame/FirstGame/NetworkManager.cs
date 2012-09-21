using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * This is the network manager that creates and stores networks
 */

namespace FirstGame
{
    class NetworkManager
    {

        public String s = "";
        public List<Network> networks;

        public NetworkManager()
        {

            networks = new List<Network>();
        }

        public void addNetwork(int n, Random r)
        {

            networks.Add(new Network(n, r));
        }

        public double [] update(int n, double [] inputs){

                return networks.ElementAt(n).update(inputs);
        }

        public void die(int training)
        {
            double[] best =  findBest();
            s += "Generation: " + networks[0].generation + "\n";

            if (training == 1)
                return;
            
            for (int i = 0; i < networks.Count; i++)
            {
                s += "Network # "+i+": " + networks[i].fitness + " Distance: "+networks[i].distance+"\n";
                networks.ElementAt(i).index = i;
                    
                if (best != null || networks.ElementAt(i).fitness < 100)
                {
                        //if ( networks.ElementAt(i).fitness < -100)
                       //     networks.ElementAt(i).die();

                        //else
                            networks[i].mutate(best);

                }

                else
                   networks[i].mutate(null);
            }

            s += "\n";

            
        }

        public double [] findBest()
        {
            int location = 0;
            int xFitness = 50;

            double[] bestWeights = new double[networks[0].layer[0].numNodes * networks[0].layer[0].numInputs +
                                               networks[0].layer[1].numNodes * networks[0].layer[1].numInputs];

            for (int i = 0; i < networks.Count; i++)
            {
                if (networks[i].done == 1)
                {
                    location = i;
                    xFitness = networks[i].fitness;
                }

                if (xFitness < networks[i].fitness)
                {
                    location = i;
                    xFitness = networks[i].fitness;
                }
            }

            if (xFitness == 50)
                return null;

            int s = 0;
            for (int i = 0; i < 1; i++)
            {

                for (int g = 0; g < networks[location].layer[i].numNodes; g++)
                {
                    for (int j = 0; j < networks[location].layer[i].numInputs; j++)
                    {
                        bestWeights[s] = networks[location].layer[i].nodes[g].weight[j];
                        s++;
                    }
                }
            }

            return bestWeights;
        }
    }
}
