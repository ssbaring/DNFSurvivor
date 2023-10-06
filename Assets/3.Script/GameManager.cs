using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController player;
    public GameObject BSkill;
    public GameObject WSkill;


    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        DevMode();
    }

    private void DevMode()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && WSkill.activeSelf == false)
        {
            WSkill.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1) && WSkill.activeSelf == true)
        {
            WSkill.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && BSkill.activeSelf == false)
        {
            BSkill.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && BSkill.activeSelf == true)
        {
            BSkill.SetActive(false);
        }
    }

}
