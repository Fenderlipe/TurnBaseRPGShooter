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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        //agent.destination = destinoDummie.position;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            
            HandleClick();
        }

        velocidadX = agent.velocity;
        animator.SetFloat("InputX", agent.velocity.x);
        animator.SetFloat("InputY", agent.velocity.z);
    }
    private void HandleClick()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f))
        {
            destinoDummie.position = hit.point;
            agent.destination = destinoDummie.position;
        }
        //StartCoroutine(MoveToPosition(destination));   
    }

}