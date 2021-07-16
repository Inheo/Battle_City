using System;
using System.Collections;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    [SerializeField] private Sprite _destroySprite;

    private bool isBroken = false;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Bullet>(out Bullet bullet) && !isBroken)
        {
            isBroken = true;
            spriteRenderer.sprite = _destroySprite;
            AllSounds.Instance.EagleDestroy.Play();
            AllSounds.Instance.GameOver.Play();

            StartCoroutine(Helper.Delay(AllSounds.Instance.GameOver.clip.length, Game.Instance.onGameOver));
        }
    }
}
