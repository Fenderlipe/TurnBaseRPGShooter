using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Unit))]
[RequireComponent(typeof(Shooting))]
public class EnemyAI : MonoBehaviour
{
    private Unit unit;
    private Shooting shooting;
    [SerializeField] private float visionRange = 30f;
    [SerializeField] private float attackRange;
    public float weaponRange;
    private bool isActing = false;
    NavMeshAgent agent;
    Animator animator;

    void Start()
    {
        attackRange = weaponRange;
    }

    private void Awake()
    {
        unit = GetComponent<Unit>();
        shooting = GetComponent<Shooting>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (unit.isFriendly) return;

        if (TurnManager.Instance.isPlayerTurn)
        {
            return;
        }

        if (!isActing)
        {
            StartCoroutine(DoenemyTurn());
        }
    }

    IEnumerator DoenemyTurn()
    {
        isActing = true;

        Unit target = FindClosestPlayerUnit();

        if (target == null)
        {
            Debug.Log(unit.characterName + " no encuentra objetivos validos");
            unit.FinishAction();
            isActing = false;
            yield break;
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        // Intentar atacar inmediatamente
        if (distanceToTarget <= attackRange && hasLineOfSight(target))
        {
            yield return AttackTarget(target);
            unit.FinishAction();
        }
        else
        {
            // Moverse hacia el objetivo
            yield return MoveTowardTarget(target.transform.position);

            // Intentar atacar otra vez
            distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

            if (distanceToTarget <= attackRange && hasLineOfSight(target))
            {
                yield return AttackTarget(target);
            }
        }
        unit.FinishAction();
        isActing = false;
    }

    private IEnumerator MoveTowardTarget(Vector3 targetPosition)
    {
        Debug.Log(unit.characterName + " se mueve buscando a su objetivo:");

        agent.isStopped = false;
        agent.destination = targetPosition;

        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            animator.SetFloat("forwardMovement", agent.velocity.magnitude);
            yield return null;
        }

        agent.isStopped = true;
        animator.SetFloat("forwardMovement", 0f);
        unit.FinishMovement();
    }

    private IEnumerator AttackTarget(Unit target)
    {
        Debug.Log(unit.characterName + " ataca a " + target.characterName);

        Vector3 lookDir = target.transform.position - transform.position;
        lookDir.y = 0f;
        if (lookDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookDir);
        }

        shooting.Shoot(target.transform.position, attackRange);
        yield return new WaitForSeconds(0.2f);

        unit.FinishAttack();
    }

    private bool hasLineOfSight(Unit target)
    {
        return shooting.isOnLoS(target.transform.position, weaponRange);
    }


    private Unit FindClosestPlayerUnit()
    {


        Unit closest = null;
        float closestDist = Mathf.Infinity;

        foreach (Unit playerUnit in TurnManager.Instance.playerUnits)
        {
            float dist = Vector3.Distance(transform.position, playerUnit.transform.position);
            if (dist < closestDist && dist <= visionRange)
            {
                closestDist = dist;
                closest = playerUnit;
            }
        }

        return closest;

    }
}
