using System;
using System.Collections;
using UnityEngine;

public static class Helper
{
    public static IEnumerator MoveToTarget(this RectTransform rectTransform, float duration, Vector2 target)
    {
        Vector2 startPosition = rectTransform.anchoredPosition;

        float progress = 0;

        while (progress < 1)
        {
            progress += Time.deltaTime / duration;
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, target, progress * progress);
            yield return null;
        }
    }

    public static IEnumerator MoveToTarget(this RectTransform rectTransform, float duration, Vector2 target, Action action)
    {
        Vector2 startPosition = rectTransform.anchoredPosition;

        float progress = 0;

        while (progress < 1)
        {
            progress += Time.deltaTime / duration;
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition,target, progress * progress);
            yield return null;
        }

        action?.Invoke();
    }

    public static IEnumerator Delay(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}
