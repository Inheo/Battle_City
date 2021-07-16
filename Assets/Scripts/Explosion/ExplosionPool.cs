using UnityEngine;

public class ExplosionPool : MonoBehaviour
{
    [SerializeField] private int _poolCount = 10;
    [SerializeField] private Explosion _explosionPrefab;

    private PoolMono<Explosion> _poolExplosion;

    public PoolMono<Explosion> PoolExplosion => _poolExplosion;

    public static ExplosionPool Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _poolExplosion = new PoolMono<Explosion>(_explosionPrefab, _poolCount, transform);
    }
    public void ShowExplosion(TypeExplosion typeExplosion, Vector2 position)
    {
        var explosion = PoolExplosion.GetFreeElement();
        explosion.TypeAnimation = typeExplosion;
        explosion.transform.position = position;
        explosion.Active();
    }
}
