using System.Collections;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField] protected float Speed = 2;
    [SerializeField] protected float TimeBetweenFrameChanges = 0.2f;

    [SerializeField] protected Sprite[] SpriteForAnimation;

    protected SpriteRenderer SpriteRendererTank;

    private void Start()
    {
        SpriteRendererTank = GetComponent<SpriteRenderer>();

        StartCoroutine(MoveAnimation(TimeBetweenFrameChanges));
    }

    protected readonly Vector2 Border = new Vector2(6f, 6f);

    public bool IsMove { get; protected set; }

    protected virtual void Move()
    {
        transform.Translate(transform.up * Speed * Time.fixedDeltaTime, Space.World);

        IsMove = true;
    }

    protected void Rotate(float eulerAngle)
    {
        transform.localRotation = Quaternion.Euler(0, 0, eulerAngle);
    }

    protected IEnumerator MoveAnimation(float speed)
    {
        int indexSprite = 0;
        var delay = new WaitForSeconds(speed);
        while (true)
        {
            if(IsMove)
            {
                indexSprite++;
                indexSprite = indexSprite >= SpriteForAnimation.Length ? 0 : indexSprite;
                SpriteRendererTank.sprite = SpriteForAnimation[indexSprite];
                yield return delay;
            }

            yield return null;
        }
    }
}
