import socket

class ConnectionManager:
    def __init__(self, sock = None):
        if sock is None:
            self.sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        else:
            self.sock = sock
    def connect(self, host, port):
        self.sock.connect((host, port))
    def sendBytes(self, bytes):
        self.sock.send(bytes)
    def receiveBytes(self):
        receivedBytes = self.sock.recv(2048)
        return receivedBytes