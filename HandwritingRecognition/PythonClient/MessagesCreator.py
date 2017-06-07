"""
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

"""

class MessagesCreator:
    def __init__(self):
        pass

    def createClientReadyMessage(self):
        ret = bytearray()
        ret.append(4)
        return ret
    
    def createResponseOfMultipleCharacterPossibilites(self, multipleCharacterPossibilitesAsString, messageSignature):
        # message of type 2
        lengthOfString = len(multipleCharacterPossibilitesAsString)
        responseAsBytes = bytearray(1 + 1 + 4 + lengthOfString);

        responseAsBytes[0] = 2
        responseAsBytes[1] = lengthOfString
        responseAsBytes[2] = messageSignature[0]
        responseAsBytes[3] = messageSignature[1]
        responseAsBytes[4] = messageSignature[2]
        responseAsBytes[5] = messageSignature[3]

        cnt = 5
        for i in range(len(multipleCharacterPossibilitesAsString)):
            cnt = cnt + 1
            responseAsBytes[cnt] = ord(multipleCharacterPossibilitesAsString[i])

        return responseAsBytes
