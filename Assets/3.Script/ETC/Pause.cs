using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pause;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!GameManager.IsPause)
            {
                ActivePause();
            }
            else
            {
                InActivePause();
            }
        }
    }

    public void ActivePause()
    {
        GameManager.IsPause = true;
        pause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void InActivePause()
    {
        GameManager.IsPause = false;
        pause.SetActive(false);
        Time.timeScale = 1f;
    }
}
