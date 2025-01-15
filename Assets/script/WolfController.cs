using UnityEngine;

public class WolfController : MonoBehaviour
{
    public Animator animator;  // Référence à l'Animator du loup
    public int health = 1;     // Points de vie du loup (1 coup suffit pour le tuer)
    public bool isDead = false;  // Vérifie si le loup est mort

    void Start()
    {
        // Si l'Animator n'est pas configuré, on le récupère automatiquement
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    // Vérifie les collisions
    void OnCollisionEnter(Collision collision)
    {
        // Si la balle touche le loup et qu'il n'est pas encore mort
        if (!isDead && collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Le loup a été touché par une balle !");
            TakeDamage(health);  // Réduit la santé du loup
        }
    }

    // Méthode pour réduire la santé du loup lorsqu'il prend des dégâts
    public void TakeDamage(int damage)
    {
        if (isDead) return;  // Si le loup est déjà mort, on ne fait rien.

        health -= damage;  // Réduit la santé

        if (health <= 0)
        {
            Die();  // Si la santé est à zéro ou moins, le loup meurt
            isDead = true;
        }
    }

    // Méthode qui déclenche l'animation de mort et gère les actions à faire après la mort
    void Die()
    {
        Debug.Log("Le loup meurt.");
        animator.SetTrigger("isDead");  // Déclenche l'animation de mort

        // Désactive les collisions et ce script du loup
        GetComponent<Collider>().enabled = false; // Désactive les collisions
        this.enabled = false;  // Désactive le script pour ne plus calculer les dégâts ou autres

        // Appelle la méthode pour ajouter des points au score du joueur (10 points par loup tué par exemple)
        GameManager.instance.AddScore(10);
    }
}
