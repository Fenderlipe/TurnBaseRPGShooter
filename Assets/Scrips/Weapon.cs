using UnityEngine;

public class Weapon : ScriptableObject
{
    [SerializeField] string weaponName;
    [SerializeField] float weaponDamage;
    [SerializeField] float armorPenetration;
    [SerializeField] int magazine;
    [SerializeField] int magazineSize;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
