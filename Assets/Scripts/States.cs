using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for dog chasing after ball
class ChaseBallState : State
{
    public override void Enter()
    {
        Debug.Log("Dog state: ChaseBallState");
        owner.GetComponent<Seek>().enabled = true;

        //play random bark when starting to chase
        owner.GetComponent<AudioManager>().PlayBark();
    }

    public override void Think()
    {
        
        float dist = Vector3.Distance(owner.GetComponent<Seek>().targetGameObject.transform.position, owner.transform.position);
        if (dist < 10)//arrive at ball
        {
            owner.GetComponent<Arrive>().targetGameObject = owner.GetComponent<Seek>().targetGameObject;
            owner.GetComponent<Seek>().enabled = false;
            owner.GetComponent<Arrive>().enabled = true;
        }
        if (dist < 2)//is at ball
        {
            owner.GetComponent<DogController>().Attach(owner.GetComponent<Seek>().targetGameObject);
            owner.ChangeState(new GoToPlayerState());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Seek>().enabled = false;
        owner.GetComponent<Arrive>().enabled = false;
    }
}

//go to point infront of player
class GoToPlayerState : State
{
    public override void Enter()
    {
        Debug.Log("Dog state: GoToPlayerState");
        owner.GetComponent<Seek>().targetGameObject = GameObject.Find("Main Camera");
        owner.GetComponent<Seek>().enabled = true;
    }

    public override void Think()
    {
        float dist = Vector3.Distance(owner.GetComponent<Seek>().targetGameObject.transform.position,
            owner.transform.position);
        if (dist < 15)//arriving at player
        {
            owner.GetComponent<Arrive>().targetGameObject = owner.GetComponent<Seek>().targetGameObject;
            owner.GetComponent<Seek>().enabled = false;
            owner.GetComponent<Arrive>().enabled = true;
        }
        if (dist < 10)//is at player so wait and drop ball
        {
            owner.GetComponent<DogController>().Detach();
            owner.ChangeState(new WaitState());
        }
        
    }

    public override void Exit()
    {
        owner.GetComponent<Seek>().enabled = false;
        owner.GetComponent<Arrive>().enabled = false;
    }
}

class WaitState : State
{
    public override void Enter()
    {
        Debug.Log("Dog state: WaitState");
        owner.GetComponent<Seek>().enabled = false;
    }

    public override void Think()
    {
        //checks it has a target
        if (owner.GetComponent<DogController>().ball != null)
        {
            //wait for the ball to get x distance away
            if (Vector3.Distance(
                owner.GetComponent<DogController>().ball.transform.position,
                owner.GetComponent<DogController>().player.transform.position) > 10)
            {
                owner.GetComponent<Seek>().targetGameObject=owner.GetComponent<DogController>().ball;
                owner.ChangeState(new ChaseBallState());
            }
        }
        else if(GameObject.Find("ball(Clone)"))//there is a ball in the scene
        {
            owner.GetComponent<DogController>().ball = GameObject.Find("ball(Clone)");
        }
        
        //if standing still look at player
        if (owner.GetComponent<Boid>().velocity.magnitude < .5f)
        {
            owner.GetComponent<Boid>().velocity=Vector3.zero;
            owner.GetComponent<Boid>().acceleration=Vector3.zero;
                
            Vector3 targetDir = owner.GetComponent<DogController>().player.transform.position - owner.transform.position;
            targetDir.y = 0;//keep it on same level
            Quaternion targetRot = Quaternion.LookRotation(targetDir);
            
            owner.transform.rotation = Quaternion.Slerp( owner.transform.rotation, targetRot, Time.deltaTime );
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Seek>().enabled = false;
        owner.GetComponent<Arrive>().enabled = false;
    }
}