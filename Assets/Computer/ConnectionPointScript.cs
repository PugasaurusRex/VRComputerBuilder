using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConnectionPointScript : MonoBehaviour
{
    public TMP_Text HoldingText;

    public GameObject[] ConnectionPoints;
    public GameObject[] ConnectTo;
    public GameObject ObjectToConnectTo;

    //bool tutorial = true;
    bool connected = false;
    bool disableCollider = false;
    bool grabbed = false;

    public float threshhold = .01f;

    OVRGrabbable grab;
    Rigidbody Rig;

    public Vector3 snapPosition;
    public Vector3 snapRotation;

    // Start is called before the first frame update
    void Start()
    {
        grab = this.GetComponent<OVRGrabbable>();
        Rig = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!connected && grab.isGrabbed)
        {
            HoldingText.text = gameObject.name;
            grabbed = true;

            connected = true;
            // Check all connections if in range
            for(int i = 0; i < ConnectionPoints.Length; i++)
            {
                if (Vector3.Distance(ConnectionPoints[i].transform.position, ConnectTo[i].transform.position) > threshhold)
                {
                    connected = false;
                    break;
                }
            }
        }

        if(grabbed && !grab.isGrabbed)
        {
            grabbed = false;
            HoldingText.text = "";
        }

        if (connected)
        {
            if(!disableCollider && !grab.isGrabbed)
            {
                this.GetComponent<Collider>().enabled = false;
                disableCollider = true;
            }

            this.transform.parent = ObjectToConnectTo.transform;
            this.transform.localPosition = snapPosition;
            this.transform.localEulerAngles = snapRotation;
        }
    }
}
