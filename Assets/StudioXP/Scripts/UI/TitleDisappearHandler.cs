using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleDisappearHandler : MonoBehaviour
{
    private IEnumerator coroutine;

    public void DisappearAfterTime(float waitTime)
    {
        coroutine = WaitAndDisappear(waitTime);
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitAndDisappear(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }
}
