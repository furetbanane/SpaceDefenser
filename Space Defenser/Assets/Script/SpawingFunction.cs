using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SpawingFunction : MonoBehaviour
{
    [AssetsOnly]
    [SerializeField]
    private GameObject prefabToGenerate;

    public void Generate()
    {
        GameObject prefabGenerated = Instantiate(prefabToGenerate, transform);
        prefabGenerated.transform.localPosition = new Vector3(0,0,0);
    }
}
