using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform _pointContainer;
    [SerializeField] private float _speed;

    private Transform[] _points;
    private int _currentPointIndex;

    private void Start()
    {
        _points = new Transform[_pointContainer.childCount];

        for (int i = 0; i < _points.Length; i++)
            _points[i] = _pointContainer.GetChild(i);
    }

    private void Update()
    {
        Transform target = _points[_currentPointIndex];

        transform.position =
            Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (transform.position == target.position)
            ChangeNextPlace();
    }

    private void ChangeNextPlace()
    {
        _currentPointIndex = ++_currentPointIndex % _points.Length;
    }
}