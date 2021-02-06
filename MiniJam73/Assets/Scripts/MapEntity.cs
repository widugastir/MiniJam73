using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class MapEntity : MonoBehaviour
{
    [Header("Settings")]
    public Marker CurrentMarker;
    public float MoveWaving = 25f;

    [Header("References")]
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _pathChunkStep = 0.3f;
    [SerializeField] private GameObject _pathChunkPrefab;
    [HideInInspector] public Transform _pathChunksParent;
    public bool Selected = false;

    public int DiceCount;
    public int Candles = 0;

    public static System.Action<MapEntity, Marker> OnTargetArrived;

    private List<GameObject> _path = new List<GameObject>();
    private Marker _homeMarker;
    private Marker _moveTarget;
    private Vector3 direction;
    private bool _movingHome;

    private void Start()
    {
        _homeMarker = CurrentMarker;
    }

    private void OnEnable()
    {
        if(_moveTarget != null)
        {
            StartCoroutine(Moving());
        }
    }

    public void Kill()
    {
        SelectionMinions.Instance.PullEntity.Remove(gameObject);
        GameManager.Instance.AddResource(GameManager.ResourceType.Minions, -1);
        Destroy(gameObject);
    }

    public void MoveTo(Marker marker)
    {
        if (_moveTarget != null)
            return;
        _moveTarget = marker;
        direction = Vector3.Normalize(_moveTarget.transform.position - transform.position);
        StartCoroutine(Moving());
    }

    public void BackHome()
    {
        _movingHome = true;
        MoveTo(_homeMarker);
    }

    private IEnumerator Moving()
    {
        while(Vector3.Distance(_moveTarget.transform.position, transform.position) > _pathChunkStep)
        {
            direction = Vector3.Normalize(_moveTarget.transform.position - transform.position);
            transform.position += direction * _pathChunkStep + (Vector3)Random.insideUnitCircle * MoveWaving;
            DrawPath();
            yield return new WaitForSeconds(_moveSpeed);
        }
        TargetArrived();
    }

    private void TargetArrived()
    {
        transform.position = _moveTarget.transform.position;
        foreach (var chunk in _path)
            Destroy(chunk);
        _path.Clear();

        if (_movingHome)
        {
            GameManager.Instance.AddResource(GameManager.ResourceType.Candles, Candles);
            Kill();
        }
        else
        {
            CurrentMarker = _moveTarget;
            OnTargetArrived?.Invoke(this, _moveTarget);
            _moveTarget = null;
        }
    }

    private void DrawPath()
    {
        GameObject pathChunk = Instantiate(_pathChunkPrefab, transform.position, Quaternion.identity, _pathChunksParent);
        _path.Add(pathChunk);
    }
    
}
