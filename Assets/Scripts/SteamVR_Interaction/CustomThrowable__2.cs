using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;



public class CustomThrowable__2 : Throwable__2
{
    protected override void OnAttachedToHand(Hand hand)
    {
        base.OnAttachedToHand(hand);

        Debug.Log("Object attached to hand: " + hand.name);
    }

    protected override void OnDetachedFromHand(Hand hand)
    {
        base.OnDetachedFromHand(hand);

        Debug.Log("Object detached from hand: " + hand.name);
    }

    protected override void HandHoverUpdate(Hand hand)
    {
        base.HandHoverUpdate(hand);

        Debug.Log("Object HandHoverUpdate from hand: " + hand.name);
    }

    protected override void HandAttachedUpdate(Hand hand)
    {
        base.HandAttachedUpdate(hand);

        Debug.Log("Object HandAttachedUpdate from hand: " + hand.name);
    }
}
