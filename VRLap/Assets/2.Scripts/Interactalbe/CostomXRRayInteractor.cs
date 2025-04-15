using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CostomXRRayInteractor : XRRayInteractor
{

    public PlayerCtrl playerCtrl; // XRRig의 PlayerCtrl 참조

    private void Start()
    {
        // XRRig에서 PlayerCtrl 찾기
        playerCtrl = FindObjectOfType<PlayerCtrl>();
    }

    protected override void OnHoverEntered(XRBaseInteractable interactable)
    {

    }

    protected override void OnHoverExited(XRBaseInteractable interactable)
    {

    }


    protected override void OnSelectEntered(XRBaseInteractable interactable)
    {


 
    }


    protected override void OnSelectExited(SelectExitEventArgs args)
    {

    }
}
