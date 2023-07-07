using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coins"))
        {
            Collect();
        }
    }

    public void Collect()
    {
        GameManager.Instance.CollectObject();
    }
}

