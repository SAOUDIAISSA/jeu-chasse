using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;  // Prefab de la balle
    public Transform firePoint;      // Point de sortie de la balle
    public float bulletSpeed = 20f;  // Vitesse de la balle
    public AudioSource gunSound;     // R�f�rence � l'AudioSource du tir

    void Update()
    {
        // V�rifie si le joueur appuie sur le bouton de tir
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Jouer le son du tir
        if (gunSound != null)
        {
            gunSound.Play();
        }

        // Instancie la balle au point de tir
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Ajoute une force � la balle pour qu'elle se d�place
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }

        // Optionnel : d�truire la balle apr�s un certain temps
        Destroy(bullet, 5f);
    }
}
