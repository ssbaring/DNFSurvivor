using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DescriptionClass : MonoBehaviour
{
    [SerializeField] private GameObject DescClass;

    public void Active()
    {
        GameManager.instance.playerID = 3;
    }

}
