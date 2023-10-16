using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;           //캐릭터 속도
    [SerializeField] private GameObject Player;         //캐릭터 오브젝트

    private Animator anim;
    private SpriteRenderer sprite;
    public Vector2 InputVector;
    private Rigidbody2D rigid;
    private Collider2D col;
    public Scanner scan;

    private AudioSource audio;
    public AudioClip deadclip;

    WaitForFixedUpdate wfu = new WaitForFixedUpdate();

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        scan = GetComponent<Scanner>();
        audio = GetComponent<AudioSource>();
        col = GetComponent<Collider2D>();
        MoveSpeed = 5f;
    }

    private void Update()
    {
        if (!GameManager.instance.IsLive) return;

        InputVector.x = Input.GetAxisRaw("Horizontal");        //수평이동  
        InputVector.y = Input.GetAxisRaw("Vertical");          //수직이동
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.IsLive) return;


        Vector2 nextVector = InputVector.normalized * MoveSpeed * Time.fixedDeltaTime;          //물리프레임 하나가 소모한 시간만큼
        rigid.MovePosition(rigid.position + nextVector);        //위치 이동
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.IsLive) return;


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

    private void TurnCharacter()    //캐릭터 좌우 돌기
    {
        if (InputVector.x < 0)
        {
            sprite.flipX = true;
        }
        else if (InputVector.x > 0)
        {
            sprite.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameManager.instance.IsLive) return;

        
            StartCoroutine(HPHit());
        

        if (GameManager.instance.PlayerHP < 0)
        {
            for (int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            sprite.flipX = false;
            anim.SetTrigger("Dead");
            col.enabled = false;
            audio.clip = deadclip;
            audio.Play();
            MoveSpeed = 0;
            GameManager.instance.GameOver();
        }
    }

    private IEnumerator HPHit()
    {
        yield return wfu;
        GameManager.instance.PlayerHP -= GameManager.instance.Enemy.spawndata[GameManager.instance.Enemy.level].damage;
    }


}
