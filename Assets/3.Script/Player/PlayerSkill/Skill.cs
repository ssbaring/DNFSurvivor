using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public int id;          //무기 id
    public int prefabID;    //무기 prefabID
    public float damage;
    public int count;
    public float speed;
    private void Start()
    {
        Init();
    }
    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.forward * speed * Time.deltaTime);
                break;
            default:
                break;

        }

        if (Input.GetButtonDown("Jump"))
        {
            LevelUP(30, 1);
        }
    }

    public void LevelUP(float damage, int count)
    {
        this.damage += damage;
        this.count += count;

        if (id == 0)
        {
            SkillCount();
        }
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = -150;
                SkillCount();
                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            default:
                break;

        }
    }

    private void SkillCount()
    {
        for (int i = 0; i < count; i++)
        {
            Transform Asura;
            if (i < transform.childCount)
            {
                Asura = transform.GetChild(i);
            }
            else
            {
                Asura = GameManager.instance.pool.GetPool(prefabID).transform;
                Asura.parent = transform;
            }

            //초기화
            Asura.localPosition = Vector3.zero;
            Asura.localRotation = Quaternion.identity;


            Vector3 rotateVec = Vector3.forward * 360 * i / count;
            Asura.Rotate(rotateVec);
            Asura.Translate(Asura.up * 2, Space.World);

            Asura.GetComponent<AsuraSkill>().Init(damage, -1);         //-1은 무한관통력
        }
    }
}
