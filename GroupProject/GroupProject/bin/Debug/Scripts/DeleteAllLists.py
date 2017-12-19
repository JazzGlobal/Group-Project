#Sets Directory to the Directory.GetCurrentDirectory() of the project.
#The script then deletes all of the files in the Material Lists directory (GetCurrentDirectory).
#Use this script if, and only if, clearing the Material List directory is desired.

import os

print os.getcwd()

def SetDirectory(path):
    os.chdir(path)
