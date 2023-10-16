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
    public Image[] UsingSkillIcon;
    public Image[] UsingPotionIcon;


    static public int index_s = 0;             //스킬의 배열
    static public int index_p = 0;             //포션의 배열
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
                    textDesc.text = string.Format("데미지 {0} 증가. \n불꽃 {1}개 추가.", data.damage[level], data.count[level]);
                }
                break;
            case ItemData.ItemType.WeaponMaster:
                if (level == 0)
                {
                    textDesc.text = string.Format(data.itemDescription);
                }
                else if (level == 3)
                {
                    textDesc.text = string.Format("데미지 {0} 증가. \n발도 공격 후 후속타가 시전됩니다.", data.damage[level]);
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
            case ItemData.ItemType.Heal:
                textDesc.text = string.Format(data.itemDescription, 30);
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
                    UsingSkillIcon = GameObject.Find("Skill Icon").GetComponentsInChildren<Image>();
                    skill = newSkill.AddComponent<Skill>();
                    skill.Init(data);
                    UsingSkillIcon[index_s].sprite = data.itemIcon;
                    UsingSkillIcon[index_s].color = new Color(1, 1, 1, 1);
                    index_s++;
                }
                else
                {
                    float nextDamage = 0;
                    int nextCount = 0;
                    nextDamage += data.damage[level];
                    nextCount += data.count[level];

                    skill.LevelUP(nextDamage, nextCount);
                }
                level++;
                break;
            case ItemData.ItemType.Cooltime:
            case ItemData.ItemType.Damage:
            case ItemData.ItemType.Range:
                if (level == 0)
                {
                    GameObject newPotion = new GameObject();
                    UsingPotionIcon = GameObject.Find("Potion Icon").GetComponentsInChildren<Image>();
                    potion = newPotion.AddComponent<Potion>();
                    potion.Init(data);
                    UsingPotionIcon[index_p].sprite = data.itemIcon;
                    UsingPotionIcon[index_p].color = new Color(1, 1, 1, 1);
                    index_p++;
                }
                else
                {
                    float nextPotion = data.damage[level];
                    potion.LevelUp(nextPotion);
                }
                level++;
                break;
            case ItemData.ItemType.Heal:
                if (GameManager.instance.PlayerMaxHP - GameManager.instance.PlayerHP > 30)
                {
                    GameManager.instance.PlayerHP += 30;
                }
                else
                {
                    GameManager.instance.PlayerHP = GameManager.instance.PlayerMaxHP;
                }
                break;

        }

        if (level == data.damage.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }


}
