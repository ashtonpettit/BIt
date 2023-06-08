import serial
import sys
import crcmod.predefined
import re
from tabulate import tabulate
import serial.tools.list_ports
import time
import mysql.connector
from datetime import datetime

#Connect to DB
mydb = mysql.connector.connect(
  host="ID328605_energy.db.webhosting.be",
  user="ID328605_energy",
  password="energy123",
  database="ID328605_energy"
)
print(mydb)
mycursor = mydb.cursor()

#Connect to correct port COM4 for pc, /dev/ttyUSBO for rasppi
ports = serial.tools.list_ports.comports()
serialInst= serial.Serial()
portlist=[]

for onePort in ports:
    portlist.append(str(onePort))
    print(str(onePort))
    
myport=portlist[0][0:12]
print(myport)

#Set Baudrate and open port
serialInst.baudrate =115200
serialInst.port=myport
serialInst.open()

#Getting the stuff
#make into a function
while True:
    #read in one complete reading, taking only what is needed, then insert into database
    #timestamp[1] is the timestamp needed
    if serialInst.in_waiting:
        
        packet = serialInst.readline()
        reading = packet.decode('utf').rstrip('\n')
        if "0-0:1.0.0" in reading:
            timestamp=reading.replace('('," ").replace('W'," ").replace("S","").replace(')',"").split(' ')
            #print(timestamp[1])
            dt_obj=datetime.strptime(timestamp[1].strip(),'%y%m%d%H%M%S')
            #print(dt_obj)
        elif "1-0:1.8.1" in reading:
            dayout=reading.replace('('," ").replace('*'," ").split(' ')
            #print(dayout[1])
        elif "1-0:1.8.2" in reading:
            nightout=reading.replace('('," ").replace('*'," ").split(' ')
            #print(nightout[1])
            
            #print(timestamp[1],dayout[1],nightout[1])
            sql = "INSERT INTO constant_reading (datetime,total_day_consumption, total_night_consumption) VALUES (%s, %s, %s)"
            val = (dt_obj, dayout[1],nightout[1])
            mycursor.execute(sql, val)

            mydb.commit()
            timestamp=""
            dayout=""
            nightout=""
            print(mycursor.rowcount, "record inserted.")