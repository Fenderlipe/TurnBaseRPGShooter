using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    [Header ("Movement Control")]
    Rigidbody rb;

    Vector3 destination;
    [SerializeField] Transform destinoDummie;
    NavMeshAgent agent;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        
        agent.destination = destinoDummie.position;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            
            HandleClick();
        }
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