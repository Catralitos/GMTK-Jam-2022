using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelUpScreen : MonoBehaviour
    {
        public TextMeshProUGUI levelUpText;
        public List<TextMeshProUGUI> percentageTexts;
        public List<Image> percentageBars;
        public List<Toggle> addButtons;
        public List<Toggle> minusButtons;
        public Button confirmButton;

        private bool _add;
        private int _currentFace;
        
        private float[] _currentPercentages;

        private PlayerEntity _player;
        private PlayerUI _playerUI;

        private void Start()
        {
            _player = PlayerEntity.Instance;
            _playerUI = GetComponentInParent<PlayerUI>();
            for (int i = 0; i < addButtons.Count; i++)
            {
                addButtons[i].onValueChanged.AddListener(delegate{AddToPercentage(i + 1);});
            }
            for (int i = 0; i < minusButtons.Count; i++)
            {
                minusButtons[i].onValueChanged.AddListener(delegate{SubtractFromPercentage(i + 1);});
            }
            confirmButton.onClick.AddListener(ConfirmButton);
        }

        private void OnEnable()
        {
            Time.timeScale = 0;
            levelUpText.text = "Please choose a side to change your odds by " +
                               (_player.progression.nextPercentageIncrease * 100);
            _currentPercentages = _player.dice.GetAllPercentages();
            UpdateValues();
        }

        private void Update()
        {
            confirmButton.interactable = IsAToggleOn();
            if (!IsAToggleOn())
            {
                ResetPercentages();
            }
           
        }

        private void UpdateValues()
        {
            for (int i = 0; i < percentageTexts.Count; i++)
            {
                percentageTexts[i].text = (_currentPercentages[i] * 100) + "%";
            }

            for (int i = 0; i < percentageBars.Count; i++)
            {
                percentageBars[i].fillAmount = _currentPercentages[i] / 1.0f;
            }

            for (int i = 0; i < percentageBars.Count; i++)
            {
                percentageBars[i].fillAmount = _currentPercentages[i] / 1.0f;
            }
        }

        private void AddToPercentage(int face)
        {
            _currentPercentages =
                _player.dice.GetSpeculativeAdditivePercentages(face,
                    _player.progression.nextPercentageIncrease);
            _currentFace = face;
            _add = true;
            UpdateValues();
        }
        
        private void SubtractFromPercentage(int face)
        {
            _currentPercentages =
                _player.dice.GetSpeculativeSubtracticePercentages(face,
                    _player.progression.nextPercentageIncrease);
            _currentFace = face;
            _add = false;
            UpdateValues();
        }

        private void ResetPercentages()
        {
            _currentPercentages = _player.dice.GetAllPercentages();
            _currentFace = -1;
            UpdateValues();
        }

        private void ConfirmButton()
        {
            if (!IsAToggleOn()) return;
            if (_add)
            {
                _player.dice.AddPercentage(_currentFace, _player.progression.nextPercentageIncrease);
            }
            else
            {
                _player.dice.SubtractPercentage(_currentFace, _player.progression.nextPercentageIncrease);
            }
            Time.timeScale = 0;
            _player.progression.SetNewPercentageIncrease();
            gameObject.SetActive(false);
        }

        private bool IsAToggleOn()
        {
            foreach (Toggle x in addButtons)
            {
                if (x.isOn) return true;
            }
            foreach (Toggle x in minusButtons)
            {
                if (x.isOn) return true;
            }
            return false;
        }
    }
}