using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUP : MonoBehaviour
{
    private RectTransform rect;
    private Item[] items;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
        RandomSelect();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }

    private void RandomSelect()
    {
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        int[] rand = new int[3];
        while (true)
        {
            rand[0] = Random.Range(0, items.Length);
            rand[1] = Random.Range(0, items.Length);
            rand[2] = Random.Range(0, items.Length);

            if (rand[0] != rand[1] && rand[1] != rand[2] && rand[0] != rand[2])
            {
                break;
            }
        }
        for (int i = 0; i < rand.Length; i++)
        {
            Item randItem = items[rand[i]];
            if (randItem.level == randItem.data.damage.Length)
            {
                
            }
            else
            {
                randItem.gameObject.SetActive(true);
            }
        }
    }
}
