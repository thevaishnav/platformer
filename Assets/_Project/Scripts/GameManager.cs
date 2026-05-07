using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private Character _character;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
        DontDestroyOnLoad(gameObject);
        _character.gameObject.SetActive(false);
    }

    public static void StartGame()
    {
        _instance._character.gameObject.SetActive(true);
    }
    
    public static void GameOver(int currentScore)
    {
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
        
        _instance._character.canMove = false;
        _instance._gameOverScreen.Show(currentScore, bestScore);
    }
}
