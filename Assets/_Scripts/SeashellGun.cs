using UnityEngine;

public class SeashellGun : MonoBehaviour
{
    public GameObject shellPrefab;
    public Transform firePoint;
    public float shootForce = 25f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click to shoot
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject shell = Instantiate(shellPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = shell.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * shootForce, ForceMode.Impulse);
    }
}
