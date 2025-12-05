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


    void Start()
    {

        if (equippedWeapon != null)
            Debug.Log(gameObject.name + " usa el arma fija: " + equippedWeapon.GetWeaponName());


        if (equippedEquipment == null && equipmentList.Count > 0)
            equippedEquipment = equipmentList[0];


        if (targetSelectionPanel != null)
            targetSelectionPanel.SetActive(false);
    }


    public Weapon GetEquippedWeapon()
    {
        return equippedWeapon;
    }



    public void EquipWeapon(int weaponIndex)
    {
        Debug.LogWarning(gameObject.name + " no puede cambiar de arma.");
        return;
    }



    public List<Weapon> GetWeaponList()
    {
        return new List<Weapon>();
    }


    public override float GetTotalArmor()
    {
        if (equippedEquipment == null && equipmentList.Count > 0)
            equippedEquipment = equipmentList[0];

        float totalArmor = 0f;

        if (equippedEquipment != null)
            totalArmor = equippedEquipment.GetArmor();

        return totalArmor;
    }
}