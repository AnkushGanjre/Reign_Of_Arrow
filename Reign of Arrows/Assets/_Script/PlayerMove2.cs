using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
public class PlayerMove2 : MonoBehaviour
{
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float animDampTime = 0.15f;
    [SerializeField] float smoothTime = 0.05f;

    Vector2 playerMoveInput;
    float currentVelocity;

    CharacterController characterController;
    Animator anim;

    public string enemyTag = "Enemy"; // Tag of the enemy GameObjects.
    public float rotationSpeed = 5f; // Speed at which the player rotates to face the detected enemy.

    [SerializeField] private Transform targetEnemy; // Reference to the detected enemy's transform.

    private void Awake()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Move the player with the given input.
        MovePlayer();

        // Find the nearest enemy with the specified tag.
        FindNearestEnemy();

        // Rotate the player to face the detected enemy.
        RotateTowardsEnemy();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        playerMoveInput = context.ReadValue<Vector2>();
    }

    private void MovePlayer()
    {
        Vector3 moveInput = (new Vector3(playerMoveInput.x, 0, playerMoveInput.y)).normalized;
        float moveAmount = Mathf.Abs(playerMoveInput.x) + Mathf.Abs(playerMoveInput.y);

        // Movement
        characterController.Move(moveInput * walkSpeed * Time.deltaTime);

        if (moveAmount > 0)        // Walking
        {
            if (Input.GetKey(KeyCode.C))        // Aiming while moving
            {
                moveAmount = 2;
            }
            if (Input.GetKey(KeyCode.Space))        // Shoot While moving
            {
                moveAmount = 3;
            }

            // Rotation
            //var targetAngle = Mathf.Atan2(moveInput.x, moveInput.z) * Mathf.Rad2Deg;
            //var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
            //transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        }
        else        // Idle
        {
            if (Input.GetKey(KeyCode.C))        // Aiming While Idle
            {
                moveAmount = -1;
            }
            if (Input.GetKey(KeyCode.Space))        // Shoot while Idle
            {
                moveAmount = -2;
            }
        }

        // Set Animation
        anim.SetFloat("ErikaMoveVal", moveAmount, animDampTime, Time.deltaTime);
    }

    private void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float nearestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < nearestDistance)
            {
                nearestDistance = distanceToEnemy;
                nearestEnemy = enemy.transform;
            }
        }

        targetEnemy = nearestEnemy;
    }

    private void RotateTowardsEnemy()
    {
        if (targetEnemy != null)
        {
            // Calculate the direction towards the detected enemy.
            Vector3 direction = (targetEnemy.position - transform.position).normalized;

            // Calculate the rotation angle to face the enemy.
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // Smoothly rotate the player towards the enemy.
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        }
    }
}
