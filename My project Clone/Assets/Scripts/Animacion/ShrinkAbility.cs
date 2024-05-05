using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShrinkAbility : MonoBehaviour, IAbility
{
    //public float scaleFactor = 0.2f;
    public float duration = 2.5f;

    //private Vector3 startScale;
    private Vector3 startPosition;
    //private bool started = false;
    public Transform targetTransform;
    public float distance;
    private bool isAnimating = false;

    void Start()
    {
        //startScale = transform.localScale;
        startPosition = transform.localPosition;
    }
    public void Execute()
    {
        //if (started) return;
        //started = true;
        //Sequence sequence = DOTween.Sequence();
        //sequence.Append(transform.DOScale(startScale * scaleFactor, 0.3f));
        //sequence.Append(transform.DOScale(startScale, 0.3f));

        //transform.DOScale(startScale * scaleFactor, 0.3f).OnComplete(() => transform.DOScale(startScale, 0.3f));
    }
    private void Update()
    {
        if (targetTransform != null)
        {
            distance = Vector3.Distance(transform.position, targetTransform.position);
            if (distance <= 3.6f)
            {
                if (!isAnimating)
                {
                    StartFall();
                    isAnimating = true;
                }
            }
            else
            {
                isAnimating = false;
            }
        }
    }

    private void StartFall()
    {
        isAnimating = true;

        Sequence blockSequence = DOTween.Sequence();
        blockSequence.Append(transform.DOMove(new Vector3(startPosition.x, 0.5f, startPosition.z), duration))
                    .SetEase(Ease.OutCirc);
        blockSequence.Append(transform.DOMove(new Vector3(startPosition.x, 3.5f, startPosition.z), duration))
                    .SetEase(Ease.OutCirc)
                    .OnComplete(() => isAnimating = false);
    }
    public void FindGameObjectWithTag(string tag)
    {
        targetTransform = GameObject.FindGameObjectWithTag(tag).transform;
    }
}
