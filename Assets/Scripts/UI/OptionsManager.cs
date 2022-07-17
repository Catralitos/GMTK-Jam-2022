using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public Toggle fullscreenToggle;
    private void Start() {
        bool value = GameManager.Instance.fullscreen;
        fullscreenToggle.isOn = value;
    }
}
