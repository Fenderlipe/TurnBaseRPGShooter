using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    [Header("Movement Control")]
    [SerializeField] private float moveSpeed;

    Rigidbody rb;

    Vector3 destination;
    [SerializeField] Transform destinoDummie;
    NavMeshAgent agent;
    Animator animator;
    private Vector3 velocidadX;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        agent.destination = destinoDummie.position;
        agent.updatePosition = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            
            HandleClick();
            Unit unit = GetComponent<Unit>();
            unit.FinishMovement();
        }

        animator.SetFloat("forwardMovement", agent.velocity.magnitude);
    }
    private void HandleClick()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f))
        {
            destinoDummie.position = hit.point;
            agent.destination = destinoDummie.position;
            hit.collider.GetComponent<Unit>();
        }

        //StartCoroutine(MoveToPosition(destination));   
    }

    private void OnAnimatorMove()
    {
        Vector3 position = animator.rootPosition;
        position.y = agent.nextPosition.y;
        transform.position = position;
        agent.nextPosition = transform.position; 
    }

}