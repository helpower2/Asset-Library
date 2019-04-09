using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        objectPooler.InstantiateFromPool(prefab, this.transform)?.transform.SetParent(this.transform);
    }
}
