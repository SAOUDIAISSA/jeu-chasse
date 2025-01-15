using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f;  // Temps avant que la balle soit automatiquement d�truite

    void Start()
    {
        // D�truire la balle apr�s un certain temps pour �viter qu'elle reste dans la sc�ne
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // D�truire la balle en cas de collision
        Debug.Log("Balle d�truite apr�s collision avec : " + collision.gameObject.name);
        Destroy(gameObject);
    }
}
