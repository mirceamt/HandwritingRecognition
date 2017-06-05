using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HandwritingRecognition.Utils;
using HandwritingRecognition.ImageProcessing;
using HandwritingRecognition.Writing;

/*
    **messages meaning**

    -- request to classify a charecter from c#
    msg[0] = 1
    msg[1:5] = messageSignature (which is the ID of the last adjustment over the connected components)
    msg[5:5+1024] = 1024 characters of '0' or '1' which represent the liniarized black and white letter written on the panel

    -- response from client with the predictions as characters
    msg[0] = 2
    msg[1] = length of the string
    msg[2:6] = messageSignature (which is the ID of the last adjustment over the connected components)
    msg[6:] = the actual string

    -- response from client with the predictions as stringified floats
    msg[0] = 3
    msg[1] = length of the string  # TODO not enough
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
        private static ConnectedComponentsTool connectedComponentsTool = null;
        private static WritingObserver writingObserver = null;

        public static void Initialize(ConnectedComponentsTool cct, WritingObserver wo)
        {
            connectedComponentsTool = cct;
            writingObserver = wo;
        }

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
                    ApplicationUseManager.Instance.TriggerApplicationReady();
                    break;

                case MessagesMeaning.ClassificationAsLetters:
                    int lengthOfString = (int)receivedBytes[1];
                    
                    byte[] lastAdjustmentIdBytes = new byte[4];
                    lastAdjustmentIdBytes[0] = receivedBytes[2];
                    lastAdjustmentIdBytes[1] = receivedBytes[3];
                    lastAdjustmentIdBytes[2] = receivedBytes[4];
                    lastAdjustmentIdBytes[3] = receivedBytes[5];

                    int lastAdjustmentId = CommonUtils.Transform4BytesToInt(lastAdjustmentIdBytes);

                    String responseMultipleCharactersAsString = "";
                    for (int i = 6, counter = 1; counter <= lengthOfString; counter++, i++)
                    {
                        byte currentByte = receivedBytes[i];
                        responseMultipleCharactersAsString += Convert.ToChar(currentByte);
                    }

                    // use writing observer and connectedComponentsTool to update the word
                    //UIUpdater.UpdatePredictedWord(responseMultipleCharactersAsString);

                    Tuple<List<ConnectedComponent>, ConnectedComponent> latestAdjustment = connectedComponentsTool.GetAdjustmentById(lastAdjustmentId);
                    List<String> possibleChars = new List<String>();
                    possibleChars.Add(responseMultipleCharactersAsString);
                    writingObserver.AdjustExistingWords(latestAdjustment.Item1, latestAdjustment.Item2, possibleChars);
                    break;

                case MessagesMeaning.Unknown:
                    Console.WriteLine("received unkown message from client: ");
                    Console.WriteLine(receivedBytes);
                    break;
                    
            }
        }
    }
}
