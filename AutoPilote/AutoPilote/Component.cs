using BusDrone;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Component
    {
        private string type { get; set; }
        private int port { get; set; }
        private double value { get; set; }
        private string unit { get; set; }

        
        public Component(string type,  int port, double value, string unit)
        {
            this.type = type;
            this.port = port;
            this.value = value;
            this.unit = unit;
        }

        
        public Component(Packet packet)
        {
            dynamic tmp = JObject.Parse(packet.content);

            if(packet.ID == 1)
            {
                this.type = "motor";
                this.port = tmp.port;
            }
            else
            {
                this.type = "sensor";
                this.port = packet.ID;
            }
            this.value = tmp.value;
            this.unit = tmp.unit;
        }
        
        public override string ToString()
        {
            return "Type = " + this.type + "\nPort = " + this.port + "\nValue = " + this.value + "\nUnit = " + this.unit + "\n";
        }
        
    }
}
