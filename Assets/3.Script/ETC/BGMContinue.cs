using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMContinue : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void StopMusic()
    {

    }
}
