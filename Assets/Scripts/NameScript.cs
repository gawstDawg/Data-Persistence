using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameScript : MonoBehaviour
{
    public static string playerName;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }

    public static void SaveName()
    {
        playerName = GameObject.Find("InputField").name.ToString();
    }
}

