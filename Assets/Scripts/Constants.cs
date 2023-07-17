using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public enum TriggerTag
    {
        Wall = 0,
        Enemy = 1,
        Body = 2,
        Food = 3
    }

    public static TriggerTag s_TriggerTag;
}
