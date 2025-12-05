using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 1)]

public class Weapon : ScriptableObject
{
    [SerializeField] string weaponName;
    [SerializeField] float weaponDamage;
    [SerializeField] float weaponPenetration;
    [SerializeField] float armorPenetration;
    [SerializeField] int magazine;
    [SerializeField] int magazineSize;
    [SerializeField] float weaponRange;

    public float GetWeaponDamage() => weaponDamage;
    public string GetWeaponName() => weaponName;
    public float GetWeaponRange() => weaponRange;
    public float GetWeaponPenetration() => weaponPenetration;


}
