using System;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DefaultNamespace
{
    
    public class SkillTreeItem : MonoBehaviour, IPointerEnterHandler
    {
        public int cost;
        public Toggle toggle;
        public string description;
        public bool unlocked;
        public List<SkillTreeItem> dependencies;

        [Header("StuffThisDoes")] public float speedBuffMultiplier = 1;
        public float speedBuffTimeMultiplier = 1;
        public float fireCooldownDecreaseMultiplier = 1;
        public float bulletDamageMultiplier = 1;
        public int multiBulletAdditive = 0;
        public float knockbackMultiplier = 1;
        public float numberPiercingTargetsMultiplier = 1;

        private void Start()
        {
            if (dependenciesUnlocked())
            {
                toggle.onValueChanged.AddListener(delegate { SetPickedItem(); });
            }
        }

        private bool canBeBought()
        {
            if (SkillTree.Instance.stillAvailablePoints < cost) return false;
            foreach (SkillTreeItem dependency in dependencies)
            {
                if (dependency.unlocked == false && !SkillTree.Instance.checkout.Contains(dependency)) return false;
            }

            return true;
        }
        
        private bool dependenciesUnlocked()
        {
            foreach (SkillTreeItem dependency in dependencies)
            {
                if (dependency.unlocked == false && !SkillTree.Instance.checkout.Contains(dependency)) return false;
            }

            return true;
        }

        private void SetPickedItem()
        {
            if (toggle.isOn)
            {
                SkillTree.Instance.checkout.Add(this);
                SkillTree.Instance.currentCost += cost;
                SkillTree.Instance.stillAvailablePoints -= cost;
            }
            else
            {
                SkillTree.Instance.checkout.Remove(this);
                SkillTree.Instance.currentCost -= cost;
                SkillTree.Instance.stillAvailablePoints += cost;
            }
        }

        public void OnPointerEnter(PointerEventData pointer)
        {
            SkillTree.Instance.textTooltip.text = description;
        }

        public void Unlock()
        {
            if (!canBeBought()) return;
            SkillTree.Instance.currentPoints -= cost;
            unlocked = true;
            toggle.onValueChanged.RemoveAllListeners();
            toggle.interactable = false;

            PlayerEntity.Instance.movement.speedBuffMultiplier += speedBuffMultiplier;
            PlayerEntity.Instance.buffs.speedBuffTimePerFace *= speedBuffTimeMultiplier;
            PlayerEntity.Instance.shooting.cooldown += fireCooldownDecreaseMultiplier;
            //TODO se calhar passar o dano da bala da bala para o player para poder dar set aqui
            PlayerEntity.Instance.shooting.baseBullets += multiBulletAdditive;
            //TODO se calhar passar o knockback da bala da bala para o player para poder dar set aqui
            //TODO se calhar passar o piercing da bala da bala para o player para poder dar set aqui

        }
    }
    
}