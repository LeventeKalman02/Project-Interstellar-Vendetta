using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class DisableHandModelOnGrab : MonoBehaviour
{
    public GameObject leftHandModel;
    public GameObject rightHandModel;

    private XRDirectInteractor leftHandInteractor;
    private XRDirectInteractor rightHandInteractor;

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
        if (args.interactorObject is XRDirectInteractor directInteractor)
        {
            // Check if the interactor is the left or right hand
            if (directInteractor.name.Contains("Left"))
            {
                leftHandInteractor = directInteractor; // Track the left hand interactor
                if (leftHandModel != null)
                {
                    leftHandModel.SetActive(false);
                }
            }
            else if (directInteractor.name.Contains("Right"))
            {
                rightHandInteractor = directInteractor; // Track the right hand interactor
                if (rightHandModel != null)
                {
                    rightHandModel.SetActive(false);
                }
            }
            else
            {
                Debug.LogError("Interactor name does not indicate left or right hand.");
                Debug.Log($"Interactor: {directInteractor.gameObject.name}");
            }
        }
    }    

    //show hand model when releasing an object
    public void ShowGrabbingHand(SelectExitEventArgs args)
    {
        if (args.interactorObject is XRDirectInteractor directInteractor)
        {
            // Check if the interactor is the left or right hand
            if (directInteractor == leftHandInteractor)
            {
                leftHandInteractor = null; // Clear the left hand interactor
                if (leftHandModel != null && rightHandInteractor == null)
                {
                    leftHandModel.SetActive(true);
                }
            }
            else if (directInteractor == rightHandInteractor)
            {
                rightHandInteractor = null; // Clear the right hand interactor
                if (rightHandModel != null && leftHandInteractor == null)
                {
                    rightHandModel.SetActive(true);
                }
            }
            else
            {
                Debug.LogError("Interactor name does not indicate left or right hand.");
                Debug.Log($"Interactor: {directInteractor.gameObject.name}");
            }
        }
    }
}
