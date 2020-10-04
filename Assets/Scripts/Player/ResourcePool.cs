using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePool : MonoBehaviour
{
    private static float resource = 10;
    public Text resourceDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        resourceDisplay.text = "$" + resource;
    }

    public static float GetResources() { return resource; }
    public static void AddResource(float collection) {   resource += collection; }

    public static bool SpendResources(float cost)
    {
        if (cost > resource)
        {
            return false;
        }
        else
        {
            resource -= cost;
            return true;
        }
    }
}
