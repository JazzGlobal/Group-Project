#Sets Directory to the Directory.GetCurrentDirectory() of the project.
#The script then deletes all of the files in the Material Lists directory (GetCurrentDirectory).
#Use this script if, and only if, clearing the Material List directory is desired.

import os
from sys import argv

script, filename = argv
myfile = open(filename)
newdir = myfile.readline()
os.chdir(newdir.rstrip())
myfile.close()

dirPath = os.getcwd()
fileList = os.listdir(dirPath)
for fileName in fileList:
    os.remove(dirPath+"/"+fileName)
