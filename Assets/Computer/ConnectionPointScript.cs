using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionPointScript : MonoBehaviour
{
    public GameObject[] ConnectionPoints;
    public GameObject[] ConnectTo;
    public GameObject ObjectToConnectTo;

    //bool tutorial = true;
    bool connected = false;

    public float threshhold = .3f;

    OVRGrabbable grab;

    // Start is called before the first frame update
    void Start()
    {
        grab = this.GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!connected && grab.isGrabbed)
        {
            bool connect = true;
            // Check all connections if in range
            for(int i = 0; i < ConnectionPoints.Length; i++)
            {
                Debug.Log("Distance = " + Vector3.Distance(ConnectionPoints[i].transform.position, ConnectTo[i].transform.position));
                if (Vector3.Distance(ConnectionPoints[i].transform.position, ConnectTo[i].transform.position) > threshhold)
                {
                    connect = false;
                    break;
                }
            }

            if(connect)
            {
                // Connect pieces together
                connected = true;
                this.transform.parent = ObjectToConnectTo.transform;
            }
        }
    }
}
