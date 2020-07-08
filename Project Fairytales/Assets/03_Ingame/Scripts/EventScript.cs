using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScript : MonoBehaviour
{
    public delegate void ChangeMap();

    public static event ChangeMap ChangeMapEvent;

    void BeBlackScreen()
    {
        Time.timeScale = 0;

    }
}
