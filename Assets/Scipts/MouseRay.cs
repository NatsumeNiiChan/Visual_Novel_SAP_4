using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MouseRay : MonoBehaviour
{
    private Camera _MainCamera;

    //CUPBOARD
    private GameObject[] _CupboardDoor = new GameObject[6];
    private bool[] _CupboardDoorClosed = new bool[6];
    Vector3[] _DoorVanillaPosition;
    //Animation
    private Animator[] _CD_Animators = new Animator[6];

    //START
    void Start()
    {
        _MainCamera = Camera.main;

        //Get Cupboard Doors
        _CupboardDoor = GameObject.FindGameObjectsWithTag("Cupboard_Door");
        //Find Cupboard Animator
        FindingCall_CD_Animators();



        //Set Cupboard_Doors Vanilla Positions
        _DoorVanillaPosition = _CupboardDoor.Select(p => p.transform.position).ToArray();

    }

//UPDATE
    void Update()
    {
       
    }

    public void ObjectDetectionbyMouse()
    {


        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log($"{hit.collider.name}Detected", hit.collider.gameObject);


                //When one of the cupboard doors was hit
                //1) Move it
                //2) Set the Bool Closed to false
                //-> later on check the bool first, then move it and change bool accordingly
                for (int i = 0; i < _CupboardDoor.Length; i++)
                {
                    if (hit.collider.gameObject == _CupboardDoor[i] && _CupboardDoor[i].transform.position == _DoorVanillaPosition[i])
                    {
                        _CupboardDoorClosed[i] = false;
                        //Debug.Log(_CupboardDoorClosed[i]);
                        //Animation: Move Door Aside     (first check which door was hit, so the right animation can be played)
                        if (hit.collider.gameObject == _CupboardDoor[0]) /*Top Left*/
                        {
                            _CD_Animators[0].SetTrigger("CD_Top_l_open");
                        }
                        if (hit.collider.gameObject == _CupboardDoor[1]) /*Top Right*/
                        {
                            _CD_Animators[1].SetTrigger("CD_Top_r_open");
                        }
                        else if (hit.collider.gameObject == _CupboardDoor[2]) /*Mid Left*/
                        {
                            _CD_Animators[2].SetTrigger("CD_Mid_l_open");
                        }
                        else if (hit.collider.gameObject == _CupboardDoor[3]) /*Mid Right*/
                        {
                            _CD_Animators[3].SetTrigger("CD_Mid_r_open");
                        }
                        else if (hit.collider.gameObject == _CupboardDoor[4]) /*Down Left*/
                        {
                            _CD_Animators[4].SetTrigger("CD_Down_l_open");
                        }
                        else if (hit.collider.gameObject == _CupboardDoor[5]) /*Down Right*/
                        {
                            _CD_Animators[5].SetTrigger("CD_Down_r_open");
                        }

                    }
                    if (hit.collider.gameObject == _CupboardDoor[i] && _CupboardDoor[i].transform.position != _DoorVanillaPosition[i])
                    {
                        _CupboardDoorClosed[i] = true;
                        //Debug.Log(_CupboardDoorClosed[i]);
                        //Animation: Close Door
                        if (hit.collider.gameObject == _CupboardDoor[0]) /*Top Left*/
                        {
                            _CD_Animators[0].SetTrigger("CD_Top_l_closed");
                        }
                        if (hit.collider.gameObject == _CupboardDoor[1]) /*Top Right*/
                        {
                            _CD_Animators[1].SetTrigger("CD_Top_r_closed");
                        }
                        else if (hit.collider.gameObject == _CupboardDoor[2]) /*Mid Left*/
                        {
                            _CD_Animators[2].SetTrigger("CD_Mid_l_closed");
                        }
                        else if (hit.collider.gameObject == _CupboardDoor[3]) /*Mid Right*/
                        {
                            _CD_Animators[3].SetTrigger("CD_Mid_r_closed");
                        }
                        else if (hit.collider.gameObject == _CupboardDoor[4]) /*Down Left*/
                        {
                            _CD_Animators[4].SetTrigger("CD_Down_l_closed");
                        }
                        else if (hit.collider.gameObject == _CupboardDoor[5]) /*Down Right*/
                        {
                            _CD_Animators[5].SetTrigger("CD_Down_r_closed");
                        }

                    }
                }
                

                

            }
            //Check for Door Hit and then move them 






            //if ingredient, beweg in die Mitte




        }


    }




//Finding Calls and Vanilla Positions

    public void FindingCall_CD_Animators()
    {

        for (int i = 0; i < _CD_Animators.Length; i++)
        {
            _CD_Animators[i] = _CupboardDoor[i].GetComponent<Animator>();
            //Debug.Log("Tür zugewiesen",A_ItemDoors[i]);
        }


    }


}
