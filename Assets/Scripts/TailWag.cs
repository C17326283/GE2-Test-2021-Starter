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
        /*
        Quaternion toRot = Quaternion.Euler(0,toAngle,0);
        if (Mathf.Abs(transform.rotation.y - toAngle) < 1) //absolute difference between current angle and desired angle
        {
            toAngle *= -1;//change between negative and positive
        }
        
        transform.localRotation = Quaternion.Lerp(transform.localRotation, toRot, Time.deltaTime * wagSpeed);
        */
        float wagSpeed = Mathf.Clamp(defaultWagSpeed + dogRb.velocity.magnitude,0,maxWagSpeed);
        
        float time = Mathf.PingPong(Time.time * wagSpeed, 1);//go back and forth between 0 & length using wagspeed
        transform.localEulerAngles = Vector3.Lerp(new Vector3(0,-maxAngle,0), new Vector3(0,maxAngle,0), time);
    }
}
