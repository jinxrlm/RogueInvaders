using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Inputs _inputs;
    public Camera cam;

    public GameObject playerPrefab;
    public GameObject shotPrefab;
    public Vector2 fireoffset;
    public IntVariable playerHealth;
    public int maxHealth;
    public GameObject healthbar;

    GameObject ThePlayer;
    GameObject curhealthbar;
    Transform t;



    // Start is called before the first frame update
    void Start()
    {
        ThePlayer = Instantiate(playerPrefab, new Vector3(5.5f, -0.8f), Quaternion.identity);
        ThePlayer.name = "ThePlayer";
        t = ThePlayer.transform;
        _inputs = new Inputs();
        _inputs.UI.Enable();
        playerHealth.curValue = maxHealth;

        curhealthbar = Instantiate(healthbar, healthbar.transform.position, Quaternion.identity);
    }

    void doHealthbar()
    {
        float curHT = (playerHealth.curValue / 50.0f) * 2.5f;
        curhealthbar.transform.localScale = new Vector3(curHT, 2.5f, 2.5f);
    }

    void doShoot()
    {
        GameObject pbullet = Instantiate(shotPrefab, new Vector3(t.position.x + fireoffset.x, t.position.y + fireoffset.y, t.position.z), Quaternion.identity);
        var prb = pbullet.GetComponent<Rigidbody2D>();
        prb.AddForce(Vector3.up * 250);
        prb.angularVelocity = 0;
        pbullet.transform.rotation = Quaternion.Euler(Vector3.zero);
        Destroy(pbullet, 2.0f);
    }

    void SetPlayerPosition()
    {
        Vector2 mousePosition = _inputs.UI.Point.ReadValue<Vector2>();
        Vector2 mousePosWorld = cam.ScreenToWorldPoint(mousePosition);

        ThePlayer.transform.position = new Vector2(mousePosWorld.x, t.position.y);
    }


    // Update is called once per frame
    void Update()
    {
        SetPlayerPosition();
        doHealthbar();

        if (Input.GetMouseButtonDown(0))
        {
            doShoot();
        }
    }
}
