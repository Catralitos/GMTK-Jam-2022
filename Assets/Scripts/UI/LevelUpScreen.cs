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
        public Toggle addToggle1;
        public Toggle addToggle2;
        public Toggle addToggle3;
        public Toggle addToggle4;
        public Toggle addToggle5;
        public Toggle addToggle6;
        public Toggle minusToggle1;
        public Toggle minusToggle2;
        public Toggle minusToggle3;
        public Toggle minusToggle4;
        public Toggle minusToggle5;
        public Toggle minusToggle6;
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

            addToggle1.onValueChanged.AddListener(delegate { AddToPercentage(1); });
            addToggle2.onValueChanged.AddListener(delegate { AddToPercentage(2); });
            addToggle3.onValueChanged.AddListener(delegate { AddToPercentage(3); });
            addToggle4.onValueChanged.AddListener(delegate { AddToPercentage(4); });
            addToggle5.onValueChanged.AddListener(delegate { AddToPercentage(5); });
            addToggle6.onValueChanged.AddListener(delegate { AddToPercentage(6); });

            minusToggle1.onValueChanged.AddListener(delegate { SubtractFromPercentage(1); });
            minusToggle2.onValueChanged.AddListener(delegate { SubtractFromPercentage(2); });
            minusToggle3.onValueChanged.AddListener(delegate { SubtractFromPercentage(3); });
            minusToggle4.onValueChanged.AddListener(delegate { SubtractFromPercentage(4); });
            minusToggle5.onValueChanged.AddListener(delegate { SubtractFromPercentage(5); });
            minusToggle6.onValueChanged.AddListener(delegate { SubtractFromPercentage(6); });

            confirmButton.onClick.AddListener(ConfirmButton);
        }

        private void OnEnable()
        {
            _player = PlayerEntity.Instance;
            _playerUI = GetComponentInParent<PlayerUI>();
            levelUpText.text = "Please choose a side to change your odds by " +
                               (_player.progression.nextPercentageIncrease * 100);
            _currentPercentages = _player.dice.GetAllPercentages();
            Time.timeScale = 0;
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
                percentageTexts[i].text = Mathf.Round((_currentPercentages[i] * 100) * 100f) / 100f + "%";
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
            Debug.Log(face);
            _currentPercentages =
                _player.dice.GetSpeculativeAdditivePercentages(face,
                    _player.progression.nextPercentageIncrease);
            _currentFace = face;
            _add = true;
            Debug.Log(_currentFace);
            Debug.Log(_currentPercentages);
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

            Time.timeScale = 1;
            _player.progression.SetNewPercentageIncrease();
            gameObject.SetActive(false);
        }

        private bool IsAToggleOn()
        {
            return addToggle1.isOn || addToggle2.isOn || addToggle3.isOn || addToggle4.isOn || addToggle5.isOn || addToggle6.isOn
                || minusToggle1.isOn || minusToggle2.isOn || minusToggle3.isOn || minusToggle4.isOn || minusToggle5.isOn || minusToggle6.isOn;
        }
    }
}