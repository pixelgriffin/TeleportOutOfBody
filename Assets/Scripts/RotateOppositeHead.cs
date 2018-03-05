using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class RotateOppositeHead : MonoBehaviour {

    private Player player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(player.hmdTransform.eulerAngles.y + " + 180");
        this.transform.rotation = Quaternion.Euler(new Vector3(0, player.hmdTransform.eulerAngles.y, 0));
	}
}
