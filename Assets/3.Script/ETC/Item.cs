using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Skill skill;
    public Potion potion;

    Image icon;
    Text textLevel;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;
        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    private void LateUpdate()
    {
        textLevel.text = $"Lv.{level + 1}";
    }

    public void OnClick()
    {
        switch(data.itemType)
        {
            case ItemData.ItemType.Asura:
            case ItemData.ItemType.WeaponMaster:
            case ItemData.ItemType.Berserker:
            case ItemData.ItemType.Soulbringer:
                if(level == 0)
                {
                    GameObject newSkill = new GameObject();
                    skill = newSkill.AddComponent<Skill>();
                    skill.Init(data);
                }
                else
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.damage[level];
                    nextCount += data.count[level];

                    skill.LevelUP(nextDamage, nextCount);
                }
                break;
            case ItemData.ItemType.Cooltime:
            case ItemData.ItemType.Damage:
            case ItemData.ItemType.Range:
                if(level == 0)
                {
                    GameObject newPotion = new GameObject();
                    potion = newPotion.AddComponent<Potion>();
                    potion.Init(data);
                }
                else
                {
                    float nextPotion = data.damage[level];
                    potion.LevelUp(nextPotion);
                }
                break;
                
        }
        level++;

        if(level == data.damage.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
