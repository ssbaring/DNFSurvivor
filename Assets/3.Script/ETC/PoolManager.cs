using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;

    private List<GameObject>[] pool;

    private void Awake()
    {
        pool = new List<GameObject>[prefabs.Length];   //배열 초기화

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = new List<GameObject>();       //배열의 리스트 초기화
        }

    }

    public GameObject GetPool(int index)
    {
        GameObject select = null;

        //선택한 pool 비활성화 되고있는 게임오브젝트 접근
        foreach(GameObject item in pool[index])
        {
            if(!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        //못 찾을 시
        if(!select)
        {
            select = Instantiate(prefabs[index], transform);        //새롭게 생성하고 select변수에 할당
            pool[index].Add(select);
        }

        return select;
    }
}
