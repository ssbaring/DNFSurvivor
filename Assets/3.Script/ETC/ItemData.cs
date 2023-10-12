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
    //1���� �⺻ ���ݷ°� ������Ʈ ����
    public float baseDamage;
    public int baseCount;
    //������ ���ݷ°� ������Ʈ ����
    public float[] damage;
    public int[] count;

    [Header("Skill")]
    //����ü
    public GameObject projectile;
}
