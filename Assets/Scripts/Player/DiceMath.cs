using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class DiceMath : MonoBehaviour
    {
        private const float smallComparer = 0.00000001f;
        
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

        private int NumberOfZeros()
        {
            int c = 0;
            foreach (float x in _faceChances)
            {
                if (x <= smallComparer)
                {
                    c++;
                }
            }

            return c++;
        }
        
        public void AddPercentage(int face, float percentageIncrease)
        {
            int index = face - 1;
            float actualPercentage = _faceChances[index] + percentageIncrease > 1
                ? 1 - _faceChances[index]
                : percentageIncrease;

            _faceChances[index] = Mathf.Clamp(_faceChances[index] + actualPercentage, 0f, 1f);
            float toDisperse = _faceChances.Sum() - 1;
            while (Mathf.Abs(_faceChances.Sum() - 1) > smallComparer && Mathf.Abs(toDisperse - 0) > smallComparer)
            {
                int numberOfNonZeros = 5 - NumberOfZeros();

                for (int i = 0; i < _faceChances.Length; i++)
                {
                    if (i == index || Mathf.Abs(_faceChances[i]) <= smallComparer) continue;
                    float toDecrease = toDisperse / numberOfNonZeros;
                    if (_faceChances[i] < toDecrease)
                    {
                        toDisperse -= _faceChances[i];
                        _faceChances[i] = 0;
                    }
                    else
                    {
                        _faceChances[i] = Mathf.Clamp(_faceChances[i] - toDecrease, 0f, 1f);
                        toDisperse -= toDecrease;
                    }
                }
            }

            Debug.Log("Sum of face chances " + _faceChances.Sum());
        }

        public void SubtractPercentage(int face, float percentageDecrease)
        {
            int index = face - 1;
            float actualPercentage = _faceChances[index] - percentageDecrease < 0
                ? _faceChances[index]
                : percentageDecrease;
            int numberOfNonZeros = 5 - NumberOfZeros();

            for (int i = 0; i < _faceChances.Length; i++)
            {
                if (i == index)
                {
                    _faceChances[i] = Mathf.Clamp(_faceChances[i] - actualPercentage, 0f, 1f);
                }
                else
                {
                    _faceChances[i] = Mathf.Clamp(_faceChances[i] + actualPercentage / numberOfNonZeros, 0f, 1f);
                }
            }

            Debug.Log("Sum of face chances " + _faceChances.Sum());
        }

        public float[] GetSpeculativeAdditivePercentages(int face, float percentageIncrease)
        {
            float[] copy = new float[6];
            int index = face - 1;
            for (int i = 0; i < _faceChances.Length; i++)
            {
                copy[i] = _faceChances[i];
            }

            float actualPercentage = copy[index] + percentageIncrease > 1
                ? 1 - copy[index]
                : percentageIncrease;

            copy[index] = Mathf.Clamp(copy[index] + actualPercentage, 0f, 1f);
            float toDisperse = copy.Sum() - 1;
            while (Mathf.Abs(copy.Sum() - 1) > smallComparer && Mathf.Abs(toDisperse - 0) > smallComparer)
            {
                int numberOfNonZeros = 5 - NumberOfZeros();
                for (int i = 0; i < copy.Length; i++)
                {
                    if (i == index || Mathf.Abs(copy[i]) <= smallComparer) continue;
                    float toDecrease = toDisperse / numberOfNonZeros;
                    if (copy[i] < toDecrease)
                    {
                        toDisperse -= copy[i];
                        copy[i] = 0;
                    }
                    else
                    {
                        copy[i] = Mathf.Clamp(copy[i] - toDecrease, 0f, 1f);
                        toDisperse -= toDecrease;
                    }
                }
            }
            
            Debug.Log("Sum of copy " + copy.Sum());
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
      
            Debug.Log("Sum of copy " + copy.Sum());
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