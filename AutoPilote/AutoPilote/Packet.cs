
using System;

namespace BusDrone
{
    public class Packet : IComparable<Packet>
    {
        public Sensor source { get; private set; }
        public int ID { get; private set; }
        public static int PID { get; private set; } = 0;
        public int TTL { get; private set; }
        public int size { get; private set; }
        public String content { get; private set; }
        public int CRC { get; private set; }

        public Packet(Sensor src, int ID, int TTL, String content, int crc)
        {
            this.source = src;
            this.ID = ID;
            PID++;
            this.TTL = TTL;
            this.size = content.Length;
            this.content = content;
            this.CRC = crc;
        }

        public int DecreaseTTL(int tick)
        {
            return (this.TTL -= tick);
        }

        public int CompareTo(Packet other)
        {
            return (other == null) ? 1 : this.TTL.CompareTo(other.TTL);
        }

        //ComputeCRC()
    }
}

