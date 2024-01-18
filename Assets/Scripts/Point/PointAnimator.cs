using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAnimator : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float minWaitTime;
    [SerializeField] private float maxWaitTime;
    [SerializeField] private string[] nameTriggerAnimations;
    [SerializeField] private int indexStartAnim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

    }

    private void Start()
    {
        anim.SetTrigger(nameTriggerAnimations[indexStartAnim]);
        StartCoroutine(RandomAnimation());
    }

    IEnumerator RandomAnimation()
    {
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        anim.SetTrigger(nameTriggerAnimations[Random.Range(0, nameTriggerAnimations.Length)]);
        Repeat();

    }
    void Repeat()
    {
        StartCoroutine(RandomAnimation());
    }
}
