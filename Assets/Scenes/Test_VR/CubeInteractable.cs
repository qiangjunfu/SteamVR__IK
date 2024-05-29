using UnityEngine;
using Valve.VR.InteractionSystem;

public class CubeInteractable : MonoBehaviour
{
    private Interactable interactable;

    void Awake()
    {
        interactable = GetComponent<Interactable>();
        if (interactable == null)
        {
            interactable = gameObject.AddComponent<Interactable>();
        }
    }

    void OnHandHoverBegin(Hand hand)
    {
        Debug.Log("Hand hovering over Cube");
    }

    void OnHandHoverEnd(Hand hand)
    {
        Debug.Log("Hand stopped hovering over Cube");
    }

    void HandHoverUpdate(Hand hand)
    {
        if (hand.GetGrabStarting() != GrabTypes.None)
        {
            Debug.Log("Hand is attempting to grab the cube");
            hand.AttachObject(gameObject, GrabTypes.Grip);
        }
    }

    void OnAttachedToHand(Hand hand)
    {
        Debug.Log("Cube attached to hand");
        // Make sure the hand is visible
        hand.Show();
    }

    void OnDetachedFromHand(Hand hand)
    {
        Debug.Log("Cube detached from hand");
        // Ensure the hand is visible again after detaching
        hand.Show();
    }

    void Update()
    {
        // Ensure we detach the object when the grab ends
        foreach (Hand hand in Player.instance.hands)
        {
            if (hand.IsGrabEnding(gameObject))
            {
                Debug.Log("Hand is releasing the cube");
                hand.DetachObject(gameObject);
            }
        }
    }
}
