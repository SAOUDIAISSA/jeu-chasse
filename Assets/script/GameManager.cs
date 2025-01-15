using UnityEngine;
using TMPro;  // Nécessaire pour TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // Instance singleton pour faciliter l'accès depuis d'autres scripts
    public TextMeshProUGUI scoreText;    // Référence à l'élément de texte du score
    public TextMeshProUGUI timerText;    // Référence à l'élément de texte du timer

    private int score = 0;               // Score du joueur
    private float timeRemaining = 1000f;   // Temps restant (en secondes)

    private void Awake()
    {
        // Si l'instance est déjà définie, on détruira ce nouvel objet
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;  // Définir cette instance comme singleton
        }
    }

    private void Update()
    {
        // Met à jour le timer
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;  // Réduit le temps restant à chaque frame
            UpdateTimerUI();
        }
    }

    // Fonction pour afficher le temps sous forme de mm:ss
    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Fonction pour ajouter des points au score
    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;  // Met à jour le texte du score
    }

    // Fonction pour réinitialiser le timer (par exemple, en cas de redémarrage de niveau)
    public void ResetTimer()
    {
        timeRemaining = 60f;
    }
}
