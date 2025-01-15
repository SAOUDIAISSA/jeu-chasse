using UnityEngine;

public class DisableAnimatorOnHit : MonoBehaviour
{
    public Animator animator; // R�f�rence � l'Animator du zombie

    private void OnCollisionEnter(Collision collision)
    {
        // V�rifier si l'objet qui touche est une balle (tag "Bullet")
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // D�sactiver l'Animator
            if (animator != null)
            {
                animator.enabled = false;
            }
        }
    }
}
