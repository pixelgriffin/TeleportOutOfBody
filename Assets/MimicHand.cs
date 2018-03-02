using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class MimicHand : MonoBehaviour {

    public Hand hand;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update () {
        this.transform.localPosition = hand.transform.localPosition - new Vector3(player.GetComponent<Player>().hmdTransform.localPosition.x, this.transform.parent.position.y, player.GetComponent<Player>().hmdTransform.localPosition.z);
        this.transform.localRotation = hand.transform.localRotation;

        this.transform.localPosition += new Vector3(0, this.transform.parent.position.y, 0);
	}
}
