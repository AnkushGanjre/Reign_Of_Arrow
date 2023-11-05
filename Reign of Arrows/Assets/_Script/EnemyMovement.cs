using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target; // The target the enemy will move towards.
    [SerializeField] GameObject enemy;
    [SerializeField] float smoothTime = 0.05f;

    float movementSpeed;
    float currentVelocity;
    CharacterController characterController;

    private void Awake()
    {
        enemy = GameObject.Find("Enemy");
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        movementSpeed = enemy.GetComponent<EnemyScript>().movementSpeed;
        if (target == null)
        {
            // If the target is not assigned, find the player as the default target.
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        if (target != null)
        {
            // Calculate the direction towards the target.
            Vector3 direction = (target.position - transform.position).normalized;

            // Move the enemy in the calculated direction.
            characterController.Move(direction * movementSpeed * Time.deltaTime);

            // Rotate the enemy to face the direction of movement.
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        }
    }
}
