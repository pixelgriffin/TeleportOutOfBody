using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSettings : SingletonComponent<MenuSettings> {

    public enum MovmentType
    {
        TOOB,
        TELEPORT
    }

    public MovmentType type;

	void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
}
