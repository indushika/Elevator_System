using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : ElevatorBehaviors
{
    public Elevator(int ID, int FirstLevel, int FinalLevel)
    {
        id = ID;
        firstLevel = FirstLevel;
        finalLevel = FinalLevel; 
    }

    public void AddRestrictedLevel(int LevelIndex)
    {
        restrictedLevels.Add(LevelIndex); 
    } 

    public void SetCurrentLevel(int CurrentLevel)
    {
        currentLevel = CurrentLevel; 
    }
  
}
