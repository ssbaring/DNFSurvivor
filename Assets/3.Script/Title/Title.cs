using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject[] title;

    private void Awake()
    {
        StartCoroutine(SetVolume());
    }

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

    private IEnumerator SetVolume()
    {
        title[1].transform.localScale = Vector3.zero;
        title[1].SetActive(true);

        yield return new WaitForSeconds(1f);

        title[1].transform.localScale = Vector3.one;
        title[1].SetActive(false);
    }

}
