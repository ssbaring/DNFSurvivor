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
    public float shotdelay;

    private PlayerController player;
    private SpriteRenderer spriteR;

    private void Awake()
    {
        spriteR = GetComponent<SpriteRenderer>();
        player = GetComponentInParent<PlayerController>();
    }

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
            case 1:
                if (player.InputVector.x > 0)
                {
                    if (transform.localScale.x < 0)
                    {
                        transform.localScale *= -player.InputVector.x;
                    }
                    else
                        break;
                }
                else if (player.InputVector.x < 0)
                {
                    if (transform.localScale.x < 0) break;
                    else
                    {
                        transform.localScale *= player.InputVector.x;
                    }
                }
                break;
            case 3:
                shotdelay += Time.deltaTime;
                if (shotdelay > speed)
                {
                    shotdelay = 0;
                    Soulbringer();
                }
                break;
            default:
                break;

        }

        if (Input.GetButtonDown("Jump"))
        {
            LevelUP(5, 1);
        }
    }





    public void LevelUP(float damage, int count)
    {
        //int level = 0;
        this.damage += damage;
        this.count += count;

        if (id == 0)
        {
            if (count == 5) return;
            Asura();
        }
        else if (id == 1)
        {
            WeaponMaster();
        }
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = -150;
                Asura();
                break;
            case 1:
                speed = 0;
                WeaponMaster();
                break;
            case 2:
                speed = 0;
                Berserker();
                break;
            case 3:
                speed = 1f;
                break;
            default:
                break;
        }
    }

    private void Asura()
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

            Asura.GetComponent<SkillManager>().Init(damage, -1);         //-1은 무한관통력
        }
    }

    private void WeaponMaster()
    {
        for (int i = 0; i < count; i++)
        {
            Transform WeaponMaster;
            if (i < transform.childCount)
            {
                WeaponMaster = transform.GetChild(i);
            }
            else
            {
                WeaponMaster = GameManager.instance.pool.GetPool(prefabID).transform;
                WeaponMaster.parent = transform;
            }

            WeaponMaster.localPosition = Vector3.zero;

            WeaponMaster.Translate(Vector3.right * 2, Space.World);


            WeaponMaster.GetComponent<SkillManager>().Init(damage, -1);
        }


    }

    private void Berserker()
    {
        Transform Berserker;
        for (int i = 0; i < count; i++)
        {
            if (i < transform.childCount)
            {
                Berserker = transform.GetChild(i);
            }
            else
            {
                Berserker = GameManager.instance.pool.GetPool(prefabID).transform;
                Berserker.parent = transform;
            }

            Berserker.localPosition = Vector3.zero;

            Berserker.Translate(Vector3.zero, Space.World);

            Berserker.GetComponent<SkillManager>().Init(damage, -1);
        }

    }

    private void Soulbringer()
    {
        if (!player.scan.nearestTaget) return;

        Vector3 targetPos = player.scan.nearestTaget.position;
        Vector3 targetDir = (targetPos - transform.position).normalized;

        Transform Soulbringer = GameManager.instance.pool.GetPool(prefabID).transform;

        Soulbringer.position = transform.position;
        Soulbringer.rotation = Quaternion.FromToRotation(Vector3.right, targetDir);

        Soulbringer.GetComponent<SkillManager>().Init(damage, 0, targetDir);
    }

}
