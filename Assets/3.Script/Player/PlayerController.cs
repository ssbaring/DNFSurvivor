using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;           //ĳ���� �ӵ�
    [SerializeField] private GameObject Player;         //ĳ���� ������Ʈ

    private Animator anim;
    private SpriteRenderer sprite;
    public Vector2 InputVector;
    private Rigidbody2D rigid;
    public Scanner scan;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        scan = GetComponent<Scanner>();
    }

    private void Update()
    {
        InputVector.x = Input.GetAxisRaw("Horizontal");        //�����̵�  
        InputVector.y = Input.GetAxisRaw("Vertical");          //�����̵�
    }

    private void FixedUpdate()
    {
        Vector2 nextVector = InputVector.normalized * MoveSpeed * Time.fixedDeltaTime;          //���������� �ϳ��� �Ҹ��� �ð���ŭ
        rigid.MovePosition(rigid.position + nextVector);        //��ġ �̵�
    }

    private void LateUpdate()
    {
        TurnCharacter();
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }

    private void TurnCharacter()    //ĳ���� �¿� ����
    {
        if(InputVector.x < 0)
        {
            sprite.flipX = true;
        }
        else if(InputVector.x > 0)
        {
            sprite.flipX = false;
        }
    }
}
