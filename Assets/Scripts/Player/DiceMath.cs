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
            
            if (roll > sum && (sum + roll) <= _faceChances[1])
            {
                return 2;
            }

            sum += _faceChances[1];

            
            if (roll > sum && (sum + roll) <= _faceChances[2])
            {
                return 3;
            }
            
            sum += _faceChances[2];

            if (roll > sum && (sum + roll) <= _faceChances[3])
            {
                return 4;
            }
            
            sum += _faceChances[3];

            if (roll > sum && (sum + roll) <= _faceChances[4])
            {
                return 5;
            }

            sum += _faceChances[4];
            
            if (roll > sum && (sum + roll) <= _faceChances[5])
            {
                return 6;
            }

            return -1;
        }

        public void ChangePercentage(int face, float percentageIncrease)
        {
            int index = face - 1;
            for (int i = 0; i < _faceChances.Length; i++)
            {
                if (i == index)
                {
                    _faceChances[i] = Mathf.Clamp(_faceChances[i]+ percentageIncrease, 0f, 1f);
                }
                else
                {
                    _faceChances[i] = Mathf.Clamp(_faceChances[i] - percentageIncrease / 5f, 0f, 1f);
                }
            }
        }

        public float GetFacePercentage(int face)
        {
            return _faceChances[face - 1] * 100;
        }
    }
}