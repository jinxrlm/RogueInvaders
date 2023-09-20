using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Inputs _inputs;
    public Camera cam;

    public GameObject playerPrefab;

    GameObject ThePlayer;
    Transform t;

    // Start is called before the first frame update
    void Start()
    {
        ThePlayer = Instantiate(playerPrefab, new Vector3(5.5f, -1.5f), Quaternion.identity);
        ThePlayer.name = "ThePlayer";
        t = ThePlayer.transform;
        _inputs = new Inputs();
        _inputs.UI.Enable();
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
    }
}
