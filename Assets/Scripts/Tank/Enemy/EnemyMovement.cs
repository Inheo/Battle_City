using System.Collections;
using UnityEngine;

public class EnemyMovement : TankMovement
{
    private int[] _zEulerAngles = new int[] { 180, 270, 0, 90 };

    private void Start()
    {
        StartCoroutine(RandomRotate());
    }

    private void FixedUpdate()
    {
        if (!Enemy.FreezeEnemy)
        {
            Move();
        }
    }

    private IEnumerator RandomRotate()
    {
        while(true)
        {
            if (!Enemy.FreezeEnemy)
            {
                transform.localRotation = Quaternion.Euler(0, 0, _zEulerAngles[Random.Range(0, _zEulerAngles.Length)]);
                yield return new WaitForSeconds(Random.Range(2f, 7f));
            }
            else
            {
                yield return null;
            }
        }
    }
}
