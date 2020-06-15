using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (!_instance) 
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

            return _instance;
        }
    }
    #endregion

    public PlayerScript Player;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(2160, 3840, true);
    }
}
