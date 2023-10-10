using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoint;
    public SpawnData[] spawndata;



    private int level;
    private float timer;
    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();      //자기자신도 포함
    }
    private void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.FloorToInt(GameManager.instance.gameTime / 60f);          //게임 시간에 따른 레벨 증가(소수점 버림)
        if (timer > spawndata[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.GetPool(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;     //1부터 시작하는 이유는 GetComponentsInChildren이 Spawner도 포함됨
        enemy.GetComponent<EnemyControl>().Init(spawndata[level]);
    }
}

[System.Serializable]
public class SpawnData      //몹 데이터
{
    public float spawnTime;

    public int spriteType;      //타입에 따른 몬스터 종류
    public int HP;
    public float speed;
}