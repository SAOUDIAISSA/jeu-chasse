using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f;  // Temps avant que la balle soit automatiquement détruite

    void Start()
    {
        // Détruire la balle après un certain temps pour éviter qu'elle reste dans la scène
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Détruire la balle en cas de collision
        Debug.Log("Balle détruite après collision avec : " + collision.gameObject.name);
        Destroy(gameObject);
    }
}
