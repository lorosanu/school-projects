import os

class RobotPosition:
    """A class representing the robot position:
    - posX = position in the X axis
    - posY = position in the Y axis"""

    def __init__(self, posX, posY):
        """Initialize the robot position"""
        self._posX = posX
        self._posY = posY

    def _get_posX(self):
        """Get the X position of the robot"""
        return self._posX
    
    def _get_posY(self):
        """Get the Y position of the robot"""
        return self._posY
    
    def _set_posX(self, posX):
        """Set the X position of the robot"""
        self._posX = posX
    
    def _set_posY(self, posY):
        """Set the Y position of the robot"""
        self._posY = posY
    
    posX = property(_get_posX, _set_posX)
    posY = property(_get_posY, _set_posY)
    

class Maze:
    """A class representing the map of the maze and controlling the robot's position
    - map      = the map of the maze in the X,Y dimension
    - sizeX    = the size of the map in the X axis
    - sizeY    = the size of the map in the Y axis    
    - robot    = the robot's position in the X,Y dimension
    - finished = state if the robot reached an exit"""

    def __init__(self, file):
        """Initialize the map of the maze and the robot position from file"""

        self._finished = False
        
        if os.path.isfile(file):
            with open(file, 'r') as fMap:
                content = fMap.read()                
                lines   = content.split("\n")
                
                self._sizeX = len(lines)
                self._sizeY = len(lines[0])
                self._map   = [ [" " for x in range(self._sizeY)] for x in range(self._sizeX)]
                      
                for i, item in enumerate(lines):
                    for j in range(self._sizeY):
                        if item[j].upper() == "X":              # robot position
                            self._robot = RobotPosition(i, j)
                            self._map[i][j] = " "
                        else:
                            self._map[i][j] = item[j].upper()
        else:
            print("Error: file {} not found !".format(file))
            raise(FileNotFoundError)

    def getSize(self):
        """Get the size of the maze"""
        return self._sizeX, self._sizeY

    def gameFinished(self):        
        """Get the state of the current game"""
        return self._finished
    
    def getContent(self, posX, posY):
        """Check the content of the cell in position [posX, posY]"""
        
        if posX >= 0 and posX < self._sizeX and posY >=0 and posY < self._sizeY:
            if posX == self._robot.posX and posY == self._robot.posY:
                return "X"
            else:
                return self._map[posX][posY]
        else:
            return "out of boundaries"

    def getRobotPosition(self):
        """Get the current position of the robot"""
        return self._robot.posX, self._robot.posY
    
    def checkMovePosition(self, posX, posY):
        """Check if the move call points to a valid position"""

        if self.getContent(posX, posY) in [" ", ".", "U"]:
            return True
        else:
            return False

    def moveRobot(self, direction):
        """Move the robot to the chosen direction (when direction is valid) by 1 step;
        Check if the robot reached an exit."""
        rX, rY = self.getRobotPosition()
  
        if not self._finished and direction in ["N", "E", "S", "W"]:
            newX = rX
            newY = rY
            if direction == "N":
                    newX = rX - 1
            elif direction == "E":
                    newY = rY + 1
            elif direction == "S":
                    newX = rX + 1
            elif direction == "W":
                    newY = rY - 1

            if self.checkMovePosition(newX, newY):                       
                    self._robot.posX = newX
                    self._robot.posY = newY
                    print(self)

                    if self._map[newX][newY] == "U" :
                        print("Congratulations! You won!")
                        self._finished = True
            else:
                print("Error: can not move the robot to the designated position!")
                    
                
    def __repr__(self):
        """Return the maze contents"""
        contents = "\n"
        for i in range(self._sizeX):
            for j in range(self._sizeY):
                if i == self._robot.posX and j == self._robot.posY:
                    contents += "X"
                else:
                    contents += self._map[i][j]
            contents += "\n"
        return contents

    def __str__(self):
        """Return the maze contents"""
        return repr(self)
        
        
    
