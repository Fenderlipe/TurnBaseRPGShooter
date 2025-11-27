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
    [SerializeField] private float visionRange = 5f;
    private float attackRange;
    public float weaponRange;
    NavMeshAgent agent;

    void Start()
    {

    }

    private void Awake()
    {
        unit = GetComponent<Unit>();
        shooting = GetComponent<Shooting>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (unit.isFriendly) return;

        if (TurnManager.Instance.isPlayerTurn)
        {
            return;
        }

        if (!unit.hasActed)
        {
            StartCoroutine(DoenemyTurn());
        }
    }

    IEnumerator DoenemyTurn()
    {
        Unit target = FindClosestPlayerUnit(); //Encontrar aliado cercano

        //sin aliados cercanos, salta turno
        if (target == null)
        {
            Debug.Log(unit.characterName + "no encuentra objetivos validos");
            unit.FinishAction();
            yield break;
        }

        //atacar en linea de vision
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        if (distanceToTarget <= attackRange && hasLineOfSight(target))
        {
            yield return AttackTarget(target);
        }

        else //mover personaje que este cerca para atacar
        {
            yield return MoveTowardTarget(target.transform.position);

            //volver a intentar disparar
            distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

            if (distanceToTarget <= attackRange && hasLineOfSight(target))
            {
                yield return AttackTarget(target);
            }
            else
                unit.FinishAction();
        }
    }

    private IEnumerator MoveTowardTarget(Vector3 targetPosition)
    {
        Debug.Log(unit.characterName + "se mueve buscando a su objetivo:");

        agent.destination = targetPosition;

        yield return new WaitForSeconds(5);

        unit.FinishMovement();
    }

    private IEnumerator AttackTarget(Unit target)
    {
        Debug.Log(unit.characterName + "ataca a" + target.characterName);

        Vector3 lookDir = target.transform.position - transform.position;
        lookDir.y = 0f;
        if (lookDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookDir);
        }

        shooting.Shoot(target.transform.position, attackRange);

        yield return new WaitForSeconds(0.2f);

        if (unit.hasMoved)
        {
            unit.FinishAttack();
            unit.FinishAction();
        }
        else
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
