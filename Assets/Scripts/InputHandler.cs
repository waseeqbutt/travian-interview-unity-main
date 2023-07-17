using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static bool UpButton = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            BroadcastSystem.OnClickChangeDirectionEvent?.Invoke(MovementDirection.Up);

        if (Input.GetKeyDown(KeyCode.DownArrow))
            BroadcastSystem.OnClickChangeDirectionEvent?.Invoke(MovementDirection.Down);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            BroadcastSystem.OnClickChangeDirectionEvent?.Invoke(MovementDirection.Left);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            BroadcastSystem.OnClickChangeDirectionEvent?.Invoke(MovementDirection.Right);

    }
}
