using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;           //캐릭터 속도
    [SerializeField] private GameObject Player;         //캐릭터 오브젝트

    public Vector2 InputVector;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        InputVector.x = Input.GetAxisRaw("Horizontal");        //수평이동  
        InputVector.y = Input.GetAxisRaw("Vertical");          //수직이동
    }

    private void FixedUpdate()
    {
        Vector2 nextVector = InputVector.normalized * MoveSpeed * Time.fixedDeltaTime;          //물리프레임 하나가 소모한 시간만큼
        rigid.MovePosition(rigid.position + nextVector);        //위치 이동
    }
}
