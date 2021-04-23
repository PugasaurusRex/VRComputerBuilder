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

    public bool tutorial = false;
    public bool connected = false;
    bool disableCollider = false;
    bool grabbed = false;
    bool firstGrab;
    bool collided;

    public float threshhold = .01f;

    OVRGrabbable grab;
    Rigidbody Rig;

    public Vector3 snapPosition;
    public Vector3 snapScale;
    public Vector3 snapRotation;
    public GameObject resetPoint;

    AudioSource Speaker;
    public AudioClip ConnectSound;
    public AudioClip CollisionSound;
    public AudioClip GrabSound;

    // Start is called before the first frame update
    void Start()
    {
        Speaker = GetComponent<AudioSource>();

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

            if (!firstGrab)
            {
                Speaker.clip = GrabSound;
                Speaker.PlayOneShot(Speaker.clip);
                firstGrab = true;

                if(tutorial)
                {
                    foreach(GameObject i in ConnectionPoints)
                    {
                        i.GetComponentInChildren<ParticleSystem>().Play();
                    }
                    foreach (GameObject i in ConnectTo)
                    {
                        i.GetComponentInChildren<ParticleSystem>().Play();
                    }
                }
            }

            // Check all connections if in range
            if(ConnectionPoints.Length > 0)
            {
                connected = true;

                for (int i = 0; i < ConnectionPoints.Length; i++)
                {
                    if (Vector3.Distance(ConnectionPoints[i].transform.position, ConnectTo[i].transform.position) > threshhold)
                    {
                        connected = false;
                        break;
                    }
                }
            }

            if(connected)
            {
                Speaker.clip = ConnectSound;
                Speaker.PlayOneShot(Speaker.clip);
            }
        }

        if(grabbed && !grab.isGrabbed)
        {
            firstGrab = false;
            grabbed = false;
            HoldingText.text = "";

            Speaker.clip = GrabSound;
            Speaker.PlayOneShot(Speaker.clip);

            foreach (GameObject i in ConnectionPoints)
            {
                i.GetComponentInChildren<ParticleSystem>().Pause();
                i.GetComponentInChildren<ParticleSystem>().Clear();
            }
            foreach (GameObject i in ConnectTo)
            {
                i.GetComponentInChildren<ParticleSystem>().Pause();
                i.GetComponentInChildren<ParticleSystem>().Clear();
            }
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
            this.transform.localScale = snapScale;
        }
        else
        {
            if(transform.position.y < 0.2f)
            {
                transform.position = resetPoint.transform.position;
                Rig.velocity = Vector3.zero;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!collided)
        {
            Speaker.clip = CollisionSound;
            Speaker.PlayOneShot(Speaker.clip);

            collided = true;
            StartCoroutine(Collision());
        }
    }

    IEnumerator Collision()
    {
        yield return new WaitForSeconds(.25f);
        collided = false;
    }
}
