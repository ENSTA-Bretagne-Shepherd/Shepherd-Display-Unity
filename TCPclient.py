import time
import socket

# Create the socket
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# Connect to server
s.connect(("127.0.0.1", 13000))

t = 0.0
dt = 3
t_max = 50
i=0

msg = [[0] * 1 for _ in range(5)]

#trame :  auv/buoy index speed x y z
# a adapter en fonction des pos initiales des auvs dans unity
msg[0]="auv 0 10 17 12 -35" #ordre a l auv1 de bouger en (17,12,-35) a la vitesse 10m/s
msg[1]="auv 1 10 17 10 -43"
msg[2]="auv 0 10 12 12 -35"
msg[3]="auv 1 10 17 10 -37"

while t < t_max:
    #s.sendall(("temps %f"%(t)).encode('ascii'))
    s.sendall(msg[i%4])
    #print(t)
    t += dt
    time.sleep(dt)
    i+=1
