using BusDrone;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApp1
{
    class Processing
    {
        private static List<Component> componentList = new List<Component>();
        private static void refreshComponent()
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

        }

        private static List<double> calculateValuesToStabilize() // calculate the values in order to stabilize the drone 
        {
            List<double> values = new List<double>();

            return values;
        }

        static void Main(string[] args)
        {
            //Récupération d'un paquet
            Packet packet = new Packet(Sensor.MobilCaptor, 1, -1, "{ 'port': '17','value': '10.3','unit': 'mA'}", 1);
            Component motor = new Component(packet);
            Console.WriteLine(motor.ToString());
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

        public static void StartClient()
        {
            byte[] buffer = new byte[1024];

            try
            {
                /* Les informations de l'hôte, faudra remplacer Dns.getHostName() par l'adresse du bus */
                IPHostEntry hostInfos = Dns.GetHostEntry(Dns.GetHostName());
                /*On prend l'adresse IP de l'hôte */
                IPAddress hostIpAdress = hostInfos.AddressList[0];
                /*On créé le EndPoint, c'est l'autre extrémité du Socket*/
                IPEndPoint remoteEP = new IPEndPoint(hostIpAdress, 33600);

                Socket autopilotSocket = new Socket(hostIpAdress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                autopilotSocket.Connect(remoteEP);

                Console.WriteLine("Connexion à {0}", autopilotSocket.RemoteEndPoint.ToString());

                autopilotSocket.Shutdown(SocketShutdown.Both);
                autopilotSocket.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}
