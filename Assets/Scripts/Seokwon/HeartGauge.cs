using System;
using DG.Tweening;
using UnityEngine;

public class HeartGauge : MonoBehaviour
{
    [SerializeField] private Transform bldtrm;

    [Range(0f, 100f)] [SerializeField] private float gauge;

    [SerializeField] private float emptyPosY = -0.785f;
    [SerializeField] private float fullPosY = 0.0f;

    [SerializeField] private float fillduration = 0.4f;
    [SerializeField] private Ease fillEase = Ease.OutQuad;


    private void Start()
    {
        UpdateGauge(gauge);
    }

    private void OnValidate()
    {
        if (bldtrm != null && Application.isPlaying)
        {
            UpdateGauge(gauge);
        }
    }

    private void UpdateGauge(float newguage)
    {
        gauge = Mathf.Clamp(newguage, 0, 100);
        float chg = gauge / 100f;
        float targetY = Mathf.Lerp(emptyPosY, fullPosY, chg);
        bldtrm.DOKill();
        bldtrm.DOLocalMoveY(targetY, fillduration).SetEase(fillEase).SetLink(bldtrm.gameObject);   
    }

    private void OnDestroy()
    {
        bldtrm.DOKill();
    }
}
