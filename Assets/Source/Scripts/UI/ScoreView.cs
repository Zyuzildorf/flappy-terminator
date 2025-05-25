using TMPro;
using UnityEngine;

public class ScoreView : Window
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _bestScoreText;

    private void OnEnable()
    {
        _scoreCounter.ScoreValueChanged += UpdateScoreText;
    }

    private void OnDisable()
    {
        _scoreCounter.ScoreValueChanged -= UpdateScoreText;
    }

    private void Start()
    {
        UpdateScoreText();
    }

    public void ShowCurrentScore()
    {
        _scoreText.gameObject.SetActive(true);
        UpdateScoreText();
    }

    public void HideCurrentScore()
    {
        _scoreText.gameObject.SetActive(false);
    }

    public void ShowBestScore()
    {
        _bestScoreText.gameObject.SetActive(true);
        UpdateBestScoreText();
    }

    public void HideBestScore()
    {
        _bestScoreText.gameObject.SetActive(false);
    }

    public void Reset()
    {
        _scoreCounter.ResetScore();
    }

    private void UpdateScoreText()
    {
        _scoreText.text = _scoreCounter.ScoreValue.ToString();
    }

    private void UpdateBestScoreText()
    {
        _bestScoreText.text = new string("Best score: " + _scoreCounter.BestScoreValue);
    }

}
