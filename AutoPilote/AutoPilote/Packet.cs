
using System;

namespace BusDrone
{
    public class Packet 
    {
        public string type;
        public string request;
        public string args;
        public int peripherial;
        public string[] data { get; set; }

        public Packet(string type, string request, string args,int peripherial, string[] data)
        {
            this.type = type;
            this.request = request;
            this.args = args;
            this.peripherial = peripherial;
            this.data = data;
        }
    }
}

