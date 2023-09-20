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

    public AnimationCurve slideSideways;

    public List<GameObject> currentWave = new List<GameObject>();
    bool isMoving = false;
    bool isSwitchingRow = false;
    BoxCollider2D LeftEdge;
    BoxCollider2D RightEdge;
    bool enemyLeft = false;

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
            var spawnedShield = Instantiate(_shieldPrefab, new Vector3(0.75f + (x * 2.37f), 0), Quaternion.identity);
            spawnedShield.name = $"Shield{x + 1}";
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
                    var spawnedEnemy = Instantiate(_enemies[i], new Vector3(x, (r - curRow)), Quaternion.identity);
                    spawnedEnemy.name = $"Enemy{i+1} {x} {r}";
                    currentWave.Add(spawnedEnemy);
                }
            }
            curRow += Rows;
        }

    }

    void moveCurrentWave()
    {
        if (!isMoving && !isSwitchingRow)
        {
            Vector3 eMove = new Vector3(((enemyLeft ? 1 : -1) * 0.1f), 0, 0);

            foreach (GameObject e in currentWave)
            {
                //e.transform.position = e.transform.position + (eMove * slideSideways.Evaluate(Time.time));
                //e.transform.position = new Vector3.Lerp(e.transform.position, e.transform.position + eMove, slideSideways.Evaluate(Time.time);
                e.transform.position += eMove;
            }
            print($"moved: {eMove}");
            StartCoroutine(enemyMoveTick(1.0f));
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isSwitchingRow && collision.gameObject.name != "ThePlayer")
        {
            Vector3 eRowMove = new Vector3(0, -0.5f, 0);

            foreach (GameObject e in currentWave)
            {
                e.transform.position += eRowMove;
            }
            print($"collided w {collision.gameObject.name} | sw. row: {eRowMove}");
            enemyLeft = !enemyLeft;

            StartCoroutine(enemyRowTick(1.0f));
        }
    }

    IEnumerator enemyMoveTick(float Tick)
    {
        isMoving = true;
        isSwitchingRow = false;
        yield return new WaitForSeconds(Tick);
        isMoving = false;
        isSwitchingRow = false;
    }

    IEnumerator enemyRowTick(float Tick)
    {
        isMoving = false;
        isSwitchingRow = true;
        yield return new WaitForSeconds(Tick);
        isMoving = false;
        isSwitchingRow = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //GenerateGrid();
        SpawnEnemies();
        LaserGrid();
        ShieldGrid();
        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
        LeftEdge = GameObject.Find("LeftEdge").GetComponent<BoxCollider2D>();
        RightEdge = GameObject.Find("RightEdge").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveCurrentWave();
    }
}
