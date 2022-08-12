using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading; 
using System.Linq; 

public class ElevatorBehaviors : ElevatorBase
{
    public CancellationTokenSource tokenSource = new CancellationTokenSource();

    #region public_methods
    public void OnUpCall(int LevelIndex)
    {
        //this is called when the user presses the button to call the elevator to go to floors above   
        if (bufferLevels.Count == 0)
        {
            destinationLevel = LevelIndex;
            bufferLevels.Add(destinationLevel);
            MoveElevator();
        }
        else
        {
            //wait for the elevator to finish the trip and arrive 
            if (!isCalled)
            {
                isCalled = true;
            }
            if (!bufferLevels.Contains(LevelIndex))
            {
                bufferLevels.Add(LevelIndex);
            }
        }
    }

    public void OnDownCall(int LevelIndex)
    {
        //this is called when the user presses the button to call the elevator to go to floors below 
        if (bufferLevels.Count == 0)
        {
            destinationLevel = LevelIndex;
            bufferLevels.Add(destinationLevel);
            MoveElevator();
        }
        else
        {
            //wait for the elevator to finish the trip and arrive 
            if (!isCalled)
            {
                isCalled = true;
            }
            if (!bufferLevels.Contains(LevelIndex))
            {
                bufferLevels.Add(LevelIndex);
            }
        }

    }

    public void OnPickLevel(int LevelIndex)
    {
        //called when the user presses the button to pick the floor(level) 
        if (!CheckAccess(LevelIndex))
        {
            if (!bufferLevels.Contains(LevelIndex))
            {
                bufferLevels.Add(LevelIndex);
            }
        }
        else
        {
            //prompt the user to scane ID
            //which will call the ScanAccess() method 
        }
    }

    public bool ScanAccess(int PersonID, int LevelIndex)
    {
        bool granted;
        //scans to check if the user has access to that level 
        //if person ID has access return true 
        return true;
    }

    public void OnHoldDoors()
    {
        //called when user presses the button to hold the doors 
        holdDoor = true;
    }

    public void OnCloseDoors()
    {
        //called when user presses the button to close the door  
        holdDoor = false;
        if (!tokenSource.IsCancellationRequested)
        {
            tokenSource.Cancel();
        }
    }

    #endregion

    #region private_methods 

    private void MoveElevator()
    {

        if (destinationLevel == currentLevel)
        {
            OnArrival();
        }
        else
        {
            if (destinationLevel > currentLevel)
            {
                goingUp = true;
                goingDown = false;
                ElevatorLoop();
            }
            else
            {
                goingDown = true;
                goingUp = false;
                ElevatorLoop();
            }
        }

    }

    private void SwitchDirection()
    {
        if (goingDown)
        {
            goingDown = false;
            goingUp = true; 
        }
        else if (goingUp)
        {
            goingUp = false;
            goingDown = true; 
        }
    }

    private async void ElevatorLoop()
    {
        int length = finalLevel-firstLevel; 
        while (bufferLevels.Count>0&&isMoving)
        {
            isMoving = true; 
            if (goingUp)
            {
                for (int i = 0; i < length; i++)
                {
                    await Task.Delay(speed);
                    currentLevel++;
                    DisplayCurrentLevel(); 
                    if (bufferLevels.Contains(currentLevel))
                    {
                        destinationLevel = currentLevel;
                        bufferLevels.Remove(destinationLevel); 
                        isMoving = false; 
                        OnArrival();
                        break; 
                    }
                    int tempLevelIndex = currentLevel;
                    tempLevelIndex = bufferLevels.Max(); 
                    if (tempLevelIndex == currentLevel || currentLevel<=finalLevel)
                    {
                        SwitchDirection();
                    }
                }
            }
            else if (goingDown)
            {
                for (int i = 0; i < length; i++)
                {
                    await Task.Delay(speed);
                    currentLevel--;
                    DisplayCurrentLevel(); 
                    if (bufferLevels.Contains(currentLevel))
                    {
                        destinationLevel = currentLevel;
                        bufferLevels.Remove(destinationLevel);
                        isMoving = false;
                        OnArrival();
                        break;
                    }
                    int tempLevelIndex = currentLevel;
                    tempLevelIndex = bufferLevels.Min(); 
                    if (tempLevelIndex == currentLevel || currentLevel>=firstLevel)
                    {
                        SwitchDirection();
                    }
                }
            }

        }
        
    }


    private bool CheckAccess(int LevelIndex)
    {
        bool restricted = false; 
        //checks if the given level is accessible 
        //if not ask for authorization 
        foreach (int levelIndex in restrictedLevels)
        {
            if (levelIndex == LevelIndex)
            {
                //access restricted
                restricted = true; 
                
            }
        }
        return restricted; 
    } 


    private void OnArrival()
    {
        //actions when the elevator arrives at a level/floor 
        OnDoorOpens();

        OnWaitDoorHold();

        OnDoorClose();
        while (holdDoor)
        {
            holdDoor = false;
            OnWaitDoorHold(); 
        }


        SetCurrentWeight();
        CheckWeightCapacity(); 



        isMoving = true;
        ElevatorLoop(); 

    } 


    private void OnDoorClose()
    {
        //actions to perform when doors close 
        //play a ding to annouce door closing 
    } 

    private void OnDoorOpens()
    {
        //actions to perform when door opens 
        //play ding to annouce arrival 

    }

    private async void OnWaitDoorHold()
    {
        try
        {
            await Task.Delay(speed,tokenSource.Token); 
        }
        catch
        {
            return; 
        }
    }

    private void CheckWeightCapacity()
    {
        //check if the weight has exceeded or not 
        //if weight has exceeded the doors wont close and a warning light will turn on 
    } 

    private void SetCurrentWeight()
    {
        //check current weight of elevator and update current weight 
    }

    private void DisplayCurrentLevel()
    {
        //a display will indicate the current level/floor 
    }

    #endregion
}
