using System;
using UnityEngine;

public class Foreground : MonoBehaviour
{
    [SerializeField] private RectTransform _topFG;
    [SerializeField] private RectTransform _bottomFG;
    [SerializeField] private GameObject _textObject;

    [SerializeField] private Vector2 _startPositionTopFG = new Vector2(0, 540);
    [SerializeField] private Vector2 _startPositionBottomFG = new Vector2(0, -540);

    public void Show()
    {
        HideOrShowTextObject(true);

        StartCoroutine(_topFG.MoveToTarget(0.3f, Vector2.zero));
        StartCoroutine(_bottomFG.MoveToTarget(0.3f, Vector2.zero));
    }

    public void Show(Action action)
    {
        action += () => HideOrShowTextObject(true);

        StartCoroutine(_topFG.MoveToTarget(0.3f, Vector2.zero, action));
        StartCoroutine(_bottomFG.MoveToTarget(0.3f, Vector2.zero));
    }

    public void Hide()
    {
        HideOrShowTextObject(false);

        StartCoroutine(_topFG.MoveToTarget(0.3f, _startPositionTopFG));
        StartCoroutine(_bottomFG.MoveToTarget(0.3f, _startPositionBottomFG));
    }
    public void Hide(Action action)
    {
        action += () => HideOrShowTextObject(false);

        StartCoroutine(_topFG.MoveToTarget(0.3f, _startPositionTopFG, action));
        StartCoroutine(_bottomFG.MoveToTarget(0.3f, _startPositionBottomFG));
    }

    public void HideOrShowTextObject(bool active)
    {
        _textObject.SetActive(active);
    }
}
