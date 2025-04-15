using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityInteractable : MonoBehaviour
{
    public Material defaultMat;
    public Material outlineMat;
    public bool isSelected;
    public GameObject petMenu;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            isSelected = true;     
            ShowOutLine(isSelected);
            petMenu.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player")
        {
            isSelected = false;     
            ShowOutLine(isSelected);
            petMenu.gameObject.SetActive(false);
        }
    }


    void ShowOutLine(bool onOutLine)
    {
        if(defaultMat == null || outlineMat == null )
            return;
        if(onOutLine)
        {
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = outlineMat;
        }
        else
        {
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = defaultMat;
        }
    }


}
