using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorBase
{
    public int id;
    public int speed; //milliseconds (time between each level) 
    public int currentLevel;
    public int firstLevel;
    public int finalLevel;
    public List<int> restrictedLevels;
    public const float maximumWeightCapacity = 2000; //pounds
    public float currentWeight;
    public bool isMoving, goingUp, goingDown;
    public int destinationLevel;
    public List<int> bufferLevels;
    public bool isCalled;
    public const float doorOpenWaitTime = 2000; //milliseconds 
    public bool holdDoor;  
}
