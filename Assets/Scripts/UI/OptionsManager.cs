using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class OptionsManager : MonoBehaviour
{
    Resolution[] resolutions;

    public TMPro.TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    private void Start() {
        bool value = GameManager.Instance.fullscreen;
        fullscreenToggle.isOn = value;

        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for(int i=0; i <resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetRes(int index) {
        GameManager.Instance.SetResolution(resolutions[index]);
    }
}
