using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private float punchScale = 0.2f;
    [SerializeField] private float punchDuration = 0.15f;
    private Tween punchTween;
    private void Awake()
    {
        GameManager.OnGoldChanged += OnGoldChanged;
    }

    private void OnGoldChanged(int gold)
    {
        coinText.text = gold.ToString();
        punchTween?.Kill();
        punchTween = coinText.transform.DOPunchScale(Vector3.one * punchScale, punchDuration, vibrato: 6, elasticity: 0.5f).OnComplete(()=>coinText.transform.localScale = Vector3.one);
    }

    private void OnDestroy()
    {
        GameManager.OnGoldChanged -= OnGoldChanged;
    }
}
