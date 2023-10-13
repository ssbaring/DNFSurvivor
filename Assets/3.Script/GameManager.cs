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

    AudioSource audio;
    public AudioClip BGM;
    public GameObject BGMobj;

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
    public bool IsStart = false;


    [Header("DeveloperMode")]
    public GameObject Dev;



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

        Enemy.GetComponent<Spawner>();
        col.GetComponent<Collider2D>();
        audio = GetComponent<AudioSource>();
    }



    private void Update()
    {
        if (!IsLive) return;

        if (IsStart)
        {
            gameTime += Time.deltaTime;
        }


        DevMode();
    }

    public void GameStart(int id)
    {
        playerID = id;
        PlayerHP = PlayerMaxHP;
        IsStart = true;
        BGMobj = GameObject.Find("BGM");
        BGMobj.SetActive(false);
        audio.clip = BGM;
        audio.Play();

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
        //IsLive = false;
        Item.index_s = 0;
        Item.index_p = 0;

        yield return new WaitForSeconds(2f);
        Destroy(BGMobj);
        Stop();
    }
    public void GameClear()
    {
        if (gameTime > maxgameTime)
        {
            gameTime = maxgameTime;
            Stop();
            Debug.Log("클리어!");
        }
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
            NextExp[level + 1] = NextExp[level] + 5;
        }
    }

    public void GetExp()
    {
        exp++;

        if (exp == NextExp[Mathf.Min(level, NextExp.Length - 1)])
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
        if (Input.GetKeyDown(KeyCode.P) && Dev.activeSelf == false)
        {
            Dev.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.P) && Dev.activeSelf == true)
        {
            Dev.SetActive(false);
        }

    }

}
