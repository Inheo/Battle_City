using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private RectTransform _cursor;
    [SerializeField] private RectTransform _menuRectTransform;

    [SerializeField] private Foreground _foreground;

    private bool _canChoose = false;

    private void Start()
    {
        TemporaryVariables.CountPlayers = CountPlayers.OnePlayer;

        _canChoose = false;
        _cursor.gameObject.SetActive(false);


        StartCoroutine
            ( _menuRectTransform.MoveToTarget(4f, Vector2.zero, () =>
                {
                    _canChoose = true;
                    _cursor.gameObject.SetActive(true);
                })
            );
    }

    void Update()
    {
        if (!_canChoose)
        { 
            return;
        }

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _cursor.anchoredPosition = new Vector2(_cursor.anchoredPosition.x, 60);
            TemporaryVariables.CountPlayers = CountPlayers.OnePlayer;
        }

        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _cursor.anchoredPosition = new Vector2(_cursor.anchoredPosition.x, -60);
            TemporaryVariables.CountPlayers = CountPlayers.TwoPlayers;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            _canChoose = false;
            _foreground.Show(() =>
            {
                StartCoroutine(Helper.Delay(3f, () => SceneManager.LoadScene("Game") ));
            });
        }
    }
}
