using UnityEngine;

public class DisableAnimatorOnHit : MonoBehaviour
{
    public Animator animator; // Référence à l'Animator du zombie

    private void OnCollisionEnter(Collision collision)
    {
        // Vérifier si l'objet qui touche est une balle (tag "Bullet")
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Désactiver l'Animator
            if (animator != null)
            {
                animator.enabled = false;
            }
        }
    }
}
