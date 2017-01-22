using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneOnClick : MonoBehaviour {
    public string sceneName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0)){
            Application.LoadLevel(sceneName);
        }
	}
}
