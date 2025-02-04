using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailWag : MonoBehaviour
{
    public float maxAngle = 40;
    public float defaultWagSpeed = 2;
    public GameObject dog;//for using megnitude
    public float maxWagSpeed=20;
    public float wagSpeedMultiplier = 2f;

    // Update is called once per frame
    void Update()
    {
        //get tail speed based on speed dog is moving
        float wagSpeed = Mathf.Clamp(defaultWagSpeed + (dog.GetComponent<Boid>().velocity.magnitude*wagSpeedMultiplier),0,maxWagSpeed);
        
        //wave tail back and forth
        float time = Mathf.PingPong(Time.time * wagSpeed, 1);//go back and forth between 0 & length using wagspeed
        transform.localEulerAngles = Vector3.Lerp(new Vector3(0,-maxAngle,0), new Vector3(0,maxAngle,0), time);
    }
}
