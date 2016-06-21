# -*-coding:Latin-1 -*

from data import *
from functions import *
from maze import *

print("""Game: help a robot escape from a maze of obstacles!

  Maze configuration :
   - walls = 'O'
   - doors = '.'
   - exit cell(s) = 'U'
   - robot = 'X'
       
  You can control the robot by preset keyboard inputs :
  - N = move the robot to the North;
  - E = move the robot to the East;
  - S = move the robot to the South;
  - W = move the robot to the West;
Each of these directions can be followed by a number,
    allowing the robot to advance several cells (ex: E3)
  - Q = save and quit the current game""")


print()
#==================================
# get list of existing maps
#==================================
files = getMaps(mapDir)
print("The available mazes:")
for i, fileName in enumerate(files):
    indexP = fileName.find(".")
    name = fileName[0:indexP]
    print("\t{} : {}".format(name, i+1))


print()
#==================================
# choose map
#==================================
mapId = chooseMap(files)  
chosenMap = mapDir+"//"+files[mapId-1]
savedGame = gameDir+"//"+files[mapId-1][0:files[mapId-1].find(".")]


print()
#==================================
# check for an ongoing game
#==================================
continuePreviousGame = False
checkOngoing = checkOngoingGame(savedGame)

if checkOngoing:
    print("There is an ongoing game on the chosen Map")    
    choice = chooseToContinueGame()

    if choice == "y":
        print("\nContinuing a previous game")
        continuePreviousGame = True
        currentGame = readGame(savedGame) 
    else:
        print("\nStarting a new game")
        currentGame = Maze(chosenMap)
else:
    print("\nStarting a new game")
    currentGame = Maze(chosenMap)

print(currentGame)


#==================================
# play
#==================================
continuePlaying = True

while continuePlaying:    
    choice = input("\nInput your next move > ")
    choice = choice.upper()

    if choice == "Q":        
        print("Save game and exit.")
        saveGame(savedGame, currentGame)
    else:
        direction, nCells = formatDirection(choice)
        if direction != "" and nCells > 0:
            for i in range(nCells):
                currentGame.moveRobot(direction)

    continuePlaying = not currentGame.gameFinished() and choice != "Q" 


#==================================
# delete saved game if finished
#==================================
if continuePreviousGame and currentGame.gameFinished():
    deleteSavedGame(savedGame)



