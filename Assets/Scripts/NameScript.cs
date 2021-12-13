using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NameScript : MonoBehaviour
{
    public static string playerName;

    public void Start()
    {
        

    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //playerName = "test";
    }

    public static void SaveName(string name)
    {
        playerName = name;
        
        // need to get the player name from the textbox
    }
}

