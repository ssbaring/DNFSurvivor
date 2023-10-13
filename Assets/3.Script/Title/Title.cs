using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject[] title;
    public int id;

    
 
    public void StartGame()
    {
        SceneManager.LoadScene("Loading");
    }


    public void Option()
    {
        title[0].SetActive(false);
        title[1].SetActive(true);
    }

    public void PreviousOption()
    {
        title[1].SetActive(false);
        title[0].SetActive(true);
    }


    public void QuitGame()
    {
        Application.Quit();
    }



}
