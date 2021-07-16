using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 5;

    [HideInInspector] public int Damage = 0;

    [HideInInspector] public TankType TypeBullet;

    private void Update()
    {
        transform.Translate(transform.up * _speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Tank>(out Tank tank))
        {
            if(tank.Type == TypeBullet)
            {
                return;
            }
            else if(tank.Type != TypeBullet)
            {
                ToBroke();
                tank.TakeDamage(Damage);
                ExplosionPool.Instance.ShowExplosion(TypeExplosion.Small, transform.position);
                return;
            }
        }

        if(collision.TryGetComponent<Bullet>(out Bullet bullet))
        {
            ToBroke();
            return;
        }

        ExplosionPool.Instance.ShowExplosion(TypeExplosion.Small, transform.position);
        ToBroke();
    }

    public void ToBroke()
    {
        gameObject.SetActive(false);
    }
}
