using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlayerCharacter : Character
{

    float experience;
    Weapon equippedWeapon;
    Equipment equippedEquipment;
    List<Equipment> equipmentList = new List<Equipment>();
    List<Weapon> weaponList = new List<Weapon>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
