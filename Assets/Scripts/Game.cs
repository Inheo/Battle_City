using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private int _maxSpawnTanks;

    [SerializeField] private Text _addScoreText;
    [SerializeField] private SpawnerEnemy _spawnerEnemy;
    [SerializeField] private ResultPanel _resultPanel;

    [SerializeField] private Foreground _foreground;

    [SerializeField] private GameObject[] _players;

    private int[] _countsDeadEnemy = new int[Enum.GetNames(typeof(TypeEnemy)).Length];
    private int _score;
    private int _countLivePlayers = 0;

    public Text AddScoreText => _addScoreText;

    public Action onGameOver;
    public Action onGameWin;

    public static Game Instance;

    public Vector2 MapSize = new Vector2(6, 6);

    public int MaxSpawnTanks => _maxSpawnTanks;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(Helper.Delay(0.5f, () => _foreground.Hide()));
        onGameOver += RestartGame;
        onGameWin += Win;

        for (int i = 0; i <= (int)TemporaryVariables.CountPlayers; i++)
        {
            _players[i].SetActive(true);
        }

        _countLivePlayers = (int)TemporaryVariables.CountPlayers + 1;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DeadPlayer()
    {
        _countLivePlayers--;
        if(_countLivePlayers <= 0)
        {
            onGameOver?.Invoke();
        }
    }

    public void ShowAddScoreText(int cost, Vector2 position, TypeEnemy type)
    {
        _countsDeadEnemy[(int)type]++;

        _score += cost;

        _addScoreText.enabled = true;

        _addScoreText.text = cost.ToString();
        _addScoreText.transform.position = position;

       StartCoroutine(Helper.Delay(0.7f, () => { _addScoreText.enabled = false; }));

        if(_spawnerEnemy.CountSpawnTanks >= _maxSpawnTanks && _spawnerEnemy.NumberOfLiveTanks == 0)
        {
            onGameWin?.Invoke();
        }
    }

    public void ShowAddScoreText(int cost, Vector2 position)
    {
        _score += cost;

        _addScoreText.enabled = true;

        _addScoreText.text = cost.ToString();
        _addScoreText.transform.position = position;

       StartCoroutine(Helper.Delay(0.7f, () => { _addScoreText.enabled = false; }));
    }

    private void Win()
    {
        StartCoroutine(Helper.Delay(2f, () =>
        {
            _resultPanel.Show((int)TemporaryVariables.CountPlayers, _score, _countsDeadEnemy);
            AllSounds.Instance.Mute(true);
        }));
    }

    public void DieAllLiveEnemy()
    {
        _spawnerEnemy.onDieAllLiveEnemy?.Invoke();
    }

    private void OnDestroy()
    {
        onGameOver -= RestartGame;
        onGameWin -= Win;
    }
}
