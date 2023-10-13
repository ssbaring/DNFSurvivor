using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingGame : MonoBehaviour
{
    [SerializeField] private Image image;

    private void Awake()
    {
        image.GetComponent<Image>();
    }
    private void Start()
    {
        StartCoroutine(FadeOut());

    }
    IEnumerator FadeOut()
    {
        while (image.color.a > 0)
        {
            image.color -= new Color(0, 0, 0, Time.deltaTime);
            yield return null;
        }
        image.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
    }
}
