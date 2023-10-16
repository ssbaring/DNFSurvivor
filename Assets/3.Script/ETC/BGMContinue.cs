using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMContinue : MonoBehaviour
{
    private AudioSource audio;
    private GameObject[] BGM;
    [SerializeField] private AudioClip[] BGMList;

    private void Awake()
    {
        BGM = GameObject.FindGameObjectsWithTag("BGM");
        audio = GetComponent<AudioSource>();
        StopBGM();
        PlayBGM(0);

        if(BGM.Length >= 2)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(transform.gameObject);

        audio.loop = true;
    }

    

    public void PlayBGM(int index)
    {
        if (audio.isPlaying) return;

        if (index == 2) audio.loop = false;
        audio.clip = BGMList[index];
        audio.Play();
    }


    public void StopBGM()
    {
        audio.Stop();
    }
}
