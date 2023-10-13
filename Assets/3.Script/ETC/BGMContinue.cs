using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMContinue : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
    }
}
