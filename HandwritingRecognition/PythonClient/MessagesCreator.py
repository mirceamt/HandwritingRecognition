"""
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

    -- client ready
    msg[0] = 4    

    -- close python client
    msg[0] = 100

"""

class MessagesCreator:
    def __init__(self):
        pass

    def createClientReadyMessage():
        ret = bytearray()
        ret.append(4)
        return ret
    
