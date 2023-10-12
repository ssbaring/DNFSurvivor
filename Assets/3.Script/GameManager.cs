using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController player;
    public PoolManager pool;
    public Spawner Enemy;
    public Collider2D col;

    [Header("GameTime")]
    public float gameTime;
    public float maxgameTime = 5 * 60f;

    [Header("PlayerInfo")]
    public int PlayerHP = 100;
    public int PlayerMaxHP = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] NextExp = new int[35];

    [Header("GameObject")]
    public LevelUP levelupUI;
    public static bool IsPause = false;



    private void Awake()
    {
        instance = this;
        SetExp();
        PlayerHP = PlayerMaxHP;
        Enemy.GetComponent<Spawner>();
        col.GetComponent<Collider2D>();
    }

    private void Start()
    {
        levelupUI.Select(0);
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
        if (gameTime > maxgameTime)
        {
            gameTime = maxgameTime;
        }

        /*if(IsPause)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }*/
        DevMode();
    }



    private void SetExp()
    {
        NextExp[0] = 10;
        for (int level = 0; level < NextExp.Length - 1; level++)
        {
            NextExp[level + 1] = NextExp[level] + 2;
        }
    }

    public void GetExp()
    {
        exp++;

        if (exp == NextExp[Mathf.Min(level,NextExp.Length - 1)])
        {
            level++;
            exp = 0;
            levelupUI.Show();
        }
    }

    public void HPDown()
    {
        PlayerHP -= Enemy.spawndata[Enemy.level].damage;

        if (PlayerHP <= 0)
        {
            Debug.Log("Die");
            //PlayerHP = 0;
            StartCoroutine(DeadAnimation());
        }
    }

    private IEnumerator DeadAnimation()
    {
        while (player.transform.position.y > -0.68f)
        {
            if (player.transform.localScale.y > 0.05)
            {
                player.transform.localScale -= new Vector3(0, Time.deltaTime, 0);
            }
            player.transform.position -= new Vector3(0, Time.deltaTime, 0);
            yield return null;
        }
        col.enabled = false;

    }


    public void Stop()
    {
        IsPause = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        IsPause = false;
        Time.timeScale = 1f;
    }



    //개발자 모드
    private void DevMode()
    {


    }

}
