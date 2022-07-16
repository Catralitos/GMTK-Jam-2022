using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class DiceMath : MonoBehaviour
    {
        private float[] _faceChances;

        private void Start()
        {
            _faceChances = new[] { 1 / 6f, 1 / 6f, 1 / 6f, 1 / 6f, 1 / 6f, 1 / 6f };
        }

        public int RollDice()
        {
            float roll = Random.Range(0f, 1f);
            float sum = 0;
            if (roll <= _faceChances[0])
            {
                return 1;
            }

            sum += _faceChances[0];
            

            if (roll > sum && roll <= (_faceChances[1] + sum))
            {
                return 2;
            }

            sum += _faceChances[1];
            


            if (roll > sum && roll <= (_faceChances[2] + sum))
            {
                return 3;
            }

            sum += _faceChances[2];
            

            if (roll > sum && roll <= (_faceChances[3] + sum))
            {
                return 4;
            }

            sum += _faceChances[3];
            

            if (roll > sum && roll <= (_faceChances[4] + sum))
            {
                return 5;
            }

            sum += _faceChances[4];
            

            if (roll > sum && roll <= (_faceChances[5] + sum))
            {
                return 6;
            }

            return -1;
        }

        public void AddPercentage(int face, float percentageIncrease)
        {
            int index = face - 1;
            float actualPercentage = _faceChances[index] + percentageIncrease > 1
                ? 1 - _faceChances[index]
                : percentageIncrease;
            for (int i = 0; i < _faceChances.Length; i++)
            {
                if (i == index)
                {
                    _faceChances[i] = Mathf.Clamp(_faceChances[i] + actualPercentage, 0f, 1f);
                }
                else
                {
                    _faceChances[i] = Mathf.Clamp(_faceChances[i] - actualPercentage / 5f, 0f, 1f);
                }
            }
        }
        
        public void SubtractPercentage(int face, float percentageDecrease)
        {
            int index = face - 1;
            float actualPercentage = _faceChances[index] - percentageDecrease < 0
                ? _faceChances[index]
                : percentageDecrease;
            for (int i = 0; i < _faceChances.Length; i++)
            {
                if (i == index)
                {
                    _faceChances[i] = Mathf.Clamp(_faceChances[i] - actualPercentage, 0f, 1f);
                }
                else
                {
                    _faceChances[i] = Mathf.Clamp(_faceChances[i] + actualPercentage / 5f, 0f, 1f);
                }
            }
        }

        public float[] GetSpeculativeAdditivePercentages(int face, float percentageIncrease)
        {
            float[] copy = new float[6];
            int index = face - 1;
            float actualPercentage = _faceChances[index] + percentageIncrease > 1
                ? 1 - _faceChances[index]
                : percentageIncrease;
            for (int i = 0; i < _faceChances.Length; i++)
            {
                if (i == index)
                {
                    copy[i] = Mathf.Clamp(_faceChances[i] + actualPercentage, 0f, 1f);
                }
                else
                {
                    copy[i] = Mathf.Clamp(_faceChances[i] - actualPercentage / 5f, 0f, 1f);
                }
            }

            return copy;
        }
        
        public float[] GetSpeculativeSubtractivePercentages(int face, float percentageDecrease)
        {
            float[] copy = new float[6];
            int index = face - 1;
            float actualPercentage = _faceChances[index] - percentageDecrease < 0
                ? _faceChances[index]
                : percentageDecrease;
            for (int i = 0; i < _faceChances.Length; i++)
            {
                if (i == index)
                {
                    copy[i] = Mathf.Clamp(_faceChances[i] - actualPercentage, 0f, 1f);
                }
                else
                {
                    copy[i] = Mathf.Clamp(_faceChances[i] + actualPercentage / 5f, 0f, 1f);
                }
            }

            return copy;
        }

        public float[] GetAllPercentages()
        {
            float[] copy = new float[6];
            for (int i = 0; i < _faceChances.Length; i++)
            {
                copy[i] = _faceChances[i];
            }

            return copy;
        }

        public float GetFacePercentage(int face)
        {
            return _faceChances[face - 1] * 100;
        }
    }
}