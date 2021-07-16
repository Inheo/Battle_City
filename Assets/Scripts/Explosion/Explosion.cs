using System;
using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Animator ExplosionAnimator { get; private set; }

    public TypeExplosion TypeAnimation = TypeExplosion.Small;

    private readonly string[] _triggerNames = new string[] { "SmallExplosion", "BigExplosion" };

    private void Awake()
    {
        ExplosionAnimator = GetComponent<Animator>();
    }
    public void Active()
    {
        ExplosionAnimator.SetTrigger(_triggerNames[(int)TypeAnimation]);
    }

    public void HideExplosion()
    {
        gameObject.SetActive(false);
    }
}

public enum TypeExplosion
{
    Small = 0,
    Big = 1
}
