using UnityEngine;

namespace Audio
{
    public class MusicManager : MonoBehaviour
    {
        private AudioManager _audioManager;

        private void Start()
        {
            _audioManager = AudioManager.Instance;
        }
        /*
        void Update()
        {
            if (worldType != WorldManager.currentWorld)
            {
                worldType = WorldManager.currentWorld;
                SwitchMusic();
            }
        }

        private void SwitchMusic()
        {
            switch (worldType)
            {
                case WorldType.Light:
                    _audioManager.SetMusic("LightIntro", "LightLoop");
                    break;
                case WorldType.Dark:
                    _audioManager.SetMusic("DarkIntro", "DarkLoop");
                    break;
                default:
                    Debug.LogWarning("No music assigned to world type " + worldType);
                    break;
            }
        }*/
    }
}
