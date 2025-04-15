using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.XR.Interaction.Toolkit;

public class VRHandOverInteractable : MonoBehaviour
{
    public Material defaultMat;
    public Material selectedMat;
    public void Selected()
    {
        gameObject.GetComponent<MeshRenderer>().material = selectedMat;
    }

    public void Deselected()
    {
        gameObject.GetComponent<MeshRenderer>().material = defaultMat;
    }
}
