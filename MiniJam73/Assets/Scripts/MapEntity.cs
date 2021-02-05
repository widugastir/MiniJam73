using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MapEntity : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Marker _currentMarker;

    [Header("References")]
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _pathChunkStep = 0.3f;
    [SerializeField] private GameObject _pathChunkPrefab;
    [SerializeField] private Transform _pathChunksParent;

    public static System.Action<MapEntity> OnTargetArrived;

    private List<GameObject> _path = new List<GameObject>();
    private Marker _moveTarget;
    private Vector3 direction;

    public void MoveTo(Marker marker)
    {
        if (_moveTarget != null)
            return;
        _moveTarget = marker;
        direction = Vector3.Normalize(_moveTarget.transform.position - _currentMarker.transform.position);
        StartCoroutine(Moving());
    }

    private IEnumerator Moving()
    {
        while(Vector3.Distance(_moveTarget.transform.position, transform.position) > _pathChunkStep)
        {
            transform.position += direction * _pathChunkStep;
            DrawPath();
            yield return new WaitForSeconds(_moveSpeed);
        }
        TargetArrived();
    }

    private void TargetArrived()
    {
        transform.position = _moveTarget.transform.position;
        _currentMarker = _moveTarget;
        _moveTarget = null;
        foreach (var chunk in _path)
            Destroy(chunk);
        _path.Clear();
        OnTargetArrived?.Invoke(this);
    }

    private void DrawPath()
    {
        GameObject pathChunk = Instantiate(_pathChunkPrefab, transform.position, Quaternion.identity, _pathChunksParent);
        _path.Add(pathChunk);
    }
}