using UnityEngine;

public class TargetSelectionAttack : MonoBehaviour
{
    [SerializeField] GameObject target_1;
    [SerializeField] GameObject target_2;
    [SerializeField] GameObject characterShooting;

    Shooting shooter;
    PlayerCharacter playerChar;

    private void Awake()
    {
        shooter = characterShooting.GetComponent<Shooting>();
        playerChar = characterShooting.GetComponent<PlayerCharacter>();
    }

    public void ShootTarget1()
    {
        if (shooter == null) return;

        Vector3 targetPos = target_1.transform.position;

        // Obtener el rango del arma equipada
        float range = 10f; // Rango por defecto
        if (playerChar != null)
        {
            Weapon equippedWeapon = playerChar.GetEquippedWeapon();
            if (equippedWeapon != null)
            {
                range = equippedWeapon.GetWeaponRange();
            }
        }

        shooter.Shoot(targetPos, range);
    }

    public void ShootTarget2()
    {
        if (shooter == null) return;

        Vector3 targetPos = target_2.transform.position;

        // Obtener el rango del arma equipada
        float range = 10f; // Rango por defecto
        if (playerChar != null)
        {
            Weapon equippedWeapon = playerChar.GetEquippedWeapon();
            if (equippedWeapon != null)
            {
                range = equippedWeapon.GetWeaponRange();
            }
        }

        shooter.Shoot(targetPos, range);
    }
}