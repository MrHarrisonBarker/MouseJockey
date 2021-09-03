using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rug.Osc;

namespace MouseJockey
{
    class Jockey
    {
        private readonly OscReceiver Receiver;
        private Thread ListenThread;

        private const int Port = 8000;

        public Jockey()
        {
            Receiver = new OscReceiver(Port);

            ListenThread = new Thread(new ThreadStart(Listen));

            Receiver.Connect();

            ListenThread.Start();
        }

        private void Listen()
        {
            try
            {
                while (Receiver.State != OscSocketState.Closed)
                {
                    if (Receiver.State == OscSocketState.Connected)
                    {
                        OscPacket nextPacket = Receiver.Receive();

                        Debug.WriteLine(nextPacket.ToString());

                        ParseMessage(nextPacket);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void ParseMessage(OscPacket message)
        {
            var parsedMessage = OscMessage.Parse(message.ToString());

            // if the messages are 
            if (parsedMessage.Address == "/magicq")
            {
                try
                {
                    var arg = parsedMessage[0].ToString();
                    Program.Controllables.Faders.TryGetValue(arg, out var fader);

                    var value = (int) parsedMessage[1];
                    fader.MoveTo(value);

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    throw;
                }

                 Program.Controllables.Faders.ContainsKey(parsedMessage[0].ToString());
            }
        }

    }
}
