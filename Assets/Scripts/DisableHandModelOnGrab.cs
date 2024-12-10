using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DisableHandModelOnGrab : MonoBehaviour
{
    public GameObject leftHandModel;
    public GameObject rightHandModel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(HideGrabbingHand);
        grabInteractable.selectExited.AddListener(ShowGrabbingHand);
    }

    //hide hand model when grabbing an object
    public void HideGrabbingHand(SelectEnterEventArgs args)
    {
        if(args.interactorObject.transform.tag == "LeftHand")
        {
            leftHandModel.SetActive(false);
        }
        else if(args.interactorObject.transform.tag == "RightHand")
        {
            rightHandModel.SetActive(false);
        }
        else
        {
            Debug.LogError("Hand tag not found");
        }
    }

    //show hand model when releasing an object
    public void ShowGrabbingHand(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform.tag == "LeftHand")
        {
            leftHandModel.SetActive(true);
        }
        else if (args.interactorObject.transform.tag == "RightHand")
        {
            rightHandModel.SetActive(true);
        }
        else
        {
            Debug.LogError("Hand tag not found");
        }
    }
}
