using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class InputController
{
    public float CurrentValue;
    float currentPoint;
    float pastPoint;
    TcpClient client;
    public TestRepeated controller;
    // The range of inputs we expect. In this case we're saying it'll be between 0 and 30cm


    public void Begin(string ipAddress, int port)
    {
        // Give the network stuff its own special thread
        var thread = new Thread(() =>
        {

            currentPoint = -1;
            pastPoint = -1;
            // This class makes it super easy to do network stuff
             client = new TcpClient();

            // Change this to your devices real address
           // Debug.Log("Ip= " + ipAddress+"  port= "+port);
            client.Connect(ipAddress, port);
            
            var stream = new StreamReader(client.GetStream());

            // We'll read values and buffer them up in here
            var buffer = new List<byte>();
            while (client.Connected)
            {
                // Read the next byte
                var read = stream.Read();
                //Debug.Log("ESP= " + read);
                // We split readings with a carriage return, so check for it 
                if (read == 13)
                {
                    // Once we have a reading, convert our buffer to a string, since the values are coming as strings
                    var str = Encoding.ASCII.GetString(buffer.ToArray());

                    // We assume that they're floats
                    var dist = float.Parse(str);
                    //Debug.Log("++++++++++++++++++ESP= "+dist);
                    pastPoint = currentPoint;
                    currentPoint = dist;
                    if(pastPoint>0&& currentPoint>0)
                    {
                        if(currentPoint>= pastPoint)
                        {
                            CurrentValue = (currentPoint - pastPoint) / 25;
                           // Debug.Log("++++++++++++++++++ESP= " + (currentPoint - pastPoint));
                        }
                        else
                        {
                            CurrentValue = (1389f - pastPoint+ currentPoint) / 25;
                           // Debug.Log("++++++++++++++++++ESP= " + (1389f - pastPoint + currentPoint));
                        }
                       
                        controller.Setspeed(CurrentValue);
                    }
                    // Remap the value from our input range to our planes movement range
                    // CurrentValue = dist/100f;
                   
                    // Clear the buffer ready for another reading
                    buffer.Clear();
                }
                else
                    // If this wasn't the end of a reading, then just add this new byte to our buffer
                    buffer.Add((byte)read);
            }
        });

        thread.Start();
    }
    public void EndConnect()
    {
        client.Close();
    }
}
