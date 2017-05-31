from enum import Enum, unique

@unique
class MessagesMeaning(Enum):
    unknownMessage = -1
    classifyImage = 1
    closePythonClient = 100
    

class MessagesInterpreter:
    def __init__(self):
        self.closePythonClient = 100
        self.classifyImage = 1

    def interpretMessage(self, byte):
        if byte == 1:
            return MessagesMeaning.classifyImage
        elif byte == 100:
            return MessagesMeaning.closePythonClient
        else:
            return MessagesMeaning.unknownMessage