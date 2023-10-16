using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;


    public void Init(ItemData data)
    {
        name = "Potion" + data.name;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;

        type = data.itemType;
        rate = data.damage[0];
        Apply();
    }

    public void LevelUp(float rate)
    {
        this.rate += rate;
        Apply();
    }

    private void Cooltime()
    {
        Skill[] skills = transform.parent.GetComponentsInChildren<Skill>();         //부모 오브젝트에서 skill 찾기

        foreach (Skill skill in skills)
        {
            switch (skill.id)
            {
                case 0:
                    skill.speed = 150 + (300 * rate);
                    break;
                case 1:
                    skill.speed = 3f * (1 - rate);
                    break;
                case 2:
                    skill.speed = 5f * (1 - rate);
                    break;
                case 3:
                    skill.speed = 1f * (1 - rate * 1.5f);
                    break;
            }
        }
    }

    private void Damage()
    {
        Skill[] skills = transform.parent.GetComponentsInChildren<Skill>();

        foreach (Skill skill in skills)
        {
            switch (skill.id)
            {
                case 0:
                    skill.damage *= (1 + rate);
                    break;
                case 1:
                    skill.damage *= (1 + rate);
                    break;
                case 2:
                    skill.damage *= (1 + rate);
                    break;
                case 3:
                    skill.damage *= (1 + rate);
                    break;
            }
        }
    }

    private void Range()
    {
        Skill[] skills = transform.parent.GetComponentsInChildren<Skill>();

        foreach (Skill skill in skills)
        {
            switch (skill.id)
            {
                default:
                    for (int i = 0; i < skill.transform.childCount; i++)
                    {
                        skill.transform.GetChild(i).localScale = Vector3.one + (Vector3.one * rate);
                    }
                    break;

            }
        }
    }

    private void Apply()
    {
        switch (type)
        {
            case ItemData.ItemType.Cooltime:
                Cooltime();
                break;
            case ItemData.ItemType.Damage:
                Damage();
                break;
            case ItemData.ItemType.Range:
                Range();
                break;
        }
    }
}
