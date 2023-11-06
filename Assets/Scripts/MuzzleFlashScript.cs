using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashScript : MonoBehaviour
{
    private IEnumerator SelfDestructSequence()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
