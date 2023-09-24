using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;

    [SerializeField] private GameObject _tilePrefab;

    [SerializeField] private GameObject _laserPrefab;

    [SerializeField] private GameObject _shieldPrefab;

    [SerializeField] private Transform _cam;

    [SerializeField] private GameObject[] _enemies;

    public List<GameObject> currentWave = new List<GameObject>();

    public IntVariable shieldHealth;

    public float Speed = 1.0f;

    public Vector3 Direction = Vector3.left;
    public Vector3 DirDown = Vector3.down;

    public motionVars curMotion;

    //public AnimationCurve AnimCurve;

    //bool isMoving = false;
    //bool isSwitchingRow = false;
    public bool isColliding = false;
    BoxCollider2D LeftEdge;
    BoxCollider2D RightEdge;
    //bool enemyLeft = false;

    public bool CheckCollision()
    {
        return isColliding;
    }

    void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
            }
        }
    }

    void LaserGrid()
    {
        for (int x = 0; x < 45; x++)
        {
            var spawnedLaser = Instantiate(_laserPrefab, new Vector3(x * 0.25f, 1), Quaternion.identity);
            spawnedLaser.name = $"Laser{x + 1}";
        }
    }

    void ShieldGrid()
    {
        for (int x = 0; x < 5; x++)
        {
            var spawnedShield = Instantiate(_shieldPrefab, new Vector3(0.75f + (x * 2.37f), 0.3f), Quaternion.identity);
            //spawnedShield.name = $"Shield{x}1";
            spawnedShield.transform.localScale = new Vector3(1.9f, 1.6f, 1f);
            //spawnedShield.tag = "Shield";
        }
        for (int x = 0; x < 5; x++)
        {
            var spawnedShield = Instantiate(_shieldPrefab, new Vector3(0.75f + (x * 2.37f), 0), Quaternion.identity);
            //spawnedShield.name = $"Shield{x}2";
            //spawnedShield.tag = "Shield";
        }
        for (int x = 0; x < 5; x++)
        {
            var spawnedShield = Instantiate(_shieldPrefab, new Vector3(0.75f + (x * 2.37f), -0.25f), Quaternion.identity);
            //spawnedShield.name = $"Shield{x}3";
            spawnedShield.transform.localScale = new Vector3(1f, 1f, 1f);
            //spawnedShield.tag = "Shield";
        }
    }


    void SpawnEnemies()
    {
        int startTile = 1;
        int endTile = 11;
        int curRow = 0;

        for (int i = 0; i < _enemies.Length; i++)
        {
            int Rows = _enemies[i].GetComponent<EnemyStats>().Rows;

            for (int r = _height - 1; r > _height - (1 + Rows); r--)
            {
                for (int x = startTile; x < endTile; x++)
                {
                    var spawnedEnemy = Instantiate(_enemies[i], new Vector3(x, (r - curRow) * 0.9f), Quaternion.identity);
                    //spawnedEnemy.name = $"Enemy{i+1} {x} {r}";
                    currentWave.Add(spawnedEnemy);
                }
            }
            curRow += Rows;
        }

    }

    //void moveCurrentWave()
    //{
    //    if (!isMoving && !isSwitchingRow)
    //    {
    //        float eMove = (enemyLeft ? 1 : -1) * 1.0f;

    //        foreach (GameObject e in currentWave)
    //        {
    //            e.transform.position += eMove * Direction.normalized * Speed * AnimCurve.Evaluate(Time.time) * Time.deltaTime;
    //        }
    //        //print($"moved: {eMove}");
    //        //StartCoroutine(enemyMoveTick(Speed));
    //    }
    //}

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (!isSwitchingRow && collision.gameObject.name != "ThePlayer")
    //    {
    //        float eRowMove = 0.5f;

    //        foreach (GameObject e in currentWave)
    //        {
    //            e.transform.position += eRowMove * DirDown.normalized * Speed * AnimCurve.Evaluate(Time.time) * Time.deltaTime;
    //        }
    //        //print($"collided w {collision.gameObject.name} | sw. row: {eRowMove}");
    //        enemyLeft = !enemyLeft;

    //        //StartCoroutine(enemyRowTick(Speed));
    //    }
    //}

    //IEnumerator enemyMoveTick(float Tick)
    //{
    //    yield return new WaitForSeconds(Tick * 0.2f);
    //    isMoving = true;
    //    isSwitchingRow = false;
    //    yield return new WaitForSeconds(Tick);
    //    isMoving = false;
    //    isSwitchingRow = false;
    //}

    //IEnumerator enemyRowTick(float Tick)
    //{
    //    //yield return new WaitForSeconds(Tick * 2.0f);
    //    isMoving = false;
    //    isSwitchingRow = true;
    //    yield return new WaitForSeconds(Tick * 2.0f);
    //    isMoving = false;
    //    isSwitchingRow = false;
    //}

    // Start is called before the first frame update
    void Start()
    {
        //GenerateGrid();
        SpawnEnemies();
        //LaserGrid();
        ShieldGrid();
        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
        curMotion.curDirection = Vector3.left;
        curMotion.lockRowShift = false;
    }

    // Update is called once per frame
    void Update()
    {
        //moveCurrentWave();
    }
}
