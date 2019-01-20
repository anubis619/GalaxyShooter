using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelay : MonoBehaviour
{
    [SerializeField]
    private float delay = 2f;

    private void Start()
    {
        StartCoroutine(DestroyAfter(delay));
    }

    public void DestroyObject()
    {
        StartCoroutine(DestroyAfter(delay));
    }

    public void DestroyObject(float delay)
    {
        StartCoroutine(DestroyAfter(delay));
    }

    IEnumerator DestroyAfter(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
