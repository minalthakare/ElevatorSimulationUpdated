using UnityEngine;

// Handles UI button press for each floor
public class FloorButton : MonoBehaviour
{
    public int floorNumber;              // Floor assigned to button
    public ElevatorSystem system;      // Reference to manager

    // Called when button is clicked
    public void OnPress()
    {
        system.RequestElevator(floorNumber);
    }
}