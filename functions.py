import os
import re
import pickle

from data import *


def getMaps(mapDir):
    """Return the list of files in given directory"""
    index = 0
    files = []
    for fileName in os.listdir(mapDir):
        if fileName.endswith(".txt"):
            files.append(fileName)
    files.sort()
    return files

def chooseMap(files):
    """Input: choose one existing map"""
    mapId = -1
    while mapId <= 0 or mapId > len(files) :
        mapId = input("Enter a maze number to start playing: ") 
        try:
            mapId = int(mapId)
        except ValueError:
            print("You did not enter a valid number.") 
            mapId = -1
            continue        
    return mapId

def chooseToContinueGame():
    """Input: choose to continue or not a previous game"""
    choice = ""
    while choice == "":
        choice = input("Would you like to continue it? (y/n)")
        if choice.lower() not in ["y", "n"]:
            choice = ""
    return choice

def checkOngoingGame(file):
    """Check for an ongoing game on the chosen map"""
    if os.path.isfile(file):
        return True
    else:
        return False

def formatDirection(choice):
    """Check for a valid direction command"""
    direction = ""
    nCells = 0
    
    if re.search(r"^[NESW]$", choice):              # direction
        direction= choice
        nCells = 1
    elif re.search(r"^[NESW][0-9]+$", choice):      # direction followed by the number of steps
        direction = choice[0:1]
        nCells    = choice[1:]
        
        try:
            nCells = int(nCells)
        except ValueError:
            print("You did not enter a valid direction.")
            direction = ""
            nCells = ""
    else:
        print("Error : unknown command!")
    
        
    return direction, nCells

def deleteSavedGame(file):
    """Delete the saved game once it's finished"""
    if os.path.isfile(file):
        os.remove(file)

def readGame(file):
    """Read an ongoing game from a binary file"""
    if os.path.isfile(file):
        with open(file, 'rb') as fGame:
            sGame = pickle.Unpickler(fGame)	
            game  = sGame.load()
    else:
        game = {}
    return game

def saveGame(file, game):
    """Save the ongoing game to a binary file"""
    try:
        os.stat(gameDir)
    except:
        os.mkdir(gameDir)
    
    with open(file, 'wb') as fGame:
        sGame = pickle.Pickler(fGame)	
        sGame.dump(game)

