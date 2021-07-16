using UnityEngine;

public class AllSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _shoot;

    [SerializeField] private AudioSource _showBoost;
    [SerializeField] private AudioSource _takeBoost;

    [SerializeField] private AudioSource _bulletBrickWallHit;
    [SerializeField] private AudioSource _bulletIronWallHit;

    [SerializeField] private AudioSource _eagleDestroy;
    [SerializeField] private AudioSource _tankDestroy;

    [SerializeField] private AudioSource _gameOver;
    [SerializeField] private AudioSource _moveTank;
    [SerializeField] private AudioSource _notMoveTank;

    public AudioSource Shoot => _shoot;

    public AudioSource ShowBoost => _showBoost;

    public AudioSource TakeBoost => _takeBoost;

    public AudioSource BulletBrickWallHit => _bulletBrickWallHit;

    public AudioSource BulletIronWallHit => _bulletIronWallHit;

    public AudioSource EagleDestroy => _eagleDestroy;

    public AudioSource TankDestroy => _tankDestroy;

    public AudioSource GameOver => _gameOver;

    public AudioSource MoveTank => _moveTank;

    public AudioSource NotMoveTank => _notMoveTank;

    public static AllSounds Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Mute(bool isMute)
    {
        _shoot.mute = isMute;
        _showBoost.mute = isMute;
        _takeBoost.mute = isMute;
        _bulletBrickWallHit.mute = isMute;
        _bulletIronWallHit.mute = isMute;
        _eagleDestroy.mute = isMute;
        _tankDestroy.mute = isMute;
        _gameOver.mute = isMute;
        _moveTank.mute = isMute;
        _notMoveTank.mute = isMute;
    }
}
