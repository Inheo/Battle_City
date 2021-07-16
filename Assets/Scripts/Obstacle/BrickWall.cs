using UnityEngine;

public class BrickWall : Obstacle
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Bullet>(out Bullet bulet))
        {
            Destroy(gameObject);
        }
    }
}
