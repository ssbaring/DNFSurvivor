using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSize : MonoBehaviour
{
    FullScreenMode screenMode;
    public Dropdown ResolutionDropdown;
    public Toggle Fullscreen;
    List<Resolution> resolutions = new List<Resolution>();
    int resolutionNum;

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        for(int i=0;i<Screen.resolutions.Length;i++)
        {
            if(Screen.resolutions[i].refreshRate == 60)
            {
                resolutions.Add(Screen.resolutions[i]);
            }
        }
        ResolutionDropdown.options.Clear();

        int optionNum = 0;

        foreach(Resolution item in resolutions)
        {
            Dropdown.OptionData options = new Dropdown.OptionData();
            options.text = $"{item.width} x {item.height}  {item.refreshRate}Hz";
            ResolutionDropdown.options.Add(options);

            if(item.width == Screen.width && item.height == Screen.height)
            {
                ResolutionDropdown.value = optionNum;
                optionNum++;
            }
        }
        ResolutionDropdown.RefreshShownValue();

        Fullscreen.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }


    public void DropDownOptionChange(int index)
    {
        resolutionNum = index;
    }

    public void FullScreenCheck(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void SetScreenSize()
    {
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, screenMode);
    }
}
