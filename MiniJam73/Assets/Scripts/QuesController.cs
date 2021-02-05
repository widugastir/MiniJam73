using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class QuesController : MonoBehaviour
{
    [SerializeField]
    private GameObject _questPanrl;
    [SerializeField]
    private GameObject _targetPanrl;
    private SOQuest _currentQuest;
    private EventPanel _eventPanel;
    private QuestOption _lastOption;
    private DiceController _diceController;
    private int _diceResult;

    
    private void Start()
    {
        _diceController = GetComponent<DiceController>();
        _eventPanel = _questPanrl.GetComponent<EventPanel>();
    }

    public void SetQuest (SOQuest quest)
    {
            _currentQuest = quest;
    }

    private void ChengeCurrentQuest(MapEntity entity)
    {
       _questPanrl.SetActive(true);
       _lastOption = _currentQuest.QuestOptions[0];
       _eventPanel.SetDataQest(_lastOption, _currentQuest.NameEvent);
    }

    private void OnEnable()
    {
        MapEntity.OnTargetArrived += ChengeCurrentQuest;
    }

    private void OnDisable()
    {
        MapEntity.OnTargetArrived -= ChengeCurrentQuest;
    }
#region Problem
    public void EventInteraction(int index)
    {
        if(_lastOption.Exit) //проверка на необходимость закрытия меню
        {
            Debug.Log("_lastOption.Exit");
            for(int t = 0; t < _eventPanel._selectButtons.Length; t++)
            {
                if(t < _lastOption.ButtunName.Length) 
                {
                    _eventPanel._selectButtons[t].GetComponentInParent<Button>().interactable = true;
                    _eventPanel._selectButtons[t].gameObject.SetActive(true);
                }
                else
                {
                    _eventPanel._selectButtons[t].gameObject.SetActive(false);
                    _eventPanel._selectButtons[t].GetComponentInParent<Button>().interactable = false;
                }
            }
            _questPanrl.SetActive(false);
        }
        else //логика перехода в наружу
        {
            for(int t = 0; t < _eventPanel._selectButtons.Length; t++)
            {
                if(t < _lastOption.Options[index].Options.Length) 
                {
                    _eventPanel._selectButtons[t].GetComponentInParent<Button>().interactable = true;
                    _eventPanel._selectButtons[t].gameObject.SetActive(true);
                }
                else
                {
                    _eventPanel._selectButtons[t].gameObject.SetActive(false);
                    _eventPanel._selectButtons[t].GetComponentInParent<Button>().interactable = false;
                }
                
            }
            if(_lastOption.Options[index].Exit) //создаем кнопку выхода
            {
                _eventPanel._selectButtons[0].GetComponentInParent<Button>().interactable = true;
                _eventPanel._selectButtons[0].gameObject.SetActive(true);
                Debug.Log("_lastOption.Options[index].Exit");
            }
            if(_lastOption.Options[index].Check)
            {
                if(RoolDice(_lastOption.Options[index].CheckValue, (int)_lastOption.Options[index].Dice))
                {
                    _eventPanel.SetDataQest(_lastOption.Options[index], _currentQuest.NameEvent);
                    _lastOption = _lastOption.Options[index];
                    Debug.Log("Good");
                }
                else
                {
                    /*for(int i = 0; i < _lastOption.Options.Length; i++)
                    {
                        if(_lastOption.Options[i].Exit) 
                        {
                            _lastOption = _lastOption.Options[i];
                            Debug.Log("_lastOption.Options[index].Exit");
                        }
                    }*/
                    Debug.Log("Bad");
                    _eventPanel._selectButtons[index].GetComponentInParent<Button>().interactable = false;
                }
            }
            else
            {
                _eventPanel.SetDataQest(_lastOption.Options[index], _currentQuest.NameEvent);
                _lastOption = _lastOption.Options[index];
            }
            
        }
    }
#endregion

    public bool RoolDice (int ckeckValue, int maxValue)
    {
        bool result = false;
        int value = _diceController.Result(maxValue);
        if(value >= ckeckValue) result = true;
        return result;
    }

}

/////////////////////////////////////////////////////////////


[System.Serializable]
public struct QuestOption
{
    public enum Resources
    {
        DarkPower,
        Hero,
        Minions
    }

    [TextArea()] public string Main;
    [TextArea()] public string [] ButtunName;
    public bool Check;
    public bool Exit;
    public int CheckValue;
    public QuestOption [] Options;
    public TypeDice Dice;
    public enum TypeDice
    {
        D6 = 6,
        D8 = 8,
        D10 = 10,
        D12 = 12,
        D20 = 20,
        D100 = 100
    }

}
