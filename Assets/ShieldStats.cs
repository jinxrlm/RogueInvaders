using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldStats : MonoBehaviour
{
    public IntVariable sHealth;
    private int curHealth;

    public GameObject shielddeath;

    //Animator anim;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            //print(collision.gameObject.name);
            if (collision.gameObject.name == "eshot1(Clone)")
            {
                curHealth -= 1;
            }
            else if (collision.gameObject.name == "eshot2(Clone)")
            {
                curHealth -= 3;
            }
            else if (collision.gameObject.name == "eshot3(Clone)")
            {
                curHealth -= 5;
            }
        }
    }

    void showDeath()
    {
        GameObject deathsplosion = Instantiate(shielddeath, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        shielddeath.transform.localScale = transform.localScale;
        Destroy(deathsplosion, 0.7f);
    }

    // Start is called before the first frame update
    void Start()
    {
        curHealth = sHealth.curValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (curHealth < 1)
        {
            showDeath();
            Destroy(gameObject);
        }
    }
}
