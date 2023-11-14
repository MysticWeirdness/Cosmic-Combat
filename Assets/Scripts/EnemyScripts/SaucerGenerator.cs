using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaucerGenerator : MonoBehaviour
{
    [SerializeField] private GameObject ufoPrefab;
    private float ufoTimerDuration = 15f;
    private void Start()
    {
        StartCoroutine("UFOTimer");
    }

    private IEnumerator UFOTimer()
    {
        yield return new WaitForSeconds(ufoTimerDuration);
        Destroy(Instantiate(ufoPrefab, transform.position, Quaternion.identity), 10f);
        StartCoroutine("UFOTimer");
    }
}
