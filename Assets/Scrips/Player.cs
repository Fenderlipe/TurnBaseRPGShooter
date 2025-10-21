using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    float jumpForce;
    float movementForce;

    Vector3 input;

    Rigidbody rb;
    PlayerInput playerInput;

    private void Start()
    {
        jumpForce = 250f;
        movementForce = 10f;

        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        input = playerInput.actions["Move"].ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(input.x, 0f, input.y) * movementForce);
    }
}
