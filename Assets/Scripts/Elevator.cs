using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Elevator : MonoBehaviour
{
    public int elevatorID;
    public float speed = 5f; // increased speed (you can tweak in Inspector)

    public List<int> requestQueue = new List<int>();

    public int currentFloor = 0;
    public bool isMoving = false;

    public Transform[] floorPositions;
    public TextMeshProUGUI floorText;

    private Vector2 targetPosition; // UI uses Vector2 (anchoredPosition)
    private RectTransform rect;     // reference to UI RectTransform

    void Start()
    {
        // Get RectTransform (VERY IMPORTANT for UI movement)
        rect = GetComponent<RectTransform>();

        UpdateUI();
    }

    void Update()
    {
        // If there are pending requests and elevator is idle
        if (requestQueue.Count > 0 && !isMoving)
        {
            MoveToFloor(requestQueue[0]);
        }

        // If elevator is moving
        if (isMoving)
        {
            // Move only in UI space (anchoredPosition)
            Vector2 newPos = Vector2.MoveTowards(
                rect.anchoredPosition,
                targetPosition,
                speed * Time.deltaTime * 100 // multiply for UI scale
            );

            // 🔒 LOCK X (prevents sideways movement)
            newPos.x = rect.anchoredPosition.x;

            rect.anchoredPosition = newPos;

            // Check if reached target
            if (Vector2.Distance(rect.anchoredPosition, targetPosition) < 1f)
            {
                isMoving = false;

                currentFloor = requestQueue[0];
                requestQueue.RemoveAt(0);

                UpdateUI();
            }
        }
    }

    public void AddRequest(int floor)
    {
        if (!requestQueue.Contains(floor))
        {
            requestQueue.Add(floor);
        }
    }

    void MoveToFloor(int floor)
    {
        // Take only Y position from floor
        float targetY = floorPositions[floor].GetComponent<RectTransform>().anchoredPosition.y;

        // Keep X same, only change Y
        targetPosition = new Vector2(rect.anchoredPosition.x, targetY);

        isMoving = true;
    }

    void UpdateUI()
    {
        floorText.text = "Elevator " + elevatorID + " : Floor " + currentFloor;
    }
}