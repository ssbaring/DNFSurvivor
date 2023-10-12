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

    private int WMLevel = 0;
    private float WMCoolTime;
    private float BSKCoolTime;

    private PlayerController player;

    private void Awake()
    {
        player = GameManager.instance.player;
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            case 1:
                WMCoolTime += Time.deltaTime;
                if (WMCoolTime > speed)
                {
                    WMCoolTime = 0;
                    if (WMLevel <= 1)
                    {
                        WeaponMaster();
                    }
                    else if (WMLevel > 1)
                    {
                        StartCoroutine(ActiveTwice());
                    }
                }

                if (player.InputVector.x < 0)
                {
                    transform.localRotation = Quaternion.Euler(0f, -180f, 0f);
                    transform.localPosition = Vector3.left;
                }
                else if (player.InputVector.x > 0)
                {
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    transform.localPosition = Vector3.right;
                }
                break;
            case 2:
                BSKCoolTime += Time.deltaTime;
                if (BSKCoolTime > speed)
                {
                    BSKCoolTime = 0;
                    Berserker();
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

    }


    public void LevelUP(float damage, int count)
    {
        this.damage = damage;
        this.count += count;
        switch (id)
        {
            case 0:
                Asura();
                break;
            case 1:
                WMLevel++;
                Debug.Log(WMLevel);
                WeaponMaster();
                break;

        }
        player.BroadcastMessage("Apply", SendMessageOptions.DontRequireReceiver);       //이후에 스킬을 찍어도 포션이 적용됨
    }

    public void Init(ItemData data)
    {
        //기본 세팅
        name = "Skill : " + data.name;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        //상세 정보
        id = data.itemID;
        damage = data.baseDamage;
        count = data.baseCount;

        for (int i = 0; i < GameManager.instance.pool.prefabs.Length; i++)
        {
            if (data.projectile == GameManager.instance.pool.prefabs[i])
            {
                prefabID = i;
                break;
            }
        }

        switch (id)
        {
            case 0:
                speed = 150;
                Asura();
                break;
            case 1:
                speed = 3f;
                break;
            case 2:
                speed = 5f;
                break;
            case 3:
                speed = 1f;
                break;
            default:
                break;
        }

        player.BroadcastMessage("Apply", SendMessageOptions.DontRequireReceiver);       //이후에 스킬을 찍어도 포션이 적용됨
    }

    private void Asura()
    {
        for (int i = 0; i < count; i++)
        {
            Transform Asura;        //부모 오브젝트 바꾸기 위해
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


            Vector3 rotateVec = Vector3.forward * 360 * i / count;      //일정한 각도마다 구체 배치
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

            WeaponMaster = GameManager.instance.pool.GetPool(prefabID).transform;
            WeaponMaster.parent = transform;

            WeaponMaster.localPosition = Vector3.zero;
            WeaponMaster.localRotation = Quaternion.identity;


            WeaponMaster.GetComponent<SkillManager>().Init(damage, -1);
        }
    }

    private IEnumerator ActiveTwice()
    {
        WeaponMaster();
        yield return new WaitForSeconds(0.5f);
        WeaponMaster();
    }

    private void Berserker()
    {
        Transform Berserker;
        Berserker = GameManager.instance.pool.GetPool(prefabID).transform;
        Berserker.parent = transform;

        Berserker.localPosition = Vector3.zero;

        Berserker.GetComponent<SkillManager>().Init(damage, -1);


    }

    private void Soulbringer()
    {
        if (!player.scan.nearestTaget) return;

        Vector3 targetPos = player.scan.nearestTaget.position;              //목표 위치
        Vector3 targetDir = (targetPos - transform.position).normalized;    //목표 방향

        Transform Soulbringer = GameManager.instance.pool.GetPool(prefabID).transform;

        Soulbringer.position = transform.position;
        Soulbringer.rotation = Quaternion.FromToRotation(Vector3.right, targetDir);

        Soulbringer.GetComponent<SkillManager>().Init(damage, 0, targetDir);
    }

}
