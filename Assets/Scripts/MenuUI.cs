using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public InputField input;
    // Start is called before the first frame update
    void Start()
    {
        input = GameObject.Find("InputField").GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ClickStart()
    {
        //Debug.Log(input.text);

        NameScript.SaveName(input.text);
        SceneManager.LoadScene(1);   
    }
}
