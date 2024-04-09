using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleDisappearAuto : MonoBehaviour
{
    void Start()
    {
        GetComponent<TitleDisappearHandler>().DisappearAfterTime(5.0f);
    }
}




