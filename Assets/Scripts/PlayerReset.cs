using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    Rigidbody Rig;

    // Start is called before the first frame update
    void Start()
    {
        Rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -1)
        {
            Rig.velocity = Vector3.zero;
            transform.position = new Vector3(0, 1, 0.75f);
        }
    }
}
