using System.Collections;
using UnityEngine;

public class ClickToMove : MonoBehaviour
{
    [Header ("Movement Control")]
    [SerializeField] private float moveSpeed;

    Rigidbody rb;

    Vector3 destination;
    [SerializeField] Transform destinoDummie;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        destination = destinoDummie.position;
        if (Input.GetMouseButtonDown(1))
        {
            HandleClick();
        }
    }
    private void HandleClick()
    {
        StartCoroutine(MoveToPosition(destination));   
    }

    IEnumerator MoveToPosition (Vector3 _destination)
    {
        Vector3 moveDirection = _destination - transform.position;
        rb.AddForce(moveDirection * moveSpeed, ForceMode.VelocityChange);
        yield return null;
    }

}