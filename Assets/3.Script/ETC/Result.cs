using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public GameObject[] result;

    public void Lose()
    {
        result[0].SetActive(true);
    }
    
    public void Win()
    {
        result[1].SetActive(true);
    }
}
