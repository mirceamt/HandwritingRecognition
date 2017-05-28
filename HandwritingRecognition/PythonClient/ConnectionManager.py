import socket

class ConnectionManager:
    def __init__(self, sock = None):
        self.isConnectedFlag = False
        if sock is None:
            self.sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        else:
            self.sock = sock

    def connect(self, host, port):
        self.isConnectedFlag = False
        try:
            self.sock.connect((host, port))
            self.isConnectedFlag = True
        except ConnectionRefusedError:
            print("ConnectionRefusedError - Could not connect to main program")
            pass

    def isConnected(self):
        if self.sock is None:
            return False
        else:
            return self.isConnectedFlag

    def sendBytes(self, bytes):
        self.sock.send(bytes)

    def receiveBytes(self):
        receivedBytes = self.sock.recv(2048)
        return receivedBytes