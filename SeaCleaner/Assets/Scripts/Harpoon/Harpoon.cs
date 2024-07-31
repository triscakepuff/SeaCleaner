using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float projectileSpeed = 10f;

    private bool isProjectileActive = false; // Flag to track active projectile

    public float playerToHarpoonStart = 5f;
    public float offsetX = 1f;
    public float offsetY = 1f;

    public bool HarpoonActive = false;
    private Animator animator;
   

    public GameObject Crosshair;

    private InventorySystem inventorySystem;

    private void Start()
    {
        inventorySystem = GameObject.FindFirstObjectByType<InventorySystem>();
        animator = GetComponent<Animator>();
        Crosshair.SetActive(false);
    }
    void Update()
    {
        CheckClickH();

        if (HarpoonActive)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;  // Since this is a 2D game, we keep the z coordinate at 0

            // Calculate the direction from the player to the mouse position
            Vector3 directionToMouse = mousePosition - transform.position;

            // Calculate the direction
            Vector3 fixedDirection = directionToMouse.normalized;

            Vector3 CrosshairPos = transform.position + (fixedDirection * playerToHarpoonStart);
            CrosshairPos = new Vector3(CrosshairPos.x + offsetX, CrosshairPos.y + offsetY);
            // Set the position of the game object to be at a fixed distance from the player in the opposite direction
            shootPoint.transform.position = CrosshairPos;


            if (Input.GetMouseButtonDown(0) && !isProjectileActive && inventorySystem.inventoryItemCount[0]>0)
            {
                inventorySystem.inventoryItemCount[0]--;
                ShootProjectile(shootPoint.transform.position);
            }
        }

        if (HarpoonActive)
        {
            animator.SetBool("Harpoon", true);
            Crosshair.SetActive(true);
        }
        else
        {
            animator.SetBool("Harpoon", false);
            Crosshair.SetActive(false);
        }
    }

    void CheckClickH()
    {
        if(inventorySystem.inventorySelect == 1)
        {
            HarpoonActive = true;
        }
        else
        {
            HarpoonActive = false;
        }
    }

    void ShootProjectile(Vector3 targetPosition)
    {
        if (isProjectileActive)
            return; // Prevent shooting if a projectile is already active

        isProjectileActive = true;

        // Instantiate the projectile at the shoot point's position
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Calculate the direction and velocity of the projectile
        Vector3 shootDirection = (targetPosition - GameObject.FindGameObjectWithTag("Player").transform.position).normalized;
        rb.velocity = shootDirection * projectileSpeed;

        // Set the rotation of the projectile based on its velocity
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Adjust angle by -90 degrees

        // Register the projectile's destruction event
        HarpoonProjectile arrowScript = projectile.GetComponent<HarpoonProjectile>();
        if (arrowScript != null)
        {
            arrowScript.OnDestroyCallback += () => isProjectileActive = false;
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Set the color of the Gizmo
        Gizmos.DrawWireSphere(transform.position, playerToHarpoonStart); // Draw a wireframe sphere representing the radius
    }
}
