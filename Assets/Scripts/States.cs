using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ChaseBallState : State
{
    public override void Enter()
    {
        owner.GetComponent<Seek>().enabled = true;
    }

    public override void Think()
    {
        if (Vector3.Distance(
            owner.GetComponent<Seek>().targetGameObject.transform.position,
            owner.transform.position) < 1)
        {
            owner.GetComponent<DogController>().Attach(owner.GetComponent<Seek>().targetGameObject);
            //owner.ChangeState(new DefendState());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Seek>().enabled = false;
    }
}

class WaitState : State
{
    public override void Enter()
    {
        owner.GetComponent<Seek>().enabled = false;
    }

    public override void Think()
    {
        //checks it has a target
        if (owner.GetComponent<DogController>().ball != null)
        {
            if (Vector3.Distance(
                owner.GetComponent<DogController>().ball.transform.position,
                owner.GetComponent<DogController>().player.transform.position) > 1)
            {
                owner.GetComponent<Seek>().targetGameObject=owner.GetComponent<DogController>().ball;
                owner.ChangeState(new ChaseBallState());
            }
        }
        else if(GameObject.Find("ball(Clone)"))//there is a ball in the scene
        {
            owner.GetComponent<DogController>().ball = GameObject.Find("ball(Clone)");
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Seek>().enabled = false;
    }
}