using UnityEngine;
using System;

public class Shooting : MonoBehaviour
{
    Animator animator;
    [SerializeField] ParticleSystem particleSparks;
    private Unit unit;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        animator = GetComponent<Animator>();
    }

    public void Shoot(Vector3 enemyPosition, float weaponRange)
    {
        animator.SetTrigger("Attack");

        if (IsOnLoS(enemyPosition, weaponRange))
        {
            particleSparks.Play();
            Debug.Log("Enemigo en linea de tiro");

            // NUEVO: Aplicar daño al enemigo
            ApplyDamageToTarget(enemyPosition, weaponRange);
        }
        else
        {
            Debug.Log("Enemigo no esta en linea de tiro");
        }

        if (unit != null)
        {
            animator.SetFloat("forwardMovement", 0f);
            unit.FinishAttack();
        }
    }

    public bool IsOnLoS(Vector3 enemyPosition, float weaponRange)
    {
        RaycastHit hit;
        Vector3 direction = (enemyPosition - transform.position).normalized;

        Debug.DrawRay(transform.position, direction * weaponRange, Color.red, 1f);

        if (Physics.Raycast(transform.position, direction, out hit, weaponRange))
        {
            Debug.Log("Preparado para disparar");

            Character character = hit.collider.GetComponent<Character>();

            if (character != null)
            {
                return true;
            }
        }
        return false;
    }

    // NUEVO MÉTODO: Aplicar daño al objetivo
    private void ApplyDamageToTarget(Vector3 enemyPosition, float weaponRange)
    {
        RaycastHit hit;
        Vector3 direction = (enemyPosition - transform.position).normalized;

        if (Physics.Raycast(transform.position, direction, out hit, weaponRange))
        {
            Character targetCharacter = hit.collider.GetComponent<Character>();

            if (targetCharacter != null && targetCharacter.IsAlive())
            {
                // Obtener el arma equipada del jugador
                PlayerCharacter playerChar = GetComponent<PlayerCharacter>();
                float damageDealt = 10f; // Daño por defecto
                float penetration = 0f;
                string weaponUsed = "puño";

                if (playerChar != null)
                {
                    Weapon equippedWeapon = playerChar.GetEquippedWeapon();
                    if (equippedWeapon != null)
                    {
                        damageDealt = equippedWeapon.GetWeaponDamage();
                        penetration = equippedWeapon.GetWeaponPenetration();
                        weaponUsed = equippedWeapon.GetWeaponName();
                    }
                }

                // Aplicar el daño
                targetCharacter.TakeDamage(damageDealt, penetration);

                // Generar partículas en el punto de impacto
                GenerateHitParticles(hit.point);

                // Verificar si el enemigo murió
                if (!targetCharacter.IsAlive())
                {
                    Debug.Log(targetCharacter.name + " ha muerto por ataque de " + unit.characterName);
                    Unit enemyUnit = targetCharacter.GetComponent<Unit>();
                    if (enemyUnit != null)
                    {
                        // IMPORTANTE: Marcar que ya actuó para que no bloquee el cambio de turno
                        enemyUnit.hasActed = true;
                        enemyUnit.enabled = false;
                        enemyUnit.gameObject.SetActive(false);
                    }
                }
                else
                {
                    Debug.Log(unit.characterName + " causa " + damageDealt + " de daño a " + targetCharacter.name + " con " + weaponUsed);
                }
            }
        }
    }

    private void GenerateHitParticles(Vector3 hitPoint)
    {
        // Busca el prefab de partículas en Resources o úsalo si está asignado
        GameObject particlesPrefab = Resources.Load<GameObject>("Sparks");

        if (particlesPrefab != null)
        {
            GameObject particles = Instantiate(particlesPrefab, hitPoint, Quaternion.identity);
            ParticleSystem ps = particles.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
                // Destruye el objeto después de que terminen las partículas
                Destroy(particles, ps.main.duration + ps.main.startLifetime.constantMax);
            }
        }
        else
        {
            Debug.LogWarning("No se encontró el prefab 'Sparks' en Resources");
        }
    }
}