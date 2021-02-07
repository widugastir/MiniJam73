using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class FightPanel : MonoBehaviour
{   
    [Header("Referenes")]
    [SerializeField] private GraphicRaycaster _raycaster;
    [SerializeField] private GameObject _fightPanel;
    [SerializeField] private GameObject _randomDicePanel;
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
    [SerializeField] private Dice _randomDice;
    [SerializeField] private Text _randomDiceText1;
    [SerializeField] private Text _randomDiceText2;
    [SerializeField] private Text _resultText;
    [SerializeField] private Text _infoText;

    private List<Dice> _enemyActiveDices = new List<Dice>();
    private List<Dice> _enemyBestDices = new List<Dice>();
    private List<Dice> _playerActiveDices = new List<Dice>();

    public static System.Action<int> OnFightEnd;

    private int _playerScore = 0;
    private int _enemyScore = 0;
    private int _reward = 0;

    private bool readyToPlace = true;

    private Dice _playerDice;
    private Dice _enemyDice;

    private MapEntity _minion;
    private Marker _marker;

    private void OnEnable()
    {
        MapEntity.OnTargetArrived += StartFight;
        Dice.OnDicePlacement += OnDicePlacement;
        Dice.OnDiceRoll += OnDiceRoll;
    }

    private void OnDisable()
    {
        MapEntity.OnTargetArrived -= StartFight;
        Dice.OnDicePlacement -= OnDicePlacement;
        Dice.OnDiceRoll -= OnDiceRoll;
    }

    public void StartFight(MapEntity entity, Marker marker)
    {
        if (marker.StarCounter == 0)
            return;

        _marker = marker;
        _minion = entity;

        InitFight();
        InitPlayerDices();
        InitEnemyDices();
        ApplyEnemyAbilities();
    }

    private void ApplyMarkerBonus()
    {

    }

    private void InitFight()
    {
        _enemyCubePos.gameObject.SetActive(true);
        _playerCubePos.gameObject.SetActive(true);
        _fightPanel.SetActive(true);
        _resultText.text = "";
        Time.timeScale = 0f;
        _playerLayourtGroup.enabled = true;
        _reward = _marker.StarCounter;
        readyToPlace = true;
        _randomDice.Disable(false);
        _playerScore = 0;
        _enemyScore = 0;
        _enemyBestDices.Clear();
        _playerVictoryPoints.text = "" + _playerScore;
        _enemyVictoryPoints.text = "" + _enemyScore;
    }

    private void InitPlayerDices()
    {
        _playerActiveDices.Clear();
        for (int i = 0; i < _playerDices.Count; i++)
        {
            _playerDices[i].Disable();
            _playerDices[i].gameObject.SetActive(false);
            if (i < _minion.DiceCount * 2 + GameManager.Instance.BonusFightDices)
            {
                _playerDices[i].gameObject.SetActive(true);
                _playerActiveDices.Add(_playerDices[i]);
            }
        }
    }

    private void InitEnemyDices()
    {
        _enemyActiveDices.Clear();
        for (int i = 0; i < _enemyDices.Count; i++)
        {
            _enemyDices[i].gameObject.SetActive(false);
            if (i < _marker.StarCounter * 2)
            {
                _enemyDices[i].gameObject.SetActive(true);
                _enemyDices[i].Roll();
                _enemyActiveDices.Add(_enemyDices[i]);
            }
        }
        SortEnemyDicesPull();
    }

    private void SortEnemyDicesPull()
    {
        _enemyBestDices = _enemyActiveDices;
        if (_enemyBestDices.Count > _playerActiveDices.Count)
        {
            _enemyBestDices.Sort(SortDices);
            _enemyBestDices.RemoveRange(_playerActiveDices.Count, (_enemyBestDices.Count - _playerActiveDices.Count));
        }
    }

    private void ApplyEnemyAbilities()
    {
        switch(_marker.Ability)
        {
            case Marker.MarkerAbility.reroll1s:
                _infoText.text = "Fight effect:\nenemy reroll 1's dices";
                StartCoroutine(EnemyReroll1sDices(0.5f));
                break;
            case Marker.MarkerAbility.none:
            default:
            _infoText.text = "";
                break;
        }
    }

    private IEnumerator EnemyReroll1sDices(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        foreach (Dice dice in _enemyActiveDices)
        {
            if (dice.Value == 1)
            {
                dice.Value = -1;
                dice.RollWithAnim();
            }
        }
        SortEnemyDicesPull();
    }

    public void RollAllDices()
    {
        StartCoroutine(RollDiceWithDelay());
    }

    private IEnumerator RollDiceWithDelay()
    {
        for (int i = 0; i < _playerActiveDices.Count; i++)
        {
            _playerActiveDices[i].RollWithAnim();
            yield return new WaitForSecondsRealtime(Random.Range(0.1f,0.3f));
        }
    }

    private int SortDices(Dice d1, Dice d2)
    {
        if(d1.Value < d2.Value)
        {
            return 1;
        }
        else if (d1.Value > d2.Value)
        {
            return -1;
        }
        return 0;
    }

    public void EndFight()
    {
        _enemyCubePos.gameObject.SetActive(false);
        _playerCubePos.gameObject.SetActive(false);
        if (_playerScore < _enemyScore)
        {
            _reward = 0;
            _resultText.text = "Lose";
        }
        else
        {
            _resultText.text = $"Victory:\nCandles + {_reward}";
        }
        OnFightEnd?.Invoke(_reward);
        Time.timeScale = 1f;
        if(_reward > 0)
        {
            _marker.ChangeStars();
            _marker.StarCounter = 0;
        }
        _minion.Candles += _reward;
        _minion.DiceCount = _playerActiveDices.Count / 2;
        if(_minion.DiceCount == 0)
        {
            _minion.BackHome();
        }
        _randomDicePanel.SetActive(false);
        ApplyMarkerBonus();
        StartCoroutine(ClosePanelWithDelay(2f));
    }

    private IEnumerator ClosePanelWithDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        _fightPanel.SetActive(false);
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
        _playerVictoryPoints.text = "" + _playerScore;
        _enemyVictoryPoints.text = "" + _enemyScore;
    }

    private void OnDiceRoll(Dice dice)
    {
        if (_randomDicePanel.activeSelf)
        {
            if (dice.Value <= 3)
            {
                _enemyScore++;
                _randomDiceText1.text = "Lose";
                _randomDiceText2.gameObject.SetActive(false);
            }
            else
            {
                _playerScore++;
                _randomDiceText1.gameObject.SetActive(false);
                _randomDiceText2.text = "Victory";
            }
            StartCoroutine(LateEndFight());
        }
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

                /*
                _playerActiveDices.Remove(_playerDice);
                Dice enemyDice = _enemyBestDices[Random.Range(0, _enemyBestDices.Count)];
                _enemyDice = enemyDice;
                _enemyBestDices.Remove(_enemyDice);
                enemyDice.transform.position = _enemyCubePos.position;
                enemyDice.SetImageByValue();*/
                EnemySelectDice();
                CompareDices();

                readyToPlace = false;
                StartCoroutine(HideDices());
                break;
            }
        }
    }

    private IEnumerator LateEndFight()
    {
        yield return new WaitForSecondsRealtime(3f);
        EndFight();
    }

    private void EnemySelectDice()
    {
        _playerActiveDices.Remove(_playerDice);
        Dice enemyDice = _enemyBestDices[Random.Range(0, _enemyBestDices.Count)];
        _enemyDice = enemyDice;
        _enemyBestDices.Remove(_enemyDice);
        enemyDice.transform.position = _enemyCubePos.position;
        enemyDice.SetImageByValue();
    }

    private void EnableRandomVictory()
    {
        _randomDiceText1.text = "1-3: Lose";
        _randomDiceText2.text = "4-5: Victory";
        _randomDiceText1.gameObject.SetActive(true);
        _randomDiceText2.gameObject.SetActive(true);

        _randomDicePanel.SetActive(true);
    }

    private IEnumerator HideDices()
    {
        yield return new WaitForSecondsRealtime(1f);
        _playerDice.Disable();
        _enemyDice.Disable();
        readyToPlace = true;

        if (_enemyActiveDices.Count == 0 || _playerActiveDices.Count == 0)
        {
            if(_enemyScore == _playerScore)
            {
                EnableRandomVictory();
            }
            else EndFight();
        }
    }
}
