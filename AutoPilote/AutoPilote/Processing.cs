using BusDrone;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Processing
    {
        private static List<Component> componentList = new List<Component>();
        private static void refreshComponent(NetworkStream stream)
        {
            //Notre id 1
            
            //Add or modify component in the componentList. If the component already exist modify it, add a new one otherwise.
        }

        private static List<bool> checkStability()
        {

            List<bool> isStable = new List<bool>();

            //Check all component to see if the drone is stable

            return isStable;
        }

        private static void sendAllValues(List<double> values) //Pull all values on the bus
        {
            string[] dataInString = new string[] { "\"x\": " + values[0], "\"y\": " + values[1], "\"z\": " + values[2] };
            Packet packet = new Packet("request", "publish", "[1,true]", 1, dataInString);
            string packetString = JsonConvert.SerializeObject(packet);
            Console.WriteLine(packetString);
        }

        private static List<double> calculateValuesToStabilize() // calculate the values in order to stabilize the drone 
        {
            List<double> values = new List<double>();

            return values;
        }

        static void Main(string[] args)
        {
            //Récupération d'un paquet
           // Packet packet = new Packet("request", "publish", "[]",1,"{ 'port': '17','value': '10.3','unit': 'mA'}");
           // Component motor = new Component(packet);
           // componentList.Add(motor);
           // Console.WriteLine(motor.ToString());
            //process();
            //StartClient();
            List<double> values = new List<double>();
            values.Add(1.33);
            values.Add(0.56);
            values.Add(3.01);
            sendAllValues(values);
            Console.ReadKey();
            /*
            while (true)
            {
                refreshComponent();
                List<bool> isStable = checkStability();
                if (isStable.Contains(false))
                {
                    List<double> values = calculateValuesToStabilize();
                    sendAllValues(values);
                }
            }
            */
            
        }
        /*
        public createJSON()
        {
            
        }
        */
        public static void StartClient()
        {
            byte[] buffer = new byte[1024];

            try
            {
                /* Les informations de l'hôte, faudra remplacer Dns.getHostName() par l'adresse du bus */
                TcpClient client = new TcpClient();
                client.Connect(IPAddress.Parse("10.5.26.121"), 7777);
                while (true)
                {
                    NetworkStream stream = client.GetStream();
                    stream.Write(buffer, 0, 1024);
                    Console.WriteLine("salut");
                }
                //client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        static void process()
        {
            Console.WriteLine("Hello World!");
            int[] xacc = { 10, 5, 0, 0, 0 }; //valeur “imaginaire” renvoyés par
                                             // l’alccéléromètre en X
            int[] yacc = { 0, 0, 0, 0, 0, 0 }; // idem en Y
            int[] zacc = { 26, 89, -76, 18, 0 };    // idem en Z

            int consignex = 0;  //consigne de vol, 0 pour une vol stationnaire, sinon valeur envoyé par la station sol sur l’axe X
            int consigney = 0;  //idem en Y
            int consignez = 0;     //idem en Z
            int erreurx = 0;
            int erreury = 0;
            int erreurz = 0;
            int somme_erreursx = 0;
            int somme_erreursy = 0;
            int somme_erreursz = 0;
            int variation_erreurx;
            int variation_erreury;
            int variation_erreurz;
            int commandex;
            int commandey;
            int commandez;
            int erreur_precedentex = consignex;
            int erreur_precedentey = consigney;
            int erreur_precedentez = consignez;
            int Kp = 1; //coefficient de proportionnalité de l’erreur, permet de rendre plus réactif le moteur, mais trop grand le rend instable (incapable de définir une vitesse de croisière 
            int Ki = 0; //coefficient de proportionnalité de la somme des erreurs, ce qui permet d’avoir une valeur plus proche de la valeur souhaité (sans ce coefficient apparaît un décalage)
            int Kd = 0; //coefficient de proportionnalité de la variation de l'erreur. permet de se rendre compte si les changements sont optimaux
            int MoteurX = 0;
            int MoteurY = 0;
            int MoteurZ = 0;
            //int MoteurBR=1500;

            for (int i = 0; i < xacc.Length; i++)
            {

                // ----- X -----
                erreurx = consignex - xacc[i];
                somme_erreursx += erreurx;
                variation_erreurx = erreurx - erreur_precedentex;
                commandex = Kp * erreurx + Ki * somme_erreursx + Kd * variation_erreurx;
                erreur_precedentex = erreurx;
                //System.out.println(xacc[i]);
                Console.WriteLine(commandex);

                MoteurX += commandex;
                /*MoteurFL =1500+commandex;
                MoteurFR =1500-commandex;
                MoteurBL =1500+commandex;
                MoteurBR =1500-commandex;*/


                // ----- Y -----
                erreury = consigney - yacc[i];
                somme_erreursy += erreury;
                variation_erreury = erreury - erreur_precedentey;
                commandey = Kp * erreury + Ki * somme_erreursy + Kd * variation_erreury;
                erreur_precedentey = erreury;

                Console.WriteLine(commandey);
                MoteurY += commandey;
                /*MoteurFL +=commandey;
                MoteurFR +=commandey;
                MoteurBL -=commandey;
                MoteurBR -=commandey;*/



                // ----- Z -----        Est-ce vraiment pareil pour l'axe Z ?!
                erreurz = consignez - zacc[i];
                somme_erreursz += erreurz;
                variation_erreurz = erreurz - erreur_precedentez;
                commandez = Kp * erreurz + Ki * somme_erreursz + Kd * variation_erreurz;
                erreur_precedentez = erreurz;

                Console.WriteLine(commandez);
                MoteurZ += commandez;
                /*MoteurFL +=commandez;
                MoteurFR -=commandez;
                MoteurBL -=commandez;
                MoteurBR +=commandez;*/



                Console.WriteLine("X : " + MoteurX);
                Console.WriteLine("Y : " + MoteurY);
                Console.WriteLine("Z : " + MoteurZ);
                //Console.WriteLine("BR : " +MoteurBR);
                Console.WriteLine("------------------------------------------");
            }
            /*
           *
           *
           *   erreur = consigne - mesure;
            somme_erreurs += erreur;
            variation_erreur = erreur - erreur_précédente;
            commande = Kp * erreur + Ki * somme_erreurs + Kd * variation_erreur;
            erreur_précédente = erreur
           */

        }
    }
}


