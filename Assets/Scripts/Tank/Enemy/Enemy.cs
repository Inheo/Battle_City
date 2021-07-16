using UnityEngine;

public class Enemy : Tank
{
    [SerializeField] private int _costOfTankForDestruction;

    [SerializeField] private TypeEnemy _typeEnemy = TypeEnemy.First;

    public SpawnerEnemy SpawnerEnemy;

    public int CostOfTankForDestruction => _costOfTankForDestruction;

    public static bool FreezeEnemy = false;

    private void Start()
    {
        SpawnerEnemy.onDieAllLiveEnemy += Die;
    }

    private void Update()
    {
        if (ElapsedTimeAfterLastShot <= 0 && !FreezeEnemy)
        {
            Attack();
            ElapsedTimeAfterLastShot = RechargeTime;
        }

        ElapsedTimeAfterLastShot -= Time.deltaTime;
    }

    protected override void Die()
    {
        SpawnerEnemy.onDieAllLiveEnemy -= Die;

        SpawnerEnemy.SpawnTankDied(this);
        Game.Instance.ShowAddScoreText(_costOfTankForDestruction, transform.position, _typeEnemy);
        base.Die();
    }
}

public enum TypeEnemy
{ 
    First,
    Second,
    Third,
    Fourth
}
