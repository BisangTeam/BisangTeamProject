using UnityEngine;
using DG.Tweening;

public class Heartbeat : MonoBehaviour
{
    [Header("첫 번째 박동 (작게 쿵)")]
    [SerializeField] private float firstScale = 1.15f;
    [SerializeField] private float firstDuration = 0.1f;

    [Header("두 번째 박동 (크게 쾅)")]
    [SerializeField] private float secondScale = 1.3f;
    [SerializeField] private float secondDuration = 0.12f;

    [Header("쉬는 시간")]
    [SerializeField] private float restTime = 0.5f;

    private void Start()
    {
        PlayHeartbeat();
    }

    private void PlayHeartbeat()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(firstScale, firstDuration));
        sequence.Append(transform.DOScale(1f, firstDuration));

        sequence.Append(transform.DOScale(secondScale, secondDuration));
        sequence.Append(transform.DOScale(1f, secondDuration));

        sequence.AppendInterval(restTime);

        sequence.SetLoops(-1);
        sequence.SetLink(gameObject);
    }
}