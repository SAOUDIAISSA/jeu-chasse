using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Mouvement")]
    public float speed = 5f;             // Vitesse de d�placement

    [Header("Souris")]
    public float mouseSensitivity = 2f; // Sensibilit� de la souris
    public float verticalLimit = 60f;   // Limite de rotation verticale (en degr�s)

    [Header("R�f�rences")]
    public Transform leftHandTransform;  // R�f�rence � la main gauche du personnage
    public Transform rightHandTransform; // R�f�rence � la main droite du personnage
    private Quaternion initialLeftHandRotation;  // Rotation initiale de la main gauche
    private Quaternion initialRightHandRotation; // Rotation initiale de la main droite

    private float verticalRotation = 0f; // Rotation verticale actuelle
    private Rigidbody rb;                // R�f�rence au Rigidbody

    private void Start()
    {
        // Initialisation du Rigidbody
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Emp�cher les rotations dues � la physique

        // Verrouiller le curseur pour une meilleure exp�rience utilisateur
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Sauvegarder les rotations initiales des mains
        if (leftHandTransform != null)
        {
            initialLeftHandRotation = leftHandTransform.localRotation;
        }
        if (rightHandTransform != null)
        {
            initialRightHandRotation = rightHandTransform.localRotation;
        }
    }

    private void Update()
    {
        // Gestion de la rotation du personnage et des mains avec la souris
        HandleMouseRotation();
    }

    private void FixedUpdate()
    {
        // Gestion du mouvement avec le Rigidbody
        HandleMovement();
    }

    private void HandleMouseRotation()
    {
        // Rotation horizontale du personnage (gauche/droite)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up, mouseX); // Tourner le personnage sur l'axe Y

        // Rotation verticale des mains (haut/bas)
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLimit, verticalLimit);

        // Appliquer la rotation verticale aux mains tout en conservant leurs rotations initiales
        if (leftHandTransform != null)
        {
            leftHandTransform.localRotation = initialLeftHandRotation * Quaternion.Euler(verticalRotation, 0f, 0f);
        }
        if (rightHandTransform != null)
        {
            rightHandTransform.localRotation = initialRightHandRotation * Quaternion.Euler(verticalRotation, 0f, 0f);
        }
    }

    private void HandleMovement()
    {
        // Obtenir les entr�es de mouvement
        float moveForward = Input.GetAxis("Vertical");   // Z/S ou W/S
        float moveSideways = Input.GetAxis("Horizontal"); // Q/D ou A/D

        // Calculer la direction de mouvement
        Vector3 moveDirection = (transform.forward * moveForward + transform.right * moveSideways).normalized;

        // Appliquer le mouvement au Rigidbody
        if (moveDirection.magnitude > 0.01f) // �viter les micro-mouvements
        {
            rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
        }
    }
}
