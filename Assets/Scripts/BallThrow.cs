using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class BallThrow : MonoBehaviour
{
    public GameObject ball;
    public Rigidbody ballRb;

    public GameObject ballPrefab;

    //public float chargedVal = 0;
    
    public float throwForceMult = 10;
    public float minClamp = .5f;
    public float maxClamp = 10f;
    

    public float startHoldingTime;

    public Slider slider;
    public float chargedVal =0;

    private void Start()
    {
        slider.maxValue = maxClamp;
        slider.minValue = minClamp;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            //startHoldingTime = Time.time;
            chargedVal += Time.deltaTime;
            chargedVal = Mathf.Clamp(chargedVal, minClamp, maxClamp);
        }
        
        if (Input.GetKeyUp("space"))
        {
            Debug.Log("Throw ball");
            Throw();
        }
        
        //update vals
        //chargedVal = Time.time - startHoldingTime;
        slider.value = chargedVal;
            
    }

    public void Throw()
    {
        if (ball == null)
            MakeBall();
        ball.transform.SetParent(null);
        ballRb.isKinematic = false;
        
        ballRb.velocity = Vector3.zero;//cancel velocity
        ball.transform.position = this.transform.position;
        
        float throwForce = chargedVal * throwForceMult;
        
        ballRb.AddForce(transform.forward*throwForce,ForceMode.Impulse);//add an impulse force to throw
        Debug.Log("throw with force:"+throwForce);
        chargedVal = 0;

    }

    public void MakeBall()
    {
        ball = GameObject.Instantiate(ballPrefab, null);
        ballRb = ball.GetComponent<Rigidbody>();
    }
}
