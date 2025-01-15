using UnityEngine;
using UnityEngine.AI; // N�cessaire pour le NavMeshAgent

public class WolfMovement : MonoBehaviour
{
    public NavMeshAgent agent;         // R�f�rence au NavMeshAgent
    public Animator animator;          // R�f�rence � l'Animator
    public float minWaitTime = 2f;     // Temps minimum avant de changer de destination
    public float maxWaitTime = 5f;     // Temps maximum avant de changer de destination
    public float range = 10f;          // Distance dans laquelle le loup peut se d�placer

    private bool isMoving = false;     // V�rifie si le loup se d�place
    private float waitTime;            // Temps d'attente avant le prochain mouvement
    private float waitTimer = 0f;      // Chronom�tre pour attendre avant de bouger
    public WolfController controller;
    void Start()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
        if (animator == null)
            animator = GetComponent<Animator>();

        waitTime = Random.Range(minWaitTime, maxWaitTime);
    }

    void Update()
    {
        if(controller.isDead) 
            return;

        // V�rifier si le loup est en train de marcher
        if (agent.remainingDistance <= agent.stoppingDistance && isMoving)
        {
            isMoving = false;
            animator.SetBool("isWalking", false); // Revenir � l'�tat Idle
            waitTimer = 0f; // R�initialiser le chronom�tre
        }

        // Chronom�tre pour changer de destination
        if (!isMoving)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                MoveToRandomPosition();
            }
        }
    }

    void MoveToRandomPosition()
    {
        // Trouver une position al�atoire
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection += transform.position; // Ajouter la position actuelle pour obtenir une zone proche

        // V�rifier si la position est sur le NavMesh
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, range, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position); // D�placer le loup vers cette position
            animator.SetBool("isWalking", true); // Activer l'animation de marche
            isMoving = true;
            waitTime = Random.Range(minWaitTime, maxWaitTime); // D�finir un nouveau temps d'attente
        }
    }
}
