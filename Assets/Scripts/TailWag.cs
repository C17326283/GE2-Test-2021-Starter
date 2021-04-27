using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailWag : MonoBehaviour
{
    public float maxAngle = 40;
    public float defaultWagSpeed = 2;
    public Rigidbody dogRb;//for using megnitude
    public float maxWagSpeed=20;

    // Update is called once per frame
    void Update()
    {
        float wagSpeed = Mathf.Clamp(defaultWagSpeed + dogRb.velocity.magnitude,0,maxWagSpeed);
        
        float time = Mathf.PingPong(Time.time * wagSpeed, 1);//go back and forth between 0 & length using wagspeed
        transform.localEulerAngles = Vector3.Lerp(new Vector3(0,-maxAngle,0), new Vector3(0,maxAngle,0), time);
    }
}
