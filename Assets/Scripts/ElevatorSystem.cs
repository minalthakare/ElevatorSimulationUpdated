using UnityEngine;

public class ElevatorSystem : MonoBehaviour
{
    // Array holding all elevators in the system
    public Elevator[] elevators;

    // Function called when any floor button is pressed
    public void RequestElevator(int floor)
    {
        // Variable to store the best (nearest) elevator
        Elevator bestElevator = null;

        // Start with a very large distance (so any real distance will be smaller)
        int minDistance = int.MaxValue;

        // Loop through all elevators to find the best one
        foreach (var elevator in elevators)
        {
            // Calculate distance between elevator's current floor and requested floor
            int distance = Mathf.Abs(elevator.currentFloor - floor);

            // Condition to select elevator:
            // 1. Distance should be minimum (nearest elevator)
            // 2. Elevator should be idle (no pending requests)
            if (distance < minDistance && elevator.requestQueue.Count == 0)
            {
                // Update minimum distance
                minDistance = distance;

                // Assign this elevator as best candidate
                bestElevator = elevator;
            }
        }

        // If we found a suitable elevator
        if (bestElevator != null)
        {
            // Send request to that elevator
            bestElevator.AddRequest(floor);
        }
    }
}