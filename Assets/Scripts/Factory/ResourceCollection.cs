using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollection : MonoBehaviour
{
    public float collectionRate;
    public float collectionWeight;

    private float collection;
    // Start is called before the first frame update
    void Start()
    {
        collection = collectionRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (collection <= 0)
        {
            ResourcePool.AddResource(collectionWeight);
            collection = collectionRate;
        }
        else
        {
            collection -= Time.deltaTime;
        }
    }
}