using System;
using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;

[RequireComponent(typeof(Unit))]
[RequireComponent(typeof(Shooting))]

public class EnemyAI : MonoBehaviour
{
    private Unit unit;
    private Shooting shooting;
    [SerializeField] private float visionRange = 15f;
    private float attackRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Awake()
    {
        unit = GetComponent<Unit>();
        shooting = GetComponent<Shooting>();
    }

    // Update is called once per frame
    void Update()
    {
        if (unit.isFriendly) return;

        if(TurnManager.Instance.isPlayerTurn)
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
        Unit target = FindClosestPlayerUnit();
        if (target == null)
        {
            Debug.Log(unit.characterName + "No encuentra objetivos validos, salta el turno");
            unit.FinishAction();
            yield break;
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        if (distanceToTarget <= attackRange && hasLineOfSight(target))
        {
            yield return AttackTarget(target);
        }
    }

    private object AttackTarget(Unit target)
    {
        throw new NotImplementedException();
    }

    private bool hasLineOfSight(Unit target)
    {
        throw new NotImplementedException();
    }

    private Unit FindClosestPlayerUnit()
    {
        Unit closest = null;
        float closestDist = Mathf.Infinity;

        foreach(Unit playerUnit in TurnManager.Instance.playerUnits)
        {
            float dist = Vector3.Distance(transform.position, playerUnit.transform.position);
            if(dist < closestDist && dist <= visionRange)
            {
                closestDist = dist;
                closest = playerUnit;
            }
        }
        return closest;
    }
}
