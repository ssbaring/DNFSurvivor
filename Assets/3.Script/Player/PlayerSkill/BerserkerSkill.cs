using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkerSkill : MonoBehaviour
{
    private Animator BSKSkill;
    private CapsuleCollider2D Capcol;

    private void Awake()
    {
        BSKSkill = GetComponent<Animator>();
        Capcol = GetComponent<CapsuleCollider2D>();

    }
    void Start()
    {
        Capcol.enabled = false;
        StopCoroutine(Skill());
        StartCoroutine(Skill());
    }

    private IEnumerator Skill()
    {
        WaitForSeconds cooltime = new WaitForSeconds(3);
        WaitForSeconds skillframe = new WaitForSeconds(0.084f);

        while (true)
        {
            BSKSkill.SetBool("IsFinish", true);     //스킬 종료 검사
            yield return cooltime;                  //쿨타임 3초 대기

            Capcol.offset = new Vector2(0.1f, 0.1f);      //초기화
            Capcol.size = new Vector2(0.1f, 0.1f);
            Capcol.enabled = true;                  //스킬 시작
            BSKSkill.SetBool("IsFinish", false);    //스킬 종료 검사

            //각 프레임마다 콜라이더 크기 조절
            Capcol.offset = new Vector2(-0.03f, -0.25f);
            Capcol.size = new Vector2(0.9f, 0.7f);
            yield return skillframe;

            Capcol.offset = new Vector2(-0.04f, 0);
            Capcol.size = new Vector2(1.22f, 1.22f);
            yield return skillframe;

            Capcol.offset = new Vector2(-0.06f, -0.06f);
            Capcol.size = new Vector2(1.77f, 1.35f);
            yield return skillframe;

            Capcol.offset = new Vector2(0.025f, -0.02f);
            Capcol.size = new Vector2(3.78f, 2.55f);
            yield return skillframe;

            Capcol.offset = new Vector2(-0.012f, -0.095f);
            Capcol.size = new Vector2(5.6f, 2.84f);
            yield return skillframe;

            Capcol.offset = new Vector2(0.016f, -0.17f);
            Capcol.size = new Vector2(9f, 4f);
            yield return skillframe;
            Capcol.enabled = false;                 //스킬 종료

        }
    }
}
