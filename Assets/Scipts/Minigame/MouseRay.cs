using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRay : MonoBehaviour
{
    private Camera _MainCamera;


//START
    void Start()
    {
        _MainCamera = Camera.main;
    }

//UPDATE
    void Update()
    {
        ObjectDetectionbyMouse();
    }

    public void ObjectDetectionbyMouse()
    {


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log($"{hit.collider.name}Detected", hit.collider.gameObject);



            }


        }


    }


}
