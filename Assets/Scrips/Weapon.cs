using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 1)]

public class Weapon : ScriptableObject
{
    [SerializeField] string weaponName;
    [SerializeField] float weaponDamage;
    [SerializeField] float armorPenetration;
    [SerializeField] int magazine;
    [SerializeField] int magazineSize;
    [SerializeField] float weaponRange;
    public float WeaponRange => weaponRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
