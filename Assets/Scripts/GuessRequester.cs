using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;

namespace Leap.Unity
{
    public class GuessRequester : RunAbleThread
    {
        public static string _Guess;
        public static bool GotGuess, Guessing;

        protected override void Run()
        {
            //Debug.Log("Run Command Initiated");
            ForceDotNet.Force();
            using (RequestSocket client = new RequestSocket())
            {
                client.Connect("tcp://localhost:5555");
                //Debug.Log("Connected to Python");
                while(Guessing)
                {
                    if(!GuessManager.newGuess)
                    {
                        //Debug.Log("No New Guess Found");
                        continue;
                    }
                    else
                    {
                        //Debug.Log("Sending Data: " + GuessManager.handDataGuess);
                        client.SendFrame(GuessManager.handDataGuess);

                        //Debug.Log("Setting Message to null and gotMessage to false");
                        string message = null;
                        bool gotMessage = false;

                        while (Running)
                        {
                            //Debug.Log("Waiting to receive message");
                            gotMessage = client.TryReceiveFrameString(out message);
                            if (gotMessage) break;
                        }

                        //Debug.Log("Message Received");
                        if (gotMessage)
                        {
                            //Debug.Log("Guess: " + message);
                            _Guess = message;
                            GotGuess = true;
                        }
                    }
                }
            }
            //Debug.Log("Program Terminating");
            NetMQConfig.Cleanup();
        }
    }
}
