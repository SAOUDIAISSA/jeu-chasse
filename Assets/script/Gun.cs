using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;  // Prefab de la balle
    public Transform firePoint;      // Point de sortie de la balle
    public float bulletSpeed = 20f;  // Vitesse de la balle
    public AudioSource gunSound;     // Référence à l'AudioSource du tir

    void Update()
    {
        // Vérifie si le joueur appuie sur le bouton de tir
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

        // Ajoute une force à la balle pour qu'elle se déplace
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }

        // Optionnel : détruire la balle après un certain temps
        Destroy(bullet, 5f);
    }
}
