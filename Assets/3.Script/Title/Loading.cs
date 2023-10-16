using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField] private Image image;

    private void Awake()
    {
        image.GetComponent<Image>();

    }
    private void Start()
    {
        image.color = new Color(1, 1, 1, 0);
        StartCoroutine(FadeIn());
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        while (image.color.a < 1)
        {
            image.color += new Color(0, 0, 0, Time.deltaTime * 1.5f);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
    }


    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2f);
        while (image.color.a > 0)
        {
            image.color -= new Color(0, 0, 0, Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainGame");
    }
}
