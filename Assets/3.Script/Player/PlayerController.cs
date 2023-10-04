using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;           //ĳ���� �ӵ�
    [SerializeField] private GameObject Player;         //ĳ���� ������Ʈ

    public Vector2 InputVector;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
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
}
