using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMasterSkill : MonoBehaviour
{
    private Animator WMSkill;
    private CapsuleCollider2D Capcol;
    private void Awake()
    {
        WMSkill = GetComponent<Animator>();
        Capcol = GetComponent<CapsuleCollider2D>();
    }



    private void Start()
    {
        Capcol.enabled = false;
        StopCoroutine(Skill());
        StartCoroutine(Skill());
    }



    private IEnumerator Skill()
    {
        WaitForSeconds cooltime = new WaitForSeconds(3);                //��ų ��Ÿ��
        WaitForSeconds skilltime = new WaitForSeconds(0.3f);        //��ų ���� �ð�

        while (true)
        {
            Capcol.enabled = false;                //��ų ����Ʈ�� ���� �� �ݶ��̴� ��Ȱ��ȭ
            WMSkill.SetBool("IsFinish", true);     //��ų ���� ���� true(��)
            yield return cooltime;

            Capcol.enabled = true;                 //��ų ����Ʈ�� ���� �� �ݶ��̴� Ȱ��ȭ
            WMSkill.SetBool("IsFinish", false);    //��ų ���� ���� false(����)
            yield return skilltime;
        }

    }
}
