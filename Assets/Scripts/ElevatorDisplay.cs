using UnityEngine;
using TMPro;

// This script displays Elevator Name + Current Floor on UI
public class ElevatorDisplay : MonoBehaviour
{
    // Reference to the elevator script (to get current floor)
    public Elevator elevator;

    // Text UI where we show info
    public TextMeshProUGUI displayText;

    // Custom name for elevator (Example: "E1", "Elevator 1")
    public string elevatorName;

    void Update()
    {
        // Check if both elevator and UI text are assigned
        if (elevator != null && displayText != null)
        {
            // Update text every frame with elevator name + current floor
            displayText.text = elevatorName + " : Floor " + elevator.currentFloor;
        }
    }
}