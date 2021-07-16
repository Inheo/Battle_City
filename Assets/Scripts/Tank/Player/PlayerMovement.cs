using UnityEngine;

public class PlayerMovement : TankMovement
{
    private enum Players
    {
        FirstPlayer,
        SecondPlayer
    }

    [SerializeField] private Players _selectedPlayer;

    [Space]
    [SerializeField] private KeyCode _upKey;
    [SerializeField] private KeyCode _rightKey;
    [SerializeField] private KeyCode _downKey;
    [SerializeField] private KeyCode _leftKey;

    void FixedUpdate()
    {
        ControlSelectedPlayer();
    }

    private void ControlSelectedPlayer()
    {
        if (Input.GetKey(_upKey))
        {
            Rotate(0);
            Move();
        }
        else if (Input.GetKey(_rightKey))
        {
            Rotate(270);
            Move();
        }
        else if (Input.GetKey(_downKey))
        {
            Rotate(180);
            Move();
        }
        else if (Input.GetKey(_leftKey))
        {
            Rotate(90);
            Move();
        }
        else
        {
            NotMove();
        }
    }

    protected override void Move()
    {
        base.Move();
        IsMove = true;
        if (!AllSounds.Instance.MoveTank.isPlaying)
        {
            AllSounds.Instance.MoveTank.Play();
            AllSounds.Instance.NotMoveTank.Stop();
        }
    }

    private void NotMove()
    {
        IsMove = false;
        if (!AllSounds.Instance.NotMoveTank.isPlaying)
        {
            AllSounds.Instance.MoveTank.Stop();
            AllSounds.Instance.NotMoveTank.Play();
        }
    }
}
