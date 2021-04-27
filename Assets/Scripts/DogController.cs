using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    public GameObject attachPoint;
    public GameObject player;
    public GameObject ball;
    
    // Start is called before the first frame update
    void Start()
    {
        //go to player at start
        GetComponent<StateMachine>().ChangeState(new GoToPlayerState());    
    }

    //pick up ball
    public void Attach(GameObject pickedUpObj)
    {
        pickedUpObj.transform.SetParent(attachPoint.transform);//parent it
        pickedUpObj.transform.position = attachPoint.transform.position;//set to pos
        pickedUpObj.GetComponent<Rigidbody>().isKinematic = true;
    }
    
    //drop ball
    public void Detach()
    {
        //drop any children of ball attach point
        foreach (Transform obj in attachPoint.transform)
        {
            obj.transform.SetParent(null);//parent it
            obj.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
