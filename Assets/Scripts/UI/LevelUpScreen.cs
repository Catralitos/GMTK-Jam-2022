using System.Collections.Generic;
using System.Linq;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

        [HideInInspector] public int statsToLevelUp;

        private bool _add;
        private int _currentFace;

        private List<Toggle> addToggles = new List<Toggle>();
        private List<Toggle> minusToggles = new List<Toggle>();

        private float[] _currentPercentages;

        private PlayerEntity _player;

        private void Awake()
        {
            _player = PlayerEntity.Instance;

            addToggle1.onValueChanged.AddListener(delegate { AddToPercentage(1); });
            addToggle2.onValueChanged.AddListener(delegate { AddToPercentage(2); });
            addToggle3.onValueChanged.AddListener(delegate { AddToPercentage(3); });
            addToggle4.onValueChanged.AddListener(delegate { AddToPercentage(4); });
            addToggle5.onValueChanged.AddListener(delegate { AddToPercentage(5); });
            addToggle6.onValueChanged.AddListener(delegate { AddToPercentage(6); });

            addToggles.Add(addToggle1);
            addToggles.Add(addToggle2);
            addToggles.Add(addToggle3);
            addToggles.Add(addToggle4);
            addToggles.Add(addToggle5);
            addToggles.Add(addToggle6);

            minusToggle1.onValueChanged.AddListener(delegate { SubtractFromPercentage(1); });
            minusToggle2.onValueChanged.AddListener(delegate { SubtractFromPercentage(2); });
            minusToggle3.onValueChanged.AddListener(delegate { SubtractFromPercentage(3); });
            minusToggle4.onValueChanged.AddListener(delegate { SubtractFromPercentage(4); });
            minusToggle5.onValueChanged.AddListener(delegate { SubtractFromPercentage(5); });
            minusToggle6.onValueChanged.AddListener(delegate { SubtractFromPercentage(6); });

            minusToggles.Add(minusToggle1);
            minusToggles.Add(minusToggle2);
            minusToggles.Add(minusToggle3);
            minusToggles.Add(minusToggle4);
            minusToggles.Add(minusToggle5);
            minusToggles.Add(minusToggle6);

            confirmButton.onClick.AddListener(ConfirmButton);
        }

        private void OnEnable()
        {
            _player = PlayerEntity.Instance;
            levelUpText.text = "Please choose a side to change your odds by " +
                               (_player.progression.nextPercentageIncrease * 100);
            _currentPercentages = _player.dice.GetAllPercentages();

            int[] indexes = { 0, 1, 2, 3, 4, 5 };
            List<int> shuffled = indexes.ToList();
            for (int i = 0; i < shuffled.Count; i++)
            {
                int temp = shuffled[i];
                int randomIndex = Random.Range(i, shuffled.Count);
                shuffled[i] = shuffled[randomIndex];
                shuffled[randomIndex] = temp;
            }

            List<int> sublist = shuffled.GetRange(0, statsToLevelUp);
            for (int i = 0; i < addToggles.Count; i++)
            {
                if (!sublist.Contains(i))
                {
                    addToggles[i].gameObject.SetActive(false);
                    minusToggles[i].gameObject.SetActive(false);
                }
                else
                {
                    addToggles[i].gameObject.SetActive(true);
                    minusToggles[i].gameObject.SetActive(true);
                }
            }

            for (int i = 0; i < addToggles.Count; i++)
            {
                if (_currentPercentages[i] + _player.progression.nextPercentageIncrease > 1)
                {
                    addToggles[i].gameObject.SetActive(false);
                }

                if (_currentPercentages[i] - _player.progression.nextPercentageIncrease < 0)
                {
                    minusToggles[i].gameObject.SetActive(false);
                }
            }

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
                _player.dice.GetSpeculativeSubtractivePercentages(face,
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

            for (int i = 0; i < 6; i++)
            {
                addToggles[i].isOn = false;
                minusToggles[i].isOn = false;
            }

            Time.timeScale = 1;
            _player.progression.SetNewPercentageIncrease();
            gameObject.SetActive(false);
        }

        private bool IsAToggleOn()
        {
            return addToggle1.isOn || addToggle2.isOn || addToggle3.isOn || addToggle4.isOn || addToggle5.isOn ||
                   addToggle6.isOn
                   || minusToggle1.isOn || minusToggle2.isOn || minusToggle3.isOn || minusToggle4.isOn ||
                   minusToggle5.isOn || minusToggle6.isOn;
        }
    }
}