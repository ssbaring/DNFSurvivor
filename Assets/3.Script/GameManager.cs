using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController player;
    public PoolManager pool;
    public Spawner Enemy;


    [Header("GameTime")]
    public float gameTime;
    public float maxgameTime = 5 * 10f;

    [Header("PlayerInfo")]
    public int PlayerHP = 100;
    public int PlayerMaxHP = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] NextExp = new int[35];


    private void Awake()
    {
        instance = this;
        SetExp();
        PlayerHP = PlayerMaxHP;
        Enemy.GetComponent<Spawner>();
    }


    private void Update()
    {
        gameTime += Time.deltaTime;
        if (gameTime > maxgameTime)
        {
            gameTime = maxgameTime;
        }
        DevMode();
    }



    private void SetExp()
    {
        NextExp[0] = 10;
        for (int level = 0; level < NextExp.Length - 1; level++)
        {
            NextExp[level + 1] = NextExp[level] + 10;
        }
    }

    public void GetExp()
    {
        exp++;

        if (exp == NextExp[level])
        {
            level++;
            exp = 0;
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
    }

    //개발자 모드
    private void DevMode()
    {


    }

}
