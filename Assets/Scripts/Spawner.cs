using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _template;
    [SerializeField] private float _spawnTime;

    private Transform[] _points;
    private Vector3[] _directions;
    private bool _isWork = true;
    private int _minValue = 0;

    private void Awake()
    {
        _points = new Transform[gameObject.transform.childCount];
        _directions = new Vector3[_points.Length];

        for (int i = 0; i < _directions.Length; i++)
        {
            _points[i] = gameObject.transform.GetChild(i);
            _directions[i] = RandomDirection();
        }
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds time = new(_spawnTime);
        int randomPointNumber;

        while (_isWork)
        {
            randomPointNumber = Random.Range(_minValue, _points.Length);
            Enemy enemy = Instantiate(_template, _points[randomPointNumber].transform.position, Quaternion.identity);
            enemy.SetDirection(_directions[randomPointNumber]);

            yield return time;
        }
    }

    private Vector3 RandomDirection()
    {
        float _minRotation = -180;
        float _maxRotation = 180;

        return new Vector3(Random.Range(_minRotation, _maxRotation), _minValue, Random.Range(_minRotation, _maxRotation));
    }
}
