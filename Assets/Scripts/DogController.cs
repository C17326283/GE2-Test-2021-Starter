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
        GetComponent<StateMachine>().ChangeState(new WaitState());    
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Attach(GameObject pickedUpObj)
    {
        pickedUpObj.transform.SetParent(attachPoint.transform);//parent it
        pickedUpObj.transform.position = attachPoint.transform.position;//set to pos
    }
    
    public void Detach(GameObject pickedUpObj)
    {
        pickedUpObj.transform.SetParent(null);//parent it
    }
}
