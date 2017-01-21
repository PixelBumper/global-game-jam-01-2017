using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Keypad : MonoBehaviour
{

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void PressedKey(String key)
    {
        Debug.LogError("result: "+ key);
        int result;
        if (int.TryParse(key, out result))
        {
            Debug.LogError("number: "+ result);
        }
        else
        {
            //fry hamster
        }
    }
}
