using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMovementType : MonoBehaviour {

    public MenuSettings.MovmentType setType;


    public void SetType()
    {
        MenuSettings.Instance.type = setType;
    }
}
