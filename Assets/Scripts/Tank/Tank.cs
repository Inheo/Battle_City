using UnityEngine;

public abstract class Tank : MonoBehaviour
{
    [SerializeField] protected int HealthPoint = 1;
    [SerializeField] protected int Damage = 1;

    [SerializeField] protected float RechargeTime = 0.3f;
    protected float ElapsedTimeAfterLastShot;

    [SerializeField] protected Transform StartPointForBullet;

    [SerializeField] private TankType _type = TankType.Enemy;

    private int StartHealthPoint;

    public TankType Type => _type;

    private void Start()
    {
        StartHealthPoint = HealthPoint;
    }

    public virtual void TakeDamage(int damage)
    {
        HealthPoint -= damage;

        if(HealthPoint <= 0)
        {
            Die();
        }
    }

    public virtual void Attack()
    {
        Bullet bullet = BulletPool.Instance.PoolBullet.GetFreeElement();

        bullet.TypeBullet = Type;
        bullet.transform.localRotation = transform.localRotation;
        bullet.transform.position = StartPointForBullet.position;
        bullet.Damage = Damage;
    }

    protected virtual void Die()
    {
        ExplosionPool.Instance.ShowExplosion(TypeExplosion.Big, transform.position);
        Destroy(gameObject);
        AllSounds.Instance.TankDestroy.Play();
    }
}

public enum TankType
{
    Enemy,
    Player
}
