using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyMotion : MonoBehaviour
{
    public int enemyType;
    public GameObject Bullet;
    //public GameObject Bullet2;
    //public GameObject Bullet3;
    public Vector2 fireOffset;
    public float bulletSpeed;
    
    public float Speed = 1.0f;

    public Vector3 Direction = Vector3.left;

    //public AnimationCurve AnimCurve;
    private AnimationCurve AnimCurve;

    private float curveTime;
    private float lastKeyTime;

    public motionVars curMotion;

    public float bulletLife;
    public float bulletTime;

    void FindLastKey()
    {
        //Get the last keyframe and log it's time
        Keyframe lastKey = AnimCurve[AnimCurve.length - 1];
        lastKeyTime = lastKey.time;
    }

    public void doMotion()
    {
        curveTime += Time.deltaTime;

        Direction = curMotion.curDirection;

        Transform Owner = GetComponent<Transform>();
        Owner.position += Direction.normalized * Speed * AnimCurve.Evaluate(curveTime / lastKeyTime) * Time.deltaTime; //work from here!
        //print(AnimCurve.postWrapMode);
    }

    public void doShoot()
    {

        if (enemyType == 1)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -0.3f), -Vector3.up, Mathf.Infinity);

            if (hit.collider != null)
            {
                if (hit.collider.name == "ThePlayer")
                {
                    GameObject bullet = Instantiate(Bullet, new Vector3(transform.position.x + fireOffset.x, transform.position.y + fireOffset.y, transform.position.z), Quaternion.identity);
                    var brb = bullet.GetComponent<Rigidbody2D>();
                    brb.AddForce(Vector3.down * bulletSpeed);
                    brb.angularVelocity = 0;
                    bullet.transform.rotation = Quaternion.Euler(Vector3.zero);
                    Destroy(bullet, bulletLife);
                }
            }
        }
        else if (enemyType == 2)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -0.4f), -Vector3.up, Mathf.Infinity);

            if (hit.collider != null)
            {
                if (hit.collider.name == "ThePlayer")
                {
                    GameObject bullet = Instantiate(Bullet, new Vector3(transform.position.x + fireOffset.x, transform.position.y + fireOffset.y, transform.position.z), Quaternion.identity);
                    var brb = bullet.GetComponent<Rigidbody2D>();
                    brb.AddForce(Vector3.down * bulletSpeed * 2.0f);
                    brb.angularVelocity = 0;
                    bullet.transform.rotation = Quaternion.Euler(Vector3.zero);
                    Destroy(bullet, bulletLife);
                }
            }
        }
        else if (enemyType == 3)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f), -Vector3.up, Mathf.Infinity);

            if (hit.collider != null)
            {
                if (hit.collider.name == "ThePlayer" || hit.collider.name == "shield1(Clone)")
                    {
                    GameObject bullet = Instantiate(Bullet, new Vector3(transform.position.x + fireOffset.x, transform.position.y + fireOffset.y, transform.position.z), Quaternion.identity);
                    var brb = bullet.GetComponent<Rigidbody2D>();
                    brb.AddForce(Vector3.down * bulletSpeed * 1.7f);
                    brb.angularVelocity = 0;
                    bullet.transform.rotation = Quaternion.Euler(Vector3.zero);
                    Destroy(bullet, bulletLife);
                    pauseHeavyFire();
                }
            }
        }

    }

    IEnumerator pauseHeavyFire()
    {
        yield return new WaitForSeconds(5.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletTime = 0f;
        AnimCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.2f, 0.75f), new Keyframe(0.4f, 0), new Keyframe(2, 0));

        AnimCurve.preWrapMode = WrapMode.Once;
        AnimCurve.postWrapMode = WrapMode.Once;
        //print(AnimCurve.postWrapMode);

        FindLastKey();
    }

    // Update is called once per frame
    void Update()
    {
        if (curveTime < lastKeyTime && !curMotion.lockRowShift)
        {
            doMotion();
        }
        else
        {
            //if (Direction == Vector3.down)
            //{
            //    if (curMotion.switchedRow)
            //    {
            //        //doMotion();
            //        //curMotion.isGoingLeft = !curMotion.isGoingLeft;
            //        curMotion.curDirection = curMotion.isGoingLeft ? Vector3.right : Vector3.left;
            //    }
            //}
            curveTime = 0;
        }
    }
    void FixedUpdate()
    {
        doShoot();
    }
}
