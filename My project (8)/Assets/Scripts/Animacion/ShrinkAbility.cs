using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShrinkAbility : MonoBehaviour, IAbility
{
    public float duration = 2.5f;
    public Transform targetTransform;
    public List<Transform> targetTransforms = new List<Transform>();
    public float distance;
    public GameObject[] players;

    private Vector3 startPosition;
    private bool isAnimating = false;

    void Start()
    {
        //startScale = transform.localScale;
        startPosition = transform.localPosition;
        StartCoroutine(FindTargetTransform());
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

    }

    IEnumerator FindTargetTransform()
    {
        while (true)
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


            players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                if (!targetTransforms.Contains(player.transform))
                {
                    targetTransforms.Add(player.transform);
                }

            }

            if (targetTransforms.Count > 1 && targetTransforms[0] != null && targetTransforms[1] != null)
            {
                if (Vector3.Distance(transform.position, targetTransforms[0].position) > Vector3.Distance(transform.position, targetTransforms[1].position))
                {
                    targetTransform = targetTransforms[1];
                }
                else if (Vector3.Distance(transform.position, targetTransforms[0].position) < Vector3.Distance(transform.position, targetTransforms[1].position))
                {
                    targetTransform = targetTransforms[0];
                }
            }
            else if (targetTransforms[0] != null)
            {
                targetTransform = targetTransforms[0];
            }

            yield return new WaitForSeconds(1);
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
    public void FindGameObjectWithName(string name)
    {
        targetTransform = GameObject.Find(name).transform;
    }
}
