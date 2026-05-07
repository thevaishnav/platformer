using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private Character _character;
    [SerializeField] private Transform _endPoint;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _character.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        Instance._character.gameObject.SetActive(true);
    }

    public void GameOver(int currentScore)
    {
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }

        var percentage = (100f * currentScore) / Character.GetScoreFromPosition(_endPoint.position.x);
        var tease = GameOverPhrases.GetPhrase((int)percentage);
        _character.canMove = false;
        _gameOverScreen.Show(currentScore, bestScore, tease);
    }

    
}