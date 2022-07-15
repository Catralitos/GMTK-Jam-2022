using UnityEngine;

namespace Audio
{
    public class MainMenuMusicManager : MonoBehaviour
    {
        private AudioManager _audioManager;

        public string intro = "MenuIntro";
        public string loop = "MenuLoop";

        private void Start()
        {
            _audioManager = AudioManager.Instance;
            _audioManager.SetMusic(intro, loop);
        }
    }
}
