using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouchpadDatalogger
{
    public class SampleUtil
    {
        public UInt16 Timestamp;

        public UInt16 MeasX;
        public UInt16 MeasY;

        public byte Confidence; 

        public UInt16 FilterX;
        public UInt16 FilterY;

        public SampleUtil()
        {
            Clear();
        }

        public void Clear()
        {
            Timestamp = 0;
            MeasX = 0;
            MeasY = 0;
            Confidence = 0;
            FilterX = 0;
            FilterY = 0;
        }

        public string GetHeader()
        {
            string result = "";

            result += "TIMESTAMP,";
            result += "MEAS X,";
            result += "MEAS Y,";
            result += "CONFIDENCE,";
            result += "FILTER X,";
            result += "FILTER Y\r\n";

            return result;
        }

        override public string ToString()
        {
            string result = "";

            result += Timestamp.ToString() + ",";
            result += MeasX.ToString() + ",";
            result += MeasY.ToString() + ",";
            result += Confidence.ToString() + ",";
            result += FilterX.ToString() + ",";
            result += FilterY.ToString() + "\r\n";

            return result; 
        }

        public void Parse(List<byte> packet)
        {
            Clear();

            Timestamp = (UInt16)(packet[2] | ( packet[3] << 8 ));
            MeasX = (UInt16) ( packet[4] | ( packet[5] << 8 ) );
            MeasY = (UInt16) ( packet[6] | ( packet[7] << 8 ) );
            Confidence = packet[8];
            FilterX = (UInt16) ( packet[9] | ( packet[10] << 8 ) );
            FilterY = (UInt16) ( packet[11] | ( packet[12] << 8 ) );
        }
    }
}
