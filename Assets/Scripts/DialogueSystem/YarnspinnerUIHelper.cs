using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class YarnspinnerUIHelper : MonoBehaviour
{
    public Transform OptionButtonParent = null;
    public void DeselectUI()
    {
        EventSystem.current.SetSelectedGameObject(null); // deselect!
    }

    public void SelectFirstOption()
    {
        for (int i = 0; i < OptionButtonParent.childCount; i++)
        {
            GameObject currObject = OptionButtonParent.GetChild(i).gameObject;
            if (currObject.activeSelf && currObject.GetComponent<UnityEngine.UI.Button>())
            {
                EventSystem.current.SetSelectedGameObject(currObject);
                break;
            }
        }
    }
}