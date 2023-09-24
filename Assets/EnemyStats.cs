using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int Rows;
    public int Health;
    public int Damage;
    private int curHealth;

    public GameObject enemydeath;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.name == "pshot1(Clone)")
            {
                curHealth -= 1;
            }
        }
    }

    void showDeath()
    {
        GameObject deathsplosion = Instantiate(enemydeath, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(deathsplosion, 0.7f);
    }

    // Start is called before the first frame update
    void Start()
    {
        curHealth = Health;
    }

    // Update is called once per frame
    void Update()
    {
        if (curHealth < 1)
        {
            //anim.Play("ShieldExplosion");
            showDeath();
            Destroy(gameObject);
        }
    }
}
