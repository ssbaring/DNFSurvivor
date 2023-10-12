using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
    public int playerID;
    public int PlayerHP = 100;
    public int PlayerMaxHP = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] NextExp = new int[35];

    [Header("GameObject")]
    public LevelUP levelupUI;
    public static bool IsPause = false;
    public bool IsLive;



    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
        
        Enemy.GetComponent<Spawner>();
        col.GetComponent<Collider2D>();
    }



    private void Update()
    {
        if (!IsLive) return;

        gameTime += Time.deltaTime;
        if (gameTime > maxgameTime)
        {
            gameTime = maxgameTime;
        }

        DevMode();
    }

    public void GameStart(int id)
    {
        playerID = id;
        PlayerHP = PlayerMaxHP;

        player.gameObject.SetActive(true);
        levelupUI.Select(playerID);
        Resume();
        SetExp();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCo());
    }

    private IEnumerator GameOverCo()
    {
        IsLive = false;

        yield return new WaitForSeconds(2f);

        Stop();
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("Title");
    }


    private void SetExp()
    {
        NextExp[0] = 5;
        for (int level = 0; level < NextExp.Length - 1; level++)
        {
            NextExp[level + 1] = NextExp[level];
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

    public void Stop()
    {
        IsLive = false;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        IsLive = true;
        Time.timeScale = 1f;
    }



    //개발자 모드
    private void DevMode()
    {


    }

}
