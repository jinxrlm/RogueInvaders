using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takeDamage : MonoBehaviour
{
    public IntVariable playerHealth;
    public GameObject playerdeath;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.name == "eshot1(Clone)")
            {
                playerHealth.curValue -= 1;
            }
            else if (collision.gameObject.name == "eshot2(Clone)")
            {
                playerHealth.curValue -= 3;
            }
            else if (collision.gameObject.name == "eshot3(Clone)")
            {
                playerHealth.curValue -= 5;
            }
        }
    }

    void showDeath()
    {
        GameObject deathsplosion1 = Instantiate(playerdeath, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(deathsplosion1, 0.7f);
        expPause();
        GameObject deathsplosion2 = Instantiate(playerdeath, new Vector3(transform.position.x -0.2f, transform.position.y + 0.05f, transform.position.z), Quaternion.identity);
        deathsplosion2.transform.localScale *= 2;
        Destroy(deathsplosion2, 0.7f);
        expPause();
        GameObject deathsplosion3 = Instantiate(playerdeath, new Vector3(transform.position.x + 0.4f, transform.position.y - 0.03f, transform.position.z), Quaternion.identity);
        Destroy(deathsplosion3, 0.7f);
        expPause();
        Destroy(gameObject, 0.4f);
    }

    IEnumerator expPause()
    {
        yield return new WaitForSeconds(0.3f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.curValue < 1)
        {
            showDeath();
        }
    }
}
