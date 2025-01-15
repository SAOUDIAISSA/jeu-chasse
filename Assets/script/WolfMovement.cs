using UnityEngine;
using UnityEngine.AI; // Nécessaire pour le NavMeshAgent

public class WolfMovement : MonoBehaviour
{
    public NavMeshAgent agent;         // Référence au NavMeshAgent
    public Animator animator;          // Référence à l'Animator
    public float minWaitTime = 2f;     // Temps minimum avant de changer de destination
    public float maxWaitTime = 5f;     // Temps maximum avant de changer de destination
    public float range = 10f;          // Distance dans laquelle le loup peut se déplacer

    private bool isMoving = false;     // Vérifie si le loup se déplace
    private float waitTime;            // Temps d'attente avant le prochain mouvement
    private float waitTimer = 0f;      // Chronomètre pour attendre avant de bouger
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

        // Vérifier si le loup est en train de marcher
        if (agent.remainingDistance <= agent.stoppingDistance && isMoving)
        {
            isMoving = false;
            animator.SetBool("isWalking", false); // Revenir à l'état Idle
            waitTimer = 0f; // Réinitialiser le chronomètre
        }

        // Chronomètre pour changer de destination
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
        // Trouver une position aléatoire
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection += transform.position; // Ajouter la position actuelle pour obtenir une zone proche

        // Vérifier si la position est sur le NavMesh
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, range, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position); // Déplacer le loup vers cette position
            animator.SetBool("isWalking", true); // Activer l'animation de marche
            isMoving = true;
            waitTime = Random.Range(minWaitTime, maxWaitTime); // Définir un nouveau temps d'attente
        }
    }
}
