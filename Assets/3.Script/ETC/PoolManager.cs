using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;

    private List<GameObject>[] pool;

    private void Awake()
    {
        pool = new List<GameObject>[prefabs.Length];   //�迭 �ʱ�ȭ

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = new List<GameObject>();       //�迭�� ����Ʈ �ʱ�ȭ
        }

    }

    public GameObject GetPool(int index)
    {
        GameObject select = null;

        //������ pool ��Ȱ��ȭ �ǰ��ִ� ���ӿ�����Ʈ ����
        foreach(GameObject item in pool[index])
        {
            if(!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        //�� ã�� ��
        if(!select)
        {
            select = Instantiate(prefabs[index], transform);        //���Ӱ� �����ϰ� select������ �Ҵ�
            pool[index].Add(select);
        }

        return select;
    }
}
