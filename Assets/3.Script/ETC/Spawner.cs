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
        spawnPoint = GetComponentsInChildren<Transform>();      //�ڱ��ڽŵ� ����
    }
    private void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.FloorToInt(GameManager.instance.gameTime / 60f);          //���� �ð��� ���� ���� ����(�Ҽ��� ����)
        if (timer > spawndata[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.GetPool(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;     //1���� �����ϴ� ������ GetComponentsInChildren�� Spawner�� ���Ե�
        enemy.GetComponent<EnemyControl>().Init(spawndata[level]);
    }
}

[System.Serializable]
public class SpawnData      //�� ������
{
    public float spawnTime;

    public int spriteType;      //Ÿ�Կ� ���� ���� ����
    public int HP;
    public float speed;
}