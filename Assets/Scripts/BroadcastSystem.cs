using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastSystem : MonoBehaviour
{
    public static Action<GameObject> OnHitFoodEvent;
    public static Action<string> OnHitWallEvent;
    public static Action<string> OnHitEnemyEvent;
    public static Action<string> OnHitBodyEvent;
    public static Action<MovementDirection> OnClickChangeDirectionEvent;
}
