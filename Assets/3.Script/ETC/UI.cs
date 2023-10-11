using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
    public enum InfoType { Exp, HP, Level, Kill, Time }
    public InfoType type;




    [SerializeField] private Text text;
    [SerializeField] private Slider ExpBar;

    private void Awake()
    {
        text = GetComponent<Text>();
        ExpBar = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                Exp();
                break;
            case InfoType.HP:
                HP();
                break;
            case InfoType.Kill:
                Kill();
                break;
            case InfoType.Level:
                Level();
                break;
            case InfoType.Time:
                GameTime();
                break;
        }
    }

    private void Exp()
    {
        float currentExp = GameManager.instance.exp;
        float nextExp = GameManager.instance.NextExp[GameManager.instance.level];
        ExpBar.value = currentExp / nextExp;
    }

    private void Level()
    {
        text.text = $"Level : {GameManager.instance.level + 1}";
    }

    private void GameTime()
    {
        int sec = Mathf.FloorToInt(GameManager.instance.gameTime % 60);
        int min = Mathf.FloorToInt(GameManager.instance.gameTime / 60);
        text.text = string.Format("{0:D2} : {1:D2}", min, sec);
    }

    private void HP()
    {
        float currentHP = GameManager.instance.PlayerHP;
        float MaxHP = GameManager.instance.PlayerMaxHP;
        ExpBar.value = currentHP / MaxHP;
    }

    private void Kill()
    {
        text.text = string.Format("{0:F0}", GameManager.instance.kill);
    }
}
