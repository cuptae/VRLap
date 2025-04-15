using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandInteractor : MonoBehaviour
{
    public PlayerCtrl playerCtrl; // XRRig의 PlayerCtrl 참조

    private void Start()
    {
        // XRRig에서 PlayerCtrl 찾기
        playerCtrl = FindObjectOfType<PlayerCtrl>();
    }


    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (playerCtrl == null) return;

  
    }

    public void OnSelectExited(SelectExitEventArgs args)
    {
        if (playerCtrl == null) return;


    }
}
