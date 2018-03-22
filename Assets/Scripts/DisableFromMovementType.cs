using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableFromMovementType : MonoBehaviour {

    public MenuSettings.MovmentType allowedType;

	void Update () {
		if(allowedType != MenuSettings.Instance.type)
        {
            for(int i = 0; i < this.transform.childCount; i++)
            {
                this.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                this.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
	}
}
