using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float speed;
    public float HP;
    public float MaxHP;
    public RuntimeAnimatorController[] animatorCon;     //���� ������ ���� �ִϸ��̼� �ٲٱ�
    public Rigidbody2D target;      //�������� Ÿ��

    private bool isLive;           //��������
    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer spriteR;
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()      //������
    {
        if(!isLive)
        {
            return;
        }

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;      //���Ͱ� �ǽð� �����̴¹���
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;                  //�����ӵ��� �̵��� ���� �Ȱ���
    }

    private void LateUpdate()
    {
        if (!isLive)
        {
            return;
        }
        spriteR.flipX = target.position.x < rigid.position.x;
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();       //Ÿ���� �ʱ�ȭ
        isLive = true;
        HP = MaxHP;         //��Ȱ�� Ǯü��
    }

    public void Init(SpawnData data)      //������ �ޱ�
    {
        anim.runtimeAnimatorController = animatorCon[data.spriteType];
        speed = data.speed;
        MaxHP = data.HP;
        HP = data.HP;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Weapon"))
        {
            return;
        }

        HP -= collision.GetComponent<AsuraSkill>().damage;

        if(HP>0)
        {

        }
        else
        {
            Dead();
        }
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }
}
