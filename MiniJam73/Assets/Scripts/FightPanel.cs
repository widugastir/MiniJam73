using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class FightPanel : MonoBehaviour
{   
    //[Header("Settings")]

    [Header("Referenes")]
    [SerializeField] private GraphicRaycaster _raycaster;
    [SerializeField] private GameObject _fightPanel;
    [SerializeField] private HorizontalLayoutGroup _playerLayourtGroup;
    [SerializeField] private Text _playerVictoryPoints;
    [SerializeField] private Text _enemyVictoryPoints;
    [SerializeField] private string _cubePosTag = "CubePos";
    [SerializeField] private Transform _enemyCubePos;
    [SerializeField] private Transform _playerCubePos;
    [SerializeField] private Transform _playerDicesParent;
    [SerializeField] private Transform _enemyDicesParent;
    [SerializeField] private List<Dice> _playerDices;
    [SerializeField] private List<Dice> _enemyDices;

    private List<Dice> _enemyActiveDices = new List<Dice>();
    private List<Dice> _playerActiveDices = new List<Dice>();

    public static System.Action<int> OnFightEnd;

    private int _playerScore = 0;
    private int _enemyScore = 0;
    private int _reward = 0;

    private bool readyToPlace = true;

    private Dice _playerDice;
    private Dice _enemyDice;

    private void OnEnable()
    {
        MapEntity.OnTargetArrived += StartFight;
        Dice.OnDicePlacement += OnDicePlacement;
    }

    private void OnDisable()
    {
        MapEntity.OnTargetArrived -= StartFight;
        Dice.OnDicePlacement -= OnDicePlacement;
    }

    public void StartFight(MapEntity entity, Marker marker)
    {
        _playerLayourtGroup.enabled = true;
        _fightPanel.SetActive(true);
        _reward = marker.StarCounter;
        readyToPlace = true;

        //Init player dices
        _playerActiveDices.Clear();
        for (int i = 0; i < _playerDices.Count; i++)
        {
            _playerDices[i].gameObject.SetActive(false);
            if (i < entity.DiceCount * 2)
            {
                _playerDices[i].gameObject.SetActive(true);
                _playerActiveDices.Add(_playerDices[i]);
            }
        }

        //Init enemy dices
        _enemyActiveDices.Clear();
        for (int i = 0; i < _enemyDices.Count; i++)
        {
            _enemyDices[i].gameObject.SetActive(false);
            if (i < marker.StarCounter * 2)
            {
                _enemyDices[i].gameObject.SetActive(true);
                _enemyActiveDices.Add(_enemyDices[i]);
            }
        }
    }

    public void EndFight()
    {
        _fightPanel.SetActive(false);
        if (_playerScore < _enemyScore)
            _reward = 0;
        OnFightEnd?.Invoke(_reward);
        print(_reward);
    }

    private void CompareDices()
    {
        if(_playerDice.Value > _enemyDice.Value)
        {
            _playerScore++;
        }
        else if(_playerDice.Value < _enemyDice.Value)
        {
            _enemyScore++;
        }
        else // equil
        {

        }
        _playerVictoryPoints.text = "" + _playerScore;
        _enemyVictoryPoints.text = "" + _enemyScore;
    }

    private void OnDicePlacement(Dice dice, PointerEventData eventData)
    {
        if (readyToPlace == false)
            return;

        PointerEventData m_PointerEventData = new PointerEventData(EventSystem.current);
        m_PointerEventData.position = eventData.position;
        List<RaycastResult> results = new List<RaycastResult>();
        _raycaster.Raycast(m_PointerEventData, results);
        _playerLayourtGroup.enabled = false;

        foreach (var r in results)
        {
            if(r.gameObject.CompareTag(_cubePosTag))
            {
                _playerDice = dice;
                _playerDice.transform.position = r.gameObject.transform.position;
                _playerActiveDices.Remove(_playerDice);
                Dice enemyDice = _enemyActiveDices[Random.Range(0, _enemyActiveDices.Count)];
                _enemyDice = enemyDice;
                _enemyActiveDices.Remove(_enemyDice);
                enemyDice.transform.position = _enemyCubePos.position;
                enemyDice.SetImageByValue();
                CompareDices();

                readyToPlace = false;
                Invoke(nameof(HideDices), 1f);
                break;
            }
        }
    }

    private void HideDices()
    {
        _playerDice.Disable();
        _enemyDice.Disable();
        readyToPlace = true;

        if (_enemyActiveDices.Count == 0 || _playerActiveDices.Count == 0)
        {
            EndFight();
        }
    }
}
