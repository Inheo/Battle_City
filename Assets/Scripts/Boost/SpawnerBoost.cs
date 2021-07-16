using System.Collections;
using UnityEngine;

public class SpawnerBoost : MonoBehaviour
{
    [SerializeField] private Boost _boost;

    [SerializeField] private Sprite[] _boostIcons;

    private void Start()
    {
        _boost.gameObject.SetActive(false);
        StartCoroutine(ShowBoost());
    }

    private IEnumerator ShowBoost()
    {
        var delay = new WaitForSeconds(15f);
        yield return delay;

        int randomBoost = Random.Range(0, System.Enum.GetNames(typeof(BoostType)).Length);

        _boost.transform.position = new Vector2(Random.Range(-Game.Instance.MapSize.x, Game.Instance.MapSize.x), Random.Range(-Game.Instance.MapSize.y, Game.Instance.MapSize.y));
        _boost.Type = (BoostType)randomBoost;
        _boost.SpriteRenderer.sprite = _boostIcons[randomBoost];
        _boost.gameObject.SetActive(true);

        AllSounds.Instance.ShowBoost.Play();

        StartCoroutine(ShowBoost());
    }
}
public enum BoostType
{
    Grenade,
    Immortal,
    FreezeEnemy
}

