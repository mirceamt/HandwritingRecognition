import ConnectionManager
import MessagesCreator
from MessagesInterpreter import *

print("Started Python Client...")
connManager = ConnectionManager.ConnectionManager()
messagesCreator = MessagesCreator.MessagesCreator()
messagesInterpreter = MessagesInterpreter()

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
        # TODO
        # use ImageClassifier and send message back to c#
        pass
    else:
        print("received unknown message:")
        for i in range(len(receivedBytes)):
            print(receivedBytes[i], )


        

    #for i in range(len(receviedBytes)):
    #    print(receviedBytes[i], )

print("press enter to close python client")
_ = input()
