using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlayerCharacter : Character
{

    float experience;
    [SerializeField] Weapon equippedWeapon;
    [SerializeField] Equipment equippedEquipment;
    [SerializeField] List<Equipment> equipmentList = new List<Equipment>();
    [SerializeField] List<Weapon> weaponList = new List<Weapon>();
    public GameObject targetSelectionPanel;
    public Weapon EquippedWeapon => equippedWeapon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetSelectionPanel.SetActive(false);
        equippedWeapon = weaponList[0];
        equippedEquipment = equipmentList[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Earnexperience(float expGain)
    {
        experience += expGain;
    }

    void LevelUp()
    {
        level++;
    }
}
