using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private UiAnimation _animation;
    [SerializeField] private TMP_Text _tease;
    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private TMP_Text _bestScoreText;
    [SerializeField] private Button _restartButton;
    
    private void Awake()
    {
        _restartButton.onClick.AddListener(RestartGame);
    }

    private void OnDestroy()
    {
        _restartButton.onClick.RemoveListener(RestartGame);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    [ContextMenu("Show Game Over Screen")]
    private void ShowGameOverScreenForTesting()
    {
        Show(123, 456, "bruh");
    }

    public void Show(int currentScore, int bestScore, string tease)
    {
        gameObject.SetActive(true);
        StartCoroutine(_animation.PlayOnAnimation());
        _currentScoreText.text = currentScore.ToString();
        _bestScoreText.text = bestScore.ToString();
        _tease.text = tease;
    }
    
    [ContextMenu("Hide")]
    public void Hide()
    {
        StartCoroutine(_animation.PlayOffAnimation(() => gameObject.SetActive(false)));
    }
}
