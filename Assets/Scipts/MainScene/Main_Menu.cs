using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Menu : MonoBehaviour
{
    public GameObject _MainMenu;




//START
    void Start()
    {
        
    }

//UPDATE
    void Update()
    {

    }


    //Start New

    public void New_Game()
    {
        _MainMenu.SetActive(false);
    }

    //Load Game





    //Settings





    //Quit Game
    public void Quit()
    {
        //Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
