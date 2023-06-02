using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraPosition : MonoBehaviour
{
    public MouseRay S_MouseRay;


    //Note: Later on change the public to grabbing them automatically at start
    [Header("Camera")]
    private Camera _MainCamera;
    [Header("CameraMovementButtons")]
    public GameObject _MovetoCooking;
    public GameObject _MovetoGuest;
    [Space]

    


    //Position of the two Backgrounds for GUest and Cooking Scene, which are needed to move the 
    //camera down or up 
    Vector3 GuestCamera = new Vector3(944, 538, -994);
    Vector3 CookCamera = new Vector3(944, -544, -994);
    



 //START
    void Start()
    {
        _MainCamera = Camera.main;

    }

//UPDATE
    void Update()
    {
        S_MouseRay.ObjectDetectionbyMouse();


    }



//Functions for Moving the Camera to the Cooking Area
    public void CookingCameraMove()
    {
        StartCoroutine(CookingCameraLogic());
        _MovetoCooking.SetActive(false);
    }

    public IEnumerator CookingCameraLogic()
    {
        float timeSinceStarted = 0f;
        while (_MainCamera.transform.position != CookCamera)
        {
            timeSinceStarted += Time.deltaTime;
            _MainCamera.transform.position = Vector3.Lerp(GuestCamera, CookCamera, timeSinceStarted);
            Debug.Log("CamMoved");

            // If the object has arrived, stop the coroutine
            if (_MainCamera.transform.position == CookCamera)
            {
                yield break;
            }

            // Otherwise, continue next frame
            yield return null;
        }
    }


//Functions for Moving the Camera to the Guest Area
    public void GuestCameraMove()
    {
        StartCoroutine(GuestCameraLogic());
        _MovetoCooking.SetActive(true);
    }

    public IEnumerator GuestCameraLogic()
    {
        float timeSinceStarted = 0f;
        while (_MainCamera.transform.position != GuestCamera)
        {
            timeSinceStarted += Time.deltaTime;
            _MainCamera.transform.position = Vector3.Lerp(CookCamera, GuestCamera, timeSinceStarted);
            Debug.Log("CamMoved");

            // If the object has arrived, stop the coroutine
            if (_MainCamera.transform.position == GuestCamera)
            {
                yield break;
            }

            // Otherwise, continue next frame
            yield return null;
        }
    }















}
