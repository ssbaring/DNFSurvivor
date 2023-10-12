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



    private Image icon;
    private Text textLevel;
    private Text textName;
    private Text textDesc;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;
        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.itemName;
    }

    private void OnEnable()
    {
        textLevel.text = $"Lv.{level}";
        switch (data.itemType)
        {
            case ItemData.ItemType.Asura:
                if (level == 0)
                {
                    textDesc.text = string.Format(data.itemDescription);
                }
                else
                {
                    textDesc.text = string.Format("데미지 {0} 증가. 불꽃 {1}개 추가.", data.damage[level], data.count[level]);
                }
                break;
            case ItemData.ItemType.WeaponMaster:
                if (level == 0)
                {
                    textDesc.text = string.Format(data.itemDescription);
                }
                else if (level == 3)
                {
                    textDesc.text = string.Format("데미지 {0} 증가. \n발도 시전시 추가타를 시전합니다.", data.damage[level]);
                }
                else
                {
                    textDesc.text = string.Format("데미지 {0} 증가.", data.damage[level]);
                }
                break;
            case ItemData.ItemType.Berserker:
                if (level == 0)
                {
                    textDesc.text = string.Format(data.itemDescription);
                }
                else
                {
                    textDesc.text = string.Format("데미지 {0} 증가.", data.damage[level]);
                }
                break;
            case ItemData.ItemType.Soulbringer:
                if (level == 0)
                {
                    textDesc.text = string.Format(data.itemDescription);
                }
                else
                {
                    textDesc.text = string.Format("데미지 {0} 증가.", data.damage[level]);
                }
                break;
            case ItemData.ItemType.Cooltime:
            case ItemData.ItemType.Range:
            case ItemData.ItemType.Damage:
                textDesc.text = string.Format(data.itemDescription, data.damage[level] * 100);
                break;
        }
    }

    public void OnClick()
    {

        switch (data.itemType)
        {
            case ItemData.ItemType.Asura:
            case ItemData.ItemType.WeaponMaster:
            case ItemData.ItemType.Berserker:
            case ItemData.ItemType.Soulbringer:
                if (level == 0)
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
                if (level == 0)
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
        Debug.Log(level);
        if (level == data.damage.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }

    
}
