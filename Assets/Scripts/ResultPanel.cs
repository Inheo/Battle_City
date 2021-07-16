using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private Text _countPlayers;
    [SerializeField] private Text _totalPointsText;
    [SerializeField] private Text[] _pointsForDeadEnemyTexts;
    [SerializeField] private Text[] _countDeadEnemyTexts;
    [SerializeField] private Text _totalDeadPointsText;


    private bool _canExit = false;

    private void Update()
    {
        if(gameObject.activeInHierarchy && _canExit && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void Show(int countPlayers, int totalPoints, int[] countDeadEnemy)
    {
        _canExit = false;

        gameObject.SetActive(true);
        _countPlayers.text = (countPlayers + 1).ToString() + (countPlayers == 0 ? " PLAYER" : " PLAYERS");
        _totalPointsText.text = totalPoints.ToString();

        StartCoroutine(CountingDeadTanks(countDeadEnemy));
    }

    private IEnumerator CountingDeadTanks(int[] countDeadEnemy)
    {
        var delay = new WaitForSeconds(0.3f);
        int startPriceForTank = 100;
        int total = 0;

        for (int i = 0; i < _pointsForDeadEnemyTexts.Length; i++)
        {
            _countDeadEnemyTexts[i].gameObject.SetActive(true);
            _pointsForDeadEnemyTexts[i].gameObject.SetActive(true);

            total += (i + 1) * startPriceForTank * countDeadEnemy[i];

            for (int j = 0; j <= countDeadEnemy[i]; j++)
            {
                _countDeadEnemyTexts[i].text = j.ToString();
                _pointsForDeadEnemyTexts[i].text = (j * (i + 1) * startPriceForTank).ToString();
                yield return delay;
            }
        }

        yield return delay;
        _totalDeadPointsText.gameObject.SetActive(true);
        _totalDeadPointsText.text = total.ToString();
        _canExit = true;
    }
}
