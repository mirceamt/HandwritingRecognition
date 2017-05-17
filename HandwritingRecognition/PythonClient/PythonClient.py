import ConnectionManager

connManager = ConnectionManager.ConnectionManager()

host = "127.0.0.1"
port = 13000

connManager.connect(host, port)

receviedBytes = connManager.receiveBytes()

for i in range(len(receviedBytes)):
    print(receviedBytes[i], )
