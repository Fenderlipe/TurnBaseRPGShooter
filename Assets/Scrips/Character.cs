using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [Header("Character Main")]
    [SerializeField] string name;
    protected int level;
    [Header("Character Stats")]
    [SerializeField] protected float currentLife;
    [SerializeField] protected float maxLife;
    [SerializeField] float baseAttackDamage;
    private bool isDead = false;

    void Start()
    {
        name = gameObject.name;
        currentLife = maxLife;
    }

    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        float armorReduction = GetTotalArmor();
        float finalDamage = damage - armorReduction;
        if (finalDamage < 0) finalDamage = 0;
        currentLife -= finalDamage;
        Debug.Log(name + " recibe " + finalDamage + " de daño. Vida actual: " + currentLife);

        if (currentLife <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float weaponDamage, float penetration)
    {
        if (isDead) return;

        float armorReduction = GetTotalArmor();
        Debug.Log(name + " armadura total: " + armorReduction);

        armorReduction = Mathf.Max(0, armorReduction - penetration);

        float finalDamage = weaponDamage - armorReduction;
        if (finalDamage < 0) finalDamage = 0;
        currentLife -= finalDamage;
        Debug.Log(name + " recibe " + finalDamage + " de daño (Arma: " + weaponDamage + " - (Armadura: " + GetTotalArmor() + " - Penetración: " + penetration + ")). Vida actual: " + currentLife);

        if (currentLife <= 0)
        {
            Die();
        }
    }

    // NUEVO: Método centralizado para manejar la muerte
    private void Die()
    {
        if (isDead) return; // Evitar ejecutar dos veces

        isDead = true;
        Debug.Log(name + " ha muerto");

        // Marcar el turno como terminado automáticamente
        Unit unit = GetComponent<Unit>();
        if (unit != null)
        {
            unit.hasActed = true;
            unit.hasAttacked = true;
            unit.hasMoved = true;

            // Forzar verificación del fin de turno
            TurnManager.Instance.CheckEndTurn();
        }

        // Desactivar componentes necesarios
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (agent != null)
            agent.enabled = false;

        EnemyAI ai = GetComponent<EnemyAI>();
        if (ai != null)
            ai.enabled = false;

        // Opcional: desactivar el objeto después de un delay
        // Destroy(gameObject, 2f);
    }

    public virtual float GetTotalArmor()
    {
        return 0f;
    }

    public bool IsAlive()
    {
        return !isDead;
    }

    public float GetCurrentLife()
    {
        return currentLife;
    }

    public float GetMaxLife()
    {
        return maxLife;
    }
}