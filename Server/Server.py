
import socket

# Set the server address
SERVER_ADDRESS = ('127.0.0.1', 5000)

# Configure the socket
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_socket.bind(SERVER_ADDRESS)
server_socket.listen(10)
print('[server]: server is running, please, press ctrl+c to stop')

# Listen to requests
while True:
    connection, address = server_socket.accept()
    try:
        print("[server]: new connection from {address}".format(address=connection))
        while True:
            data = connection.recv(1024).decode("utf-8") 
            print("[server]: received from client: `{}` sending same message back.".format(data))
            connection.send(bytes(data, encoding='UTF-8'))
           # if str(data) == "exit":
              #  break
        connection.close()
    except Exception as ex: 
        print ("[server]: {exception}".format(exception=ex))