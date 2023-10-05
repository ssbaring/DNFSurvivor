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
        WaitForSeconds time = new WaitForSeconds(3);                //스킬 쿨타임
        WaitForSeconds skilltime = new WaitForSeconds(0.3f);        //스킬 시전 시간

        while (true)
        {
            Capcol.enabled = false;                                   //스킬 이펙트가 없을 시 콜라이더 비활성화
            WMSkill.SetBool("IsFinish", false);
            yield return time;

            Capcol.enabled = true;                                    //스킬 이펙트가 있을 시 콜라이더 활성화
            WMSkill.SetBool("IsFinish", true);
            yield return skilltime;
        }

    }
}
