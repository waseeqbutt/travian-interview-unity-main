using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    public static Bounds Instance;

    public Transform UpperBound;
    public Transform LowerBound;
    public Transform LeftBound;
    public Transform RightBound;

    private void OnEnable()
    {
        Instance = this;
    }
}
