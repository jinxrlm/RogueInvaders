using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeCollider : MonoBehaviour
{
    public motionVars curMotion;
    public float Speed = 1.0f;

    public Vector3 Direction = Vector3.down;

    public AnimationCurve AnimCurve;

    private float curveTime;
    private float lastKeyTime;

    void FindLastKey()
    {
        //Get the last keyframe and log it's time
        Keyframe lastKey = AnimCurve[AnimCurve.length - 1];
        lastKeyTime = lastKey.time;
    }

    void SwitchRow()
    {
        curMotion.lockRowShift = true;

        GameObject[] currentWave = GameObject.FindGameObjectsWithTag("Enemy");
        curveTime += Time.deltaTime;

        foreach (GameObject e in currentWave)
        {
            e.transform.position += Direction * Speed * AnimCurve.Evaluate(curveTime / lastKeyTime) * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (curMotion.switchedRow == false && collision.gameObject.name != "ThePlayer")
        {
            curveTime = 0;
            curMotion.curDirection = Vector3.down;
            while (curveTime < lastKeyTime)
            {
                SwitchRow();
            }

            
            print(gameObject.name);
            
            if (gameObject.name == "LeftEdge")
            {
                print("LEFT EDGE");
                curMotion.isGoingLeft = true;
            }
            else if (gameObject.name == "RightEdge")
            {
                print("RIGHT EDGE");
                curMotion.isGoingLeft = false;
            }
            curMotion.switchedRow = true;
            curMotion.curDirection = curMotion.isGoingLeft ? Vector3.right : Vector3.left;
            curMotion.lockRowShift = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        curMotion.switchedRow = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        FindLastKey();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
