using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class RayInteractorControl : MonoBehaviour
{
    AppMenuManager appMenuManagerInstance;

    public ActionBasedController leftController;
    public ActionBasedController rightController;

    public XRDirectInteractor directInteractorLeft;
    public XRRayInteractor rayInteractorLeft;
    public XRDirectInteractor directInteractorRight;
    public XRRayInteractor rayInteractorRight;

    public TMP_Text prova;

    private void Start()
    {
        // Subscribe to the dropdown's OnValueChanged event to detect changes in the selection
        //if (DialogueBox.StringMetaphor.Equals("DG"))
        {
            rayInteractorLeft.enabled = false;
            rayInteractorRight.enabled = false;
            directInteractorLeft.enabled = true;
            directInteractorRight.enabled = true;
        }
       // else if (DialogueBox.StringMetaphor.Equals("RC"))
        {
            directInteractorRight.enabled = false;
            rayInteractorRight.enabled = true;
            directInteractorLeft.enabled = false;
            rayInteractorLeft.enabled = true;
        }

        
    }

    
}
