using UnityEngine;
using TMPro;  // N�cessaire pour TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // Instance singleton pour faciliter l'acc�s depuis d'autres scripts
    public TextMeshProUGUI scoreText;    // R�f�rence � l'�l�ment de texte du score
    public TextMeshProUGUI timerText;    // R�f�rence � l'�l�ment de texte du timer

    private int score = 0;               // Score du joueur
    private float timeRemaining = 1000f;   // Temps restant (en secondes)

    private void Awake()
    {
        // Si l'instance est d�j� d�finie, on d�truira ce nouvel objet
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;  // D�finir cette instance comme singleton
        }
    }

    private void Update()
    {
        // Met � jour le timer
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;  // R�duit le temps restant � chaque frame
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
        scoreText.text = "Score: " + score;  // Met � jour le texte du score
    }

    // Fonction pour r�initialiser le timer (par exemple, en cas de red�marrage de niveau)
    public void ResetTimer()
    {
        timeRemaining = 60f;
    }
}
