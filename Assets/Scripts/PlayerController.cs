using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 moveInput;
    public Rigidbody Rigidbody;
    private int currentPoints;
    private int currentHealth;
    private int maxHealth;
    public float playerSpeed = 10f;


    public Transform groudCheck;
    public float groundDistance;
    public LayerMask groundLayer;
    private bool isGrounded;
    private bool jumpInput;
    private float jumpForce;

    public HealthManager healthManager;

    public static Action<int, int> OnHealthUpdated;
    public static Action<int> OnPointsUpdated;
    public static Action OnPlayerWon;
    public static Action OnPlayerDefeated;


    public static void UpdateHealth(int currentHealth, int maxHealth)
    {
        OnHealthUpdated?.Invoke(currentHealth, maxHealth);
    }

    public static void UpdatePoints(int points)
    {
        OnPointsUpdated?.Invoke(points);
    }

    public static void PlayerWon()
    {
        OnPlayerWon?.Invoke();
    }

    public static void PlayerDefeated()
    {
        OnPlayerDefeated?.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groudCheck.position, groundDistance, groundLayer);
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * playerSpeed * Time.deltaTime;
        Rigidbody.MovePosition(Rigidbody.position + transform.TransformDirection(move));

        if (jumpInput)
        {
            Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpInput = false;
        }

    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpInput = true;
        }
        else if (context.canceled)
        {
            jumpInput = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            PlayerWon();
        }
        else if (other.CompareTag("Coin"))
        {
            currentPoints += 10;
            UpdatePoints(currentPoints);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            healthManager.TakeDamage(10);
        }
    }
}
