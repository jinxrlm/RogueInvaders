using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splode : MonoBehaviour
{
    public GameObject shotSplosion;
    public Vector2 dOffset;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.name == "ThePlayer" || collision.gameObject.name == "shield1(Clone)")
            {
                GameObject shotsplode = Instantiate(shotSplosion, new Vector3(transform.position.x + dOffset.x, transform.position.y + dOffset.y, transform.position.z), Quaternion.identity);
                if (gameObject.name == "eshot2(Clone)")
                {
                    shotsplode.transform.localScale *= 1.5f;
                }
                else if (gameObject.name == "eshot3(Clone)")
                {
                    shotsplode.transform.localScale *= 2f;
                }

                Destroy(gameObject);
                Destroy(shotsplode, 0.7f);
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
