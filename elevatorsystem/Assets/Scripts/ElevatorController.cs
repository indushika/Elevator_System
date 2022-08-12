using System;
using System.Collections.Generic;
using UnityEngine;


public class ElevatorController : MonoBehaviour
{
    public List<Elevator> elevators;

    // Start is called before the first frame update
    void Start()
    {
        //add elevator with access to levels between 5 - 15 
        //these can be hooked up to UI (buttons), or GameObjects where the ID will be fed in to the ID property 
        //the list of elevators helps the code to be scalable and easy to maintain 
        AddElevator(0, 5, 15);

        //call elevator to the 7th floor 
        CallElevatorUp(0, 7);

        //select a floor to travel to 
        SelectDestinationLevel(0, 14); 
    }

   
    public void AddElevator(int ID, int FirstLevel, int FinalLevel)
    {
        Elevator elevator = new Elevator(ID, FirstLevel, FinalLevel);
        elevator.currentLevel = FirstLevel; 
        elevators.Add(elevator); 

    } 

    public void CallElevatorUp(int ID, int CurrentLevelIndex)
    {
        if (elevators.Count>ID)
        {
            elevators[ID].OnUpCall(CurrentLevelIndex); 
        }
    }

    public void CallElevatorDown(int ID, int CurrentLevelIndex)
    {
        if (elevators.Count > ID)
        {
            elevators[ID].OnDownCall(CurrentLevelIndex);
        }
    } 

    public void SelectDestinationLevel(int ID, int DestinationLevelIndex)
    {
        if (elevators.Count > ID)
        {
            elevators[ID].OnPickLevel(DestinationLevelIndex);
        }
    }
}
