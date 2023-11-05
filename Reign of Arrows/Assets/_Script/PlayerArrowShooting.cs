using UnityEngine;

public class PlayerArrowShooting : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;  // The arrow prefab you want to shoot.
    [SerializeField] Transform arrowSpawnPoint;  // The spawn point for arrows (center of the player).
    [SerializeField] Transform arrowTransformPosition;  // The spawn & store position for arrows.
    [SerializeField] float shootForce = 10f;  // The force applied to the arrow when shot.

    private bool isShooting = false;

    private void Update()
    {
        // Check for input to shoot arrows.
        if (Input.GetKeyDown(KeyCode.Space) && !isShooting)  // Check for the initial key press and ensure not already shooting.
        {
            isShooting = true;
            ShootArrow();
        }
    }

    private void ShootArrow()
    {
        if (arrowPrefab != null && arrowSpawnPoint != null)
        {
            // Instantiate an arrow at the arrowSpawnPoint position and rotation.
            Quaternion arrowRotation = Quaternion.Euler(90f, arrowSpawnPoint.parent.rotation.eulerAngles.y, 0f);
            GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowRotation, arrowTransformPosition);

            // Get the arrow's rigidbody.
            Rigidbody arrowRigidbody = arrow.GetComponent<Rigidbody>();

            // Apply force to the arrow in the forward direction of the player.
            if (arrowRigidbody != null)
            {
                arrowRigidbody.AddForce(transform.forward * shootForce, ForceMode.Impulse);
            }
        }
    }

    private void LateUpdate()
    {
        // Check for key release to allow shooting again.
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isShooting = false;
        }
    }
}
