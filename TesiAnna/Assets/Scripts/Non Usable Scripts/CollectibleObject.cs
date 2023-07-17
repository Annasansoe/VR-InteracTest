using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CollectibleObject : MonoBehaviour
{

    public XRGrabInteractable XRGrabInteractable;

    private void Start()
    {
        XRGrabInteractable = GetComponent<XRGrabInteractable>();
    }

}

