using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psplode : MonoBehaviour
{
    public GameObject pshotSplosion;
    public IntVariable curScore;
    public Vector2 dOffset;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                GameObject pshotsplode = Instantiate(pshotSplosion, new Vector3(transform.position.x + dOffset.x, transform.position.y + dOffset.y, transform.position.z), Quaternion.identity);

                if (collision.gameObject.name == "Enemy1(Clone)")
                {
                    curScore.curValue += 100;
                }
                else if (collision.gameObject.name == "Enemy2(Clone)")
                {
                    curScore.curValue += 500;
                }
                else if (collision.gameObject.name == "Enemy3(Clone)")
                {
                    curScore.curValue += 2000;
                }

                Destroy(gameObject);
                Destroy(pshotsplode, 0.7f);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
