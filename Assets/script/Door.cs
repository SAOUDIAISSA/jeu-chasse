using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform doorTransform;       // Transform de la porte
    public Vector3 openRotation;          // Rotation cible pour ouvrir la porte
    public float openSpeed = 2f;          // Vitesse d'ouverture de la porte
    private bool isOpening = false;       // État de la porte (ouverture)

    private Quaternion initialRotation;   // Rotation initiale de la porte
    private Quaternion targetRotation;    // Rotation cible (ouverte)

    private void Start()
    {
        // Sauvegarder la rotation initiale
        if (doorTransform == null)
        {
            doorTransform = transform;
        }
        initialRotation = doorTransform.localRotation;
        targetRotation = initialRotation * Quaternion.Euler(openRotation);
    }

    private void Update()
    {
        // Si la porte doit s'ouvrir, interpoler vers la rotation cible
        if (isOpening)
        {
            doorTransform.localRotation = Quaternion.Lerp(doorTransform.localRotation, targetRotation, Time.deltaTime * openSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Vérifier si le chasseur touche la porte
        if (other.CompareTag("Player")) // Assurez-vous que le chasseur a le tag "Player"
        {
            isOpening = true; // Activer l'ouverture de la porte
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Optionnel : refermer la porte lorsque le chasseur s'éloigne
        if (other.CompareTag("Player"))
        {
            isOpening = false; // Désactiver l'ouverture pour revenir à l'état initial
        }
    }
}
