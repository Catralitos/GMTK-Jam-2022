using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillTreeItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int cost;
    public Toggle toggle;
    public string description;
    public bool unlocked;
    public List<SkillTreeItem> dependencies;

    public PlayerSkills.Upgrades type;

    [Header("StuffThisDoes")] public float speedBuffMultiplier = 1;
    public float speedBuffTimeMultiplier = 1;
    public float fireCooldownDecreaseMultiplier = 1;
    public float bulletDamageMultiplier = 1;
    public int multiBulletAdditive = 0;
    public float knockbackMultiplier = 1;
    public float numberPiercingTargetsMultiplier = 1;

    public void EnableChild()
    {
        toggle.onValueChanged.RemoveAllListeners();
        if (DependenciesUnlocked() && !unlocked)
        {
            toggle.onValueChanged.AddListener(SetPickedItem);
        }

        if (unlocked)
        {
            ColorBlock cb = new ColorBlock();
            cb.disabledColor = Color.white;
            cb.highlightedColor = Color.white;
            cb.normalColor = Color.white;
            cb.pressedColor = Color.white;
            cb.selectedColor = Color.white;
            toggle.colors = cb;
        }
        else
        {
            if (CanBeBought())
            {
                ColorBlock cb = toggle.colors;
                cb.normalColor = new Color(0.75f, 0.75f, 0.75f);
                cb.selectedColor = Color.red;
                cb.highlightedColor = new Color(0.6f, 0.6f, 0.6f);
                cb.pressedColor = new Color(0.75f, 0.75f, 0.75f);
                toggle.colors = cb;
            }
            else
            {
                ColorBlock cb = toggle.colors;
                cb.normalColor = new Color(0.35f, 0.35f, 0.35f);
                cb.selectedColor = new Color(0.35f, 0.35f, 0.35f);
                cb.highlightedColor = new Color(0.35f, 0.35f, 0.35f);
                cb.pressedColor = new Color(0.35f, 0.35f, 0.35f);
                toggle.colors = cb;
            }
        }
    }


    private bool CanBeBought()
    {
        if (cost >= SkillTree.Instance.stillAvailablePoints) return false;
        foreach (SkillTreeItem dependency in dependencies)
        {
            if (dependency.unlocked == false && !SkillTree.Instance.checkout.Contains(dependency)) return false;
        }

        return true;
    }

    private bool DependenciesUnlocked()
    {
        foreach (SkillTreeItem dependency in dependencies)
        {
            if (dependency.unlocked == false && !SkillTree.Instance.checkout.Contains(dependency)) return false;
        }

        return true;
    }

    private void SetPickedItem(bool selected)
    {
        if (!selected)
        {
            SkillTree.Instance.checkout.Add(this);
            SkillTree.Instance.currentCost += cost;
            Debug.Log("Added " + cost + " to make the current " + SkillTree.Instance.currentCost);
            SkillTree.Instance.stillAvailablePoints -= cost;
            ColorBlock cb = toggle.colors;
            cb.normalColor = Color.red;
            cb.selectedColor = Color.red;
            toggle.colors = cb;
        }
        else
        {
            SkillTree.Instance.checkout.Remove(this);
            SkillTree.Instance.currentCost -= cost;
            Debug.Log("Subtracted " + cost + " to make the current " + SkillTree.Instance.currentCost);
            SkillTree.Instance.stillAvailablePoints += cost;
            ColorBlock cb = toggle.colors;
            cb.normalColor = Color.gray;
            cb.selectedColor = Color.gray;
            toggle.colors = cb;
        }
    }

    public void OnPointerEnter(PointerEventData pointer)
    {
        SkillTree.Instance.textTooltip.text = description;
    }

    public void OnPointerExit(PointerEventData pointer)
    {
        SkillTree.Instance.textTooltip.text = "";
    }

    public void Unlock()
    {
        if (!CanBeBought()) return;

        PlayerSkills.instance.Unlock(type);

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