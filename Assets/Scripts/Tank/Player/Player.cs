using UnityEngine;
using UnityEngine.UI;

public class Player : Tank
{
    [SerializeField] private int _extraLife = 2;

    [SerializeField] private KeyCode _attackKey;

    [SerializeField] private GameObject _shieldObject;
    [SerializeField] private GameObject _radiance;

    [Space]
    [SerializeField] private Text _extraLifeText;
    [SerializeField] private GameObject _playerStatsBlock;

    private bool _isImmortal = true;
    private bool canShoot = true;

    private Vector2 _startPosition;

    private void OnEnable()
    {
        canShoot = true;
        _startPosition = transform.position;

        _extraLifeText.text = _extraLife.ToString();
        _playerStatsBlock.SetActive(true);

        TurnOnImmortalityMode();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_attackKey) && ElapsedTimeAfterLastShot <= 0 && canShoot)
        {
            Attack();
            ElapsedTimeAfterLastShot = RechargeTime;
        }

        ElapsedTimeAfterLastShot -= Time.deltaTime;
    }


    private void TurnOnImmortalityMode()
    {
        _shieldObject.SetActive(true);
        _isImmortal = true;
        StartCoroutine(Helper.Delay(3f, () => { TurnOffImmortalityMode(); }));
    }

    private void TurnOffImmortalityMode()
    {
        _shieldObject.SetActive(false);
        _isImmortal = false;
    }

    private void UseExtraLife()
    {
        canShoot = false;

        AllSounds.Instance.TankDestroy.Play();

        _extraLife--;
        _extraLifeText.text = _extraLife.ToString();

        _radiance.transform.position = _startPosition;
        _radiance.SetActive(true);
        transform.localScale = Vector3.zero;

        StartCoroutine(Helper.Delay(2f, () =>
        {
            TurnOnImmortalityMode();

            _radiance.SetActive(false);

            transform.position = _startPosition;
            transform.localScale = Vector3.one;

            canShoot = true;
        }));
    }

    protected override void Die()
    {
        if (_extraLife > 0)
        {
            UseExtraLife();
            return;
        }

        base.Die();
        Game.Instance.DeadPlayer();
    }

    public override void TakeDamage(int damage)
    {
        if (!_isImmortal)
        {
            base.TakeDamage(damage);
        }
    }

    public override void Attack()
    {
        base.Attack();
        AllSounds.Instance.Shoot.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Boost>(out Boost boost))
        {
            AllSounds.Instance.TakeBoost.Play();
            boost.gameObject.SetActive(false);
            Game.Instance.ShowAddScoreText(500, collision.transform.position);

            if (boost.Type == BoostType.Grenade)
            {
                Game.Instance.DieAllLiveEnemy();
            }
            else if(boost.Type == BoostType.Immortal)
            {
                TurnOnImmortalityMode();
            }
            else if(boost.Type == BoostType.FreezeEnemy)
            {
                Enemy.FreezeEnemy = true;
                StartCoroutine(Helper.Delay(5f, () => Enemy.FreezeEnemy = false));
            }
        }
    }
}
