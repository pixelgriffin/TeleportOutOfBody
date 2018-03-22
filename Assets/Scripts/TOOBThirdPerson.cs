using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Player))]
public class TOOBThirdPerson : MonoBehaviour {

    public GameObject body;
    public float speed = 3f;

    private Player player;

    private Vector3 oldBodyPos = Vector3.zero;
    private bool detachingFromBody = false;

    private bool outOfBody = false;

	// Use this for initialization
	void Start () {
        player = GetComponent<Player>();

        body.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (MenuSettings.Instance.type != MenuSettings.MovmentType.TOOB)
            return;

        //Hand controls ---------------------------------
		foreach(Hand hand in player.hands)
        {
            if (hand.controller != null)
            {
                if (!outOfBody)
                {
                    /*if (hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && !detachingFromBody)
                    {
                        oldBodyPos = player.hmdTransform.position;
                        detachingFromBody = true;
                    }
                    else if (hand.controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad) && detachingFromBody)
                    {
                        float dist = Vector3.Distance(new Vector3(player.hmdTransform.position.x, 0, player.hmdTransform.position.z), new Vector3(oldBodyPos.x, 0, oldBodyPos.z));

                        this.transform.position = new Vector3(this.transform.position.x, dist * 3, this.transform.position.z);

                        if (dist > 0.5f)
                        {
                            LeaveBody();
                            detachingFromBody = false;
                        }
                    }
                    else if (hand.controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
                    {
                        detachingFromBody = false;
                        this.transform.position -= new Vector3(0, this.transform.position.y, 0);
                    }*/
                    if(hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
                    {
                        LeaveBody();
                        this.transform.position += new Vector3(-player.hmdTransform.forward.x * 3f, 2.5f, -player.hmdTransform.forward.z * 3f);

                        if (Statistics.Instance.allowDataCollection)
                        {
                            Statistics.Instance.data.oobCount++;
                        }
                    }
                }
                else//Out of body
                {
                    //if (hand.GuessCurrentHandType() == Hand.HandType.Right)
                    {
                        Vector3 flatForward = player.hmdTransform.forward;
                        flatForward.y = 0;
                        Vector3 flatRight = player.hmdTransform.right;
                        flatRight.y = 0;

                        //TODO replace with character controller movement/velocity change
                        //body.transform.position += flatForward * hand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y * Time.deltaTime * speed;
                        //body.transform.position += flatRight * hand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x * Time.deltaTime * speed;

                        Vector3 movement = flatForward* hand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y * Time.deltaTime * speed;
                        movement += flatRight * hand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x * Time.deltaTime * speed;
                        movement *= 100f;

                        body.GetComponent<Rigidbody>().velocity = movement + new Vector3(0, body.GetComponent<Rigidbody>().velocity.y, 0);

                        body.transform.Find("Model").GetComponent<Animator>().SetFloat("Speed", hand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).magnitude);

                        body.transform.Find("Model").rotation = Quaternion.Euler(0, player.hmdTransform.eulerAngles.y, 0);

                        if (hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
                        {
                            RejoinBody();
                        }
                    }
                    /*else
                    {
                        if (hand.controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
                        {
                            //body.GetComponent<Rigidbody>().AddForce(Vector3.up * 10000f, ForceMode.Impulse);
                            //body.transform.Find("Model").GetComponent<Animator>().SetBool("Jump", true);
                        }
                    }*/
                }
            }
        }

        //------------------------------------------------

        if (outOfBody)
        {
            //Raycast rejoin
            Debug.DrawLine(player.hmdTransform.position, body.transform.position + new Vector3(0, 1.6f, 0));
            if (Physics.Linecast(player.hmdTransform.position, body.transform.position + new Vector3(0, 1.6f, 0), 1 << LayerMask.NameToLayer("Obstacle")))
            {
                RejoinBody();
            }
        }
	}

    private void LeaveBody()
    {
        Vector3 flatForward = player.hmdTransform.forward;
        flatForward.y = 0;

        //RaycastHit hit;
        //Physics.Raycast(player.hmdTransform.position, Vector3.down, out hit, 100f, 1 << LayerMask.NameToLayer("Obstacle"));

        body.transform.position = player.transform.position + new Vector3(player.hmdTransform.localPosition.x, 0f, player.hmdTransform.localPosition.z);// + flatForward * 2f;

        /*if(hit.collider != null)
        {
            body.transform.position = new Vector3(body.transform.position.x, hit.point.y + 2f, body.transform.position.z);
        }*/

        body.SetActive(true);
        outOfBody = true;
    }

    private void RejoinBody()
    {
        player.transform.position = body.transform.position - new Vector3(player.hmdTransform.localPosition.x, 0, player.hmdTransform.localPosition.z);
        //player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

        body.SetActive(false);

        outOfBody = false;
    }

    public bool IsOutOfBody()
    {
        return outOfBody;
    }
}
