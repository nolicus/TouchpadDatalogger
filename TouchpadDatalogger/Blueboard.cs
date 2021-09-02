using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Management;


namespace TouchpadDatalogger
{
    class Blueboard
    {
        SerialPort mComPort;

        const UInt16 SIMPLE_PACKET_SIZE = 16; 
        public bool IsConnected
        {
            get { return mIsConnected;  }
        }

        private bool mIsConnected;
        public Blueboard()
        {
            mIsConnected = false;

            Connect();
        }

        public bool Connect()
        {
            // NOTE: If no blueboard is found create and report an error. 
            string teensySerial = findTeensySerial();
            if ( teensySerial != "" )
            {
                mComPort = new SerialPort( teensySerial );
                if(mComPort != null)
                {
                    try
                    {
                        mComPort.Open();
                        mComPort.ReadTimeout = SerialPort.InfiniteTimeout;  // infinite is best for debug
                        mIsConnected = mComPort.IsOpen;
                    }
                    catch { }
                }
            }
            else
            {
                // error
            }

            return mIsConnected;
        }

        public bool GetSample(out SampleUtil samp)
        {
            bool result = false;
            samp = new SampleUtil();

            if(mIsConnected)
            {
                List<byte> packet; 
                getPacket(out packet); 
                if(verifyPacket(packet))
                {
                    samp.Parse(packet);
                    result = true; 
                }
            }

            return result; 
        }

        // ******************************** PRIVATE FUNC
        private void getPacket( out List<byte> packet)
        {
            packet = new List<byte>();

            byte[] buffer = new byte[4];
            bool error = false;

            do
            {
                try
                {
                    error = mComPort.Read( buffer, 0, 1 ) != 1;
                }
                catch
                {
                    error = true;
                    break;
                }

                if ( error == false )
                {
                    packet.Add( buffer[0] );
                }
            } while ( error == false && packet.Count < SIMPLE_PACKET_SIZE );
        }

        private bool verifyPacket( List<byte> packet)
        {
            bool result = false;

            if(packet.Count < SIMPLE_PACKET_SIZE)
            {
                // Verify Header and Footer
                if( packet[0] == 0x6A && 
                    packet[1] == 0xD5 &&
                    packet[SIMPLE_PACKET_SIZE - 2] == 0x17 && 
                    packet[SIMPLE_PACKET_SIZE - 1] == 0xE2 )
                {
                    byte checksum = 0; 
                    for( int i = 0; i < SIMPLE_PACKET_SIZE - 3; i++ )
                    {
                        checksum += packet[i]; 
                    }

                    if(checksum == packet[SIMPLE_PACKET_SIZE - 3])
                    {
                        result = true; 
                    }
                    else
                    {
                        // ERROR: Checksum was incorrect
                    }
                }
                else
                {
                    // ERROR: Header and Footer were incorrect
                }
            }
            else
            {
                // ERROR: Header and Footer were incorrect
            }

            return result; 
        }

        private string findTeensySerial( )
        {
            string result = "";
            List<string> Devices = new List<string>();
            List<string> DeviceLog = new List<string>();

            string expectedVid = "16C0";
            string expectedPid = "0483";

            try
            {
                // Missing this reference? Read the comment above this function.
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher( "root\\CIMV2", "SELECT * FROM Win32_PnPEntity" );

                foreach ( ManagementObject queryObj in searcher.Get() )
                {
                    if ( ( queryObj["Caption"] != null ) && ( queryObj["Caption"].ToString().Contains( "(COM" ) ) )
                    {
                        List<string> DevInfo = new List<string>();

                        string Caption = queryObj["Caption"].ToString();
                        int CaptionIndex = Caption.IndexOf( "(COM" );
                        string CaptionInfo = Caption.Substring( CaptionIndex + 1 ).TrimEnd( ')' ); // make the trimming more correct                 

                        string vid = "n/a", pid = "n/a";

                        string deviceId = queryObj["deviceid"].ToString(); //"DeviceID"
                        if ( deviceId != null )
                        {
                            int vidIndex = deviceId.IndexOf( "VID_" );
                            int pidIndex = deviceId.IndexOf( "PID_" );

                            if ( vidIndex != -1 && pidIndex != -1 )
                            {
                                string startingAtVid = deviceId.Substring( vidIndex + 4 ); // + 4 to remove "VID_"                    
                                vid = startingAtVid.Substring( 0, 4 ); // vid is four characters long
                                string startingAtPid = deviceId.Substring( pidIndex + 4 ); // + 4 to remove "PID_"                    
                                pid = startingAtPid.Substring( 0, 4 ); // pid is four characters long
                            }
                        }

                        if ( string.Equals( vid, expectedVid, StringComparison.OrdinalIgnoreCase ) && string.Equals( pid, expectedPid, StringComparison.OrdinalIgnoreCase ) )
                        {
                            Devices.Add( CaptionInfo );
                        }

                        DeviceLog.Add( string.Format( "{0} {1} {2}", CaptionInfo, vid, pid ) );
                    }
                }
            }
            catch
            {
                DeviceLog.Add( "RegistryError" );
            }

            if(Devices.Count > 0)
            {
                result = Devices[0];
            }

            return result; 
        }
    }
}
