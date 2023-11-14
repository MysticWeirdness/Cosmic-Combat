using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saucer : MonoBehaviour
{
    [SerializeField] private GameObject ufoPrefab;
    private float ufoTimerDuration = 30f;
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
