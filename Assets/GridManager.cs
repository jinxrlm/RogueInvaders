using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;

    [SerializeField] private GameObject _tilePrefab;

    [SerializeField] private Transform _cam;

    [SerializeField] private GameObject[] _enemies;

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
        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
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
                }
            }
            curRow += Rows;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
