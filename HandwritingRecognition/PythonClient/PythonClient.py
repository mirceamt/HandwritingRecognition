import ConnectionManager
import MessagesCreator
from MessagesInterpreter import *
import CNNClassifier

print("Started Python Client...")
connManager = ConnectionManager.ConnectionManager()
messagesCreator = MessagesCreator.MessagesCreator()
messagesInterpreter = MessagesInterpreter()
cnnClassifier = CNNClassifier.CNNClassifier()

host = "127.0.0.1"
port = 13000

connManager.connect(host, port)

#TODO
#load the ConvNet from file specified in C:\HandwritingRecognition\CommonResources\HandwritingRecognitionCNNPath.txt
if not connManager.isConnected():
    print("Could not connect to main programm!")
    print("press enter to close python client")
    _ = input()
    exit()

cnnClassifier.Load(r"C:\HandwritingRecognition\CommonResources\HandwritingRecognitionCNNPath.txt")

connManager.sendBytes(messagesCreator.createClientReadyMessage());
print("Sent ready message to main program")

while True:
    try:
        receivedBytes = connManager.receiveBytes()
    except ConnectionResetError:
        print ("main p.rogram has been closed")
        break;


    firstByte = receivedBytes[0]
    receivedMessageMeaning = messagesInterpreter.interpretMessage(firstByte)

    if receivedMessageMeaning == MessagesMeaning.closePythonClient: # close python client
        break

    elif receivedMessageMeaning == MessagesMeaning.classifyImage:
        # use ImageClassifier and send message back to c#
        example = [0.0] * 1024
        messageSignature = receivedBytes[1:5] # the messageSignature actually represents the ID of the latest adjustment made over connected components

        unu = ord('1')
        cnt = 0
        for i in range(5, 5 + 1024):
            currentPixel = 1.0 if receivedBytes[i] == unu else 0.0
            example[i - 5] = currentPixel

        multipleCharacterPossibilitesAsString = cnnClassifier.Classify(example);
        responseAsBytes = messagesCreator.createResponseOfMultipleCharacterPossibilites(multipleCharacterPossibilitesAsString, messageSignature)

        connManager.sendBytes(responseAsBytes)
    else:
        print("received unknown message:")
        for i in range(len(receivedBytes)):
            print(receivedBytes[i], )


    #for i in range(len(receviedBytes)):
    #    print(receviedBytes[i], )

print("press enter to close python client")
_ = input()
