using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private Vector3 initialPosition;

    void Start()
    {
        // Store the initial position at the start of the script.
        initialPosition = transform.position;
    }

    void Update()
    {
        // Calculate the current distance from the initial position.
        float distance = Vector3.Distance(transform.position, initialPosition);

        // Check if the distance exceeds the threshold (10 units).
        if (distance > 10f)
        {
            // If the distance exceeds 100 units, destroy the GameObject.
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);ewss
        if (other.CompareTag("Enemy"))
        {
            // Check if the arrow has collided with an object that has a CharacterController.
            CharacterController characterController = other.GetComponent<CharacterController>();
            if (characterController != null)
            {
                // Destroy the enemy (the object with the CharacterController component).
                Destroy(other.gameObject);

                // Destroy the arrow.
                Destroy(gameObject);
            }
        }
        
    }
}
