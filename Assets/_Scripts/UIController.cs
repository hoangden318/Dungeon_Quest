using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;

    public Dropdown resolutionDropdown, graphicDropdown;
    private Resolution[] resolutions;

    private List<string> resolutionOption = new List<string>();
    private List<string> graphicOption = new List<string>();

    private int defaultWidth = 800;
    private int defaultHeight = 600;
    private int defaultGraphic = 2;
    private void Awake()
    {
        Screen.SetResolution(defaultWidth, defaultHeight, Screen.fullScreen);
        QualitySettings.SetQualityLevel(defaultGraphic);
    }
    private void Start()
    {
        IntializeGraphic();
        IntializeResolution();
    }
    public void IntializeGraphic()
    {
        //Dropdown Graphics
        graphicDropdown.ClearOptions();

        string[] graphicNames = QualitySettings.names;
        foreach (string graphicName in graphicNames)
        {
            graphicOption.Add(graphicName);
        }
        graphicDropdown.AddOptions(graphicOption);
        graphicDropdown.value = defaultGraphic;
        graphicDropdown.RefreshShownValue();
    }
    public void IntializeResolution()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();


        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            resolutionOption.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width
                && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(resolutionOption);

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == defaultWidth && resolutions[i].height == defaultHeight)
            {
                currentResolutionIndex = i;
                break;
            }
        }
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void MusicVolume()
    {
        SoundManager.Instance.MusicVolume(musicSlider.value);
    }

    public void SfxVolume()
    {
        SoundManager.Instance.SfxVolume(sfxSlider.value);
    }

    // Graphics
    public void SetGraphics(int graphicIndex)
    {
        QualitySettings.SetQualityLevel(graphicIndex);
    }
    // Full Screen
    public void SetFullScreen(bool isFullScreen)
    {
        if (isFullScreen) return;
        
        Screen.fullScreen = isFullScreen;
    }
    // Resolutions
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    // Quit Game
    public void OnApplicationQuit()
    {
        Application.Quit();

    }
}
