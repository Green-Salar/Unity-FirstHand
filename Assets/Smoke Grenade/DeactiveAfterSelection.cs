using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveAfterSelection : MonoBehaviour
{
    public void StartDeActivePeriod(float t)
    {
        StartCoroutine(DeActiveAfterSelection(t));
    }


    // Start is called before the first frame update
     IEnumerator DeActiveAfterSelection(float t)
    {
        yield return new WaitForSeconds(t);
        gameObject.SetActive(false);

    }
}
