using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptale Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { WeaponMaster, Asura, Soulbringer, Berserker, Cooltime, Range, Damage }

    [Header("Main Info")]
    public ItemType itemType;
    public int itemID;
    public string itemName;

    [TextArea]
    public string itemDescription;
    public Sprite itemIcon;


    [Header("Level Data")]
    //1레벨 기본 공격력과 오브젝트 개수
    public float baseDamage;
    public int baseCount;
    //레벨당 공격력과 오브젝트 개수
    public float[] damage;
    public int[] count;

    [Header("Skill")]
    //투사체
    public GameObject projectile;
}
