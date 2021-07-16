using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    public System.Action onDieAllLiveEnemy;

    [SerializeField] private int _maxLiveTanks = 4;

    [SerializeField] private Enemy[] _enemyPrefabs;

    [SerializeField] private GameObject _radianceObject;
    [SerializeField] private SpawnerUITanks _spawnerUITanks;

    private bool canSpawn;

    private Vector2[] _spawnPoints = new Vector2[] { new Vector2(-6, 6), new Vector2(-4, 6), new Vector2(-2, 6), new Vector2(0, 6), new Vector2(2, 6), new Vector2(4, 6), new Vector2(6, 6) };

    private List<Enemy> _allLiveEnemy = new List<Enemy>();

    private int _numberOfLiveTanks = 0;
    private int _countSpawnTanks;

    public int NumberOfLiveTanks => _numberOfLiveTanks;
    public int CountSpawnTanks => _countSpawnTanks;

    private void Start()
    {
        canSpawn = true;
        StartCoroutine(Helper.Delay(1f, () => StartCoroutine(Spawn())));
    }

    private IEnumerator Spawn()
    {
        if (canSpawn && _countSpawnTanks < Game.Instance.MaxSpawnTanks)
        {
            canSpawn = false;

            var delay = new WaitForSeconds(2);

            Vector2 position = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

            _spawnerUITanks.RemoveUITankElement();

            _radianceObject.transform.position = position;
            _radianceObject.SetActive(true);

            yield return delay;

            var enemy = Instantiate(_enemyPrefabs[RandomTank()]);
            enemy.transform.position = position;
            enemy.SpawnerEnemy = this;

            _allLiveEnemy.Add(enemy);

            _numberOfLiveTanks++;
            _countSpawnTanks++;
            _radianceObject.SetActive(false);

            yield return delay;

            canSpawn = true;

            if (_numberOfLiveTanks < _maxLiveTanks)
            {
                StartCoroutine(Spawn());
            }
        }
    }

    private int RandomTank()
    {
        float rand = Random.Range(0f, 1f);
        if(rand < 0.50f)
        {
            return 0;
        }
        else if(rand < 0.75f)
        {
            return 1;
        }
        else if(rand < 0.9f)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    public void SpawnTankDied(Enemy enemy)
    {
        _numberOfLiveTanks--;
        _allLiveEnemy.Remove(enemy);
        StartCoroutine(Spawn());
    }
}
