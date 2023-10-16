using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clear : MonoBehaviour
{
    Text text;
    private void Awake()
    {
        text = GetComponent<Text>();
        ClearResult();
    }
    public void ClearResult()
    {
        text.text = string.Format("�� óġ Ƚ�� : {0:F0}", GameManager.instance.kill);
    }
}
