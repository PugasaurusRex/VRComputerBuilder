using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionPointScript : MonoBehaviour
{
    public GameObject[] ConnectionPoints;
    public GameObject[] ConnectTo;
    public GameObject ObjectToConnectTo;

    public List<GameObject> AttachedComponents;

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
                if (Vector3.Distance(ConnectionPoints[i].transform.position, ConnectTo[i].transform.position) > threshhold)
                {
                    connect = false;
                    break;
                }
            }

            if(connect)
            {
                if(ObjectToConnectTo.GetComponent<ConnectionPointScript>().AttachedComponents.Count > 0)
                {
                    foreach(GameObject i in ObjectToConnectTo.GetComponent<ConnectionPointScript>().AttachedComponents)
                    {
                        if(i.GetComponent<IdScript>().id == this.GetComponent<IdScript>().id)
                        {
                            // Set component active on parent
                            i.SetActive(true); 

                            // Set all active children active on parent
                            // ToDo

                            // Destroy this component
                            Destroy(this.gameObject);
                        }
                    }
                }
            }
        }
    }
}
