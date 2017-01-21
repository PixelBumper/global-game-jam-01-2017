using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWires : PuzzleModule {
    // Use this for initialization
    public override void OnPlayerProgress(GameProgress progress)
    {

    }

    public override void OnBecomeInteractable()
    {

    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Click SimpleWires");

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Test");

                BoxCollider bc = hit.collider as BoxCollider;
                if (bc != null)
                {
                    Destroy(bc.gameObject);
                }
            }
        }
    }

    private void OnMouseUpAsButton()
    {
        Debug.Log("OnMouseUpAsButton");
    }
}
