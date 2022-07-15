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
            if (roll <= _faceChances[0])
            {
                return 1;
            }

            if (roll > _faceChances[0] && roll <= _faceChances[1])
            {
                return 2;
            }

            if (roll > _faceChances[1] && roll <= _faceChances[2])
            {
                return 3;
            }

            if (roll > _faceChances[2] && roll <= _faceChances[3])
            {
                return 4;
            }

            if (roll > _faceChances[3] && roll <= _faceChances[4])
            {
                return 5;
            }

            if (roll > _faceChances[4] && roll <= _faceChances[5])
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
                    _faceChances[i] += percentageIncrease;
                }
                else
                {
                    _faceChances[i] -= percentageIncrease / 5f;
                }
            }
        }

        public int GetFacePercentage(int face)
        {
            return Mathf.RoundToInt(_faceChances[face - 1] * 100);
        }
    }
}