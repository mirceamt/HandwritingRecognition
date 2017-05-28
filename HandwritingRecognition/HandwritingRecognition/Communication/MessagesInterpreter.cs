using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    **messages meaning**

    -- request to classify a charecter from c#
    msg[0] = 1
    msg[1:1024] = 1024 characters of '0' or '1' which represent the liniarized black and white letter written on the panel

    -- response from client with the predictions as characters
    msg[0] = 2
    msg[1] = length of the string
    msg[2:] = the actual string

    -- response from client with the predictions as stringified floats
    msg[0] = 3
    msg[1] = length of the string 
    msg[2:] = the actual string

    -- from client: client ready
    msg[0] = 4    

    -- from main program: close python client
    msg[0] = 100


 */

namespace HandwritingRecognition.Communication
{
    public enum MessagesMeaning
    {
        ClassificationAsLetters = 2,
        ClassificationAsFloats = 3, 
        ClientReady = 4,
        Unknown = -1,
    }
    class MessagesInterpreter
    {
        public static MessagesMeaning interpretMessage(int cnt, byte[] receivedBytes)
        {
            if (cnt > 0 && receivedBytes != null)
            {
                switch(receivedBytes[0])
                {
                    case 2:
                        return MessagesMeaning.ClassificationAsLetters;
                    case 3:
                        return MessagesMeaning.ClassificationAsFloats;
                    case 4:
                        return MessagesMeaning.ClientReady;
                    default:
                        return MessagesMeaning.Unknown;
                }
            }
            return MessagesMeaning.Unknown;
        }

        public static void interpretMessageAndDoAction(int cnt, byte[] receivedBytes)
        {
            MessagesMeaning msgMeaning = interpretMessage(cnt, receivedBytes);
            DoAction(msgMeaning, cnt, receivedBytes);
        }

        public static void DoAction(MessagesMeaning msgMeaning, int cnt, byte[] receivedBytes)
        {
            switch(msgMeaning)
            {
                case MessagesMeaning.ClientReady:
                    // TODO 
                    // ApplicationUseManager - TriggerApplicationReady
                    int x = 1;
                    x++;

                    break;
                case MessagesMeaning.Unknown:
                    Console.WriteLine("received unkown message from client: ");
                    Console.WriteLine(receivedBytes);
                    break;
            }
        }
    }
}
