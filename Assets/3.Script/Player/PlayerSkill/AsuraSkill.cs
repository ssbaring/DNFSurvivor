using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsuraSkill : MonoBehaviour
{
    public float damage;
    public int per;             //����

    public void Init(float damage, int per)
    {
        this.damage = damage;
        this.per = per;         
    }
}
