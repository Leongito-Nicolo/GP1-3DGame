using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float fireCooldown;                                      // seconds unitl can shoot a bullet

    private float currentFireCooldown;                              // seconds passed between each bullet

    public float gunDamage;                                         // damage of the gun
    public float maxRange;                                          // range of the bullets

    [SerializeField] private PlayerController playerController;

    [SerializeField] private Transform firePoint;                   // point where the laser start
    [SerializeField] private LineRenderer lineRenderer;             // component
    [SerializeField] private float trailDuration;                   // duration of laser

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentFireCooldown = fireCooldown;

        lineRenderer.startWidth = 0.05f;    // set the width of the laser
        lineRenderer.enabled = false;       // disable on start
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.hasPickedWeapon && Time.timeScale == 1)
        {
            // checks for player's shoot input
            if (Input.GetMouseButton(0))
            {
                // checks if player can shoot
                if (currentFireCooldown <= 0)
                {
                    Shoot();       // shoot
                    currentFireCooldown = fireCooldown;     // reset timer
                }
            }

            currentFireCooldown -= Time.deltaTime;      // decrement timer
        }
    }

    public void Shoot()
    {
        Vector3 targetPoint = firePoint.position + (firePoint.forward * maxRange);      // set the target point of the laser

        // cast a ray in the direction of the mouse position
        Ray hit = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(hit, out RaycastHit hitInfo, maxRange))
        {
            // if it touches an enemy it reduces its health
            if (hitInfo.collider.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.currentEnemyHealth -= gunDamage;
            }
        }

        StartCoroutine(ShowBulletTrail(targetPoint));
    }

    // coroutine to show the laser only for seconds
    public IEnumerator ShowBulletTrail(Vector3 hitPosition)
    {
        lineRenderer.SetPosition(0, firePoint.position);    // set the start position
        lineRenderer.SetPosition(1, hitPosition);       // set the end position

        lineRenderer.enabled = true;        // enable laser
        yield return new WaitForSeconds(trailDuration);
        lineRenderer.enabled = false;       // disable laser
    }
}
