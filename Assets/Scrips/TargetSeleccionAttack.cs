using UnityEngine;

public class TargetSelectionAttack : MonoBehaviour
{
    [SerializeField] GameObject target_1;
    [SerializeField] GameObject target_2;
    [SerializeField] GameObject characterShooting;

    Shooting shootingComponent;
    [SerializeField] Weapon Weapon;
    PlayerCharacter playerCharacter;

    void Start()
    {
        shootingComponent = characterShooting.GetComponent<Shooting>();
        playerCharacter = characterShooting.GetComponent<PlayerCharacter>();
    }

    public void ShootTarget1()
    {
        shootingComponent.Shoot(target_1.transform.position, playerCharacter.EquippedWeapon.WeaponRange);
        Debug.Log("apunta a" + target_1.name);
    }

    public void ShootTarget2()
    {
        shootingComponent.Shoot(target_2.transform.position, playerCharacter.EquippedWeapon.WeaponRange);
        Debug.Log("apunta a" + target_2.name);

    }
}