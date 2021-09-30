using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement1 : MonoBehaviour
{
    public float speed;
    public float jump;

    public float checkDistance;
    public Transform jumpCheck;
    public LayerMask groundMask;

    public Transform PlayerMesh;

    public bool canJump;
    public bool canMove;

    public Vector3 starting;

    void OnTriggerEnter(Collider collider)
    {
        Destroy(collider.gameObject);
    }

    void FixedUpdate()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Rigidbody rb = GetComponent<Rigidbody>();

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 MoveDirection = forward * verticalInput + right * horizontalInput;

        rb.velocity = new Vector3(MoveDirection.x * speed, rb.velocity.y, MoveDirection.z * speed);

        if(MoveDirection != new Vector3(0, 0, 0))
        {
            PlayerMesh.rotation = Quaternion.LookRotation(MoveDirection);
        }
    }

    void Update()
    {
        canJump = Physics.CheckSphere(jumpCheck.position, checkDistance, groundMask);
        if(canJump && Input.GetButtonDown("Jump"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.up * jump;
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene("Game5");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(jumpCheck.transform.position, checkDistance);
    }
}
