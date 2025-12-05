using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ClickToMove : MonoBehaviour
{
    [Header("Control de Movimiento")]
    [SerializeField] private float moveSpeed;

    Rigidbody rb;

    Vector3 destination;
    [SerializeField] Transform destinationCapsule;
    NavMeshAgent agent;
    Animator animator;
    private Vector3 velocidadX;
    LineRenderer line;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        line = GetComponent<LineRenderer>();

        agent.destination = destinationCapsule.position;
        agent.updatePosition = false;
    }


    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            HandleClick();
        }

        //velocidadX = agent.velocity;
        animator.SetFloat("forwardMovement", agent.velocity.magnitude);

        DrawPath();

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            {
                Unit unit = GetComponent<Unit>();
                unit.FinishMovement();

            }
        }

    }

    private void HandleClick()
    {

        if (!enabled) return;

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        Unit unit = GetComponent<Unit>();

        if (unit.hasMoved)
            return;

        RaycastHit hit;


        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f))
        {
            destinationCapsule.position = hit.point;
            agent.destination = destinationCapsule.position;
            //hit.collider.GetComponent<Unit>();
        }

    }

    private void DrawPath()
    {
        if (agent == null) return;

        NavMeshPath path = agent.path;

        if (path.corners.Length < 2)
        {
            line.positionCount = 0;
            return;
        }

        line.positionCount = path.corners.Length;
        line.SetPositions(path.corners);

    }




    private void OnAnimatorMove()
    {
        Vector3 position = animator.rootPosition;
        position.y = agent.nextPosition.y;
        transform.position = position;
        agent.nextPosition = transform.position;
    }

    private void OnDrawGizmos()
    {
        if (agent == null || agent.path == null) return;

        var path = agent.path;

        Gizmos.color = Color.cyan;

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Gizmos.DrawLine(path.corners[i], path.corners[i + 1]);
            Gizmos.DrawSphere(path.corners[i], 0.1f);
        }

        // Último punto
        if (path.corners.Length > 0)
            Gizmos.DrawSphere(path.corners[path.corners.Length - 1], 0.1f);
    }
}