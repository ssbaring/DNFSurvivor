using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float speed;
    public float HP;
    public float MaxHP;
    public int Damage;
    public RuntimeAnimatorController[] animatorCon;     //���� ������ ���� �ִϸ��̼� �ٲٱ�
    public Rigidbody2D target;      //�������� Ÿ��

    private bool isLive;           //��������
    private Rigidbody2D rigid;
    private Collider2D coll;

    private Animator anim;
    private SpriteRenderer spriteR;

    public AudioClip Hit;
    private AudioSource audio;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        audio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()      //������
    {
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return;
        }
        //anim.SetBool("IsDead", false);
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
        coll.enabled = true;       //�ݶ��̴� Ȱ��ȭ
        spriteR.sortingOrder = 2;
        rigid.simulated = true;    //������ٵ� Ȱ��ȭ
        anim.SetBool("IsDead", false);
        HP = MaxHP;         //��Ȱ�� Ǯü��
    }

    public void Init(SpawnData data)      //������ �ޱ�
    {
        anim.runtimeAnimatorController = animatorCon[data.spriteType];
        speed = data.speed;
        MaxHP = data.HP;
        HP = data.HP;
        Damage = data.damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Weapon") || !isLive)
        {
            return;
        }


        HP -= collision.GetComponent<SkillManager>().damage;
        StartCoroutine(KnockBack());
        audio.clip = Hit;
        audio.Play();

        if (HP > 0)
        {
            anim.SetTrigger("Hit");
        }
        else        //���� ���� ��
        {
            isLive = false;
            coll.enabled = false;       //�ݶ��̴� ��Ȱ��ȭ
            spriteR.sortingOrder = 1;
            rigid.simulated = false;    //������ٵ� ��Ȱ��ȭ
            anim.SetBool("IsDead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    private IEnumerator KnockBack()
    {
        WaitForFixedUpdate wfu = new WaitForFixedUpdate();
        yield return wfu;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }
}
