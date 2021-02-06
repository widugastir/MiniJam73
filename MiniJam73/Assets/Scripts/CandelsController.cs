using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandelsController : MonoBehaviour
{
    [SerializeField] private GameObject [] _candles;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _failPanel;
    public void Start()
    {
        _candles[0].SetActive(true);
    }

    

    public void ChangeCountCandels (int countPower, bool availabilityDise)
    {
        print(countPower + " res");
        int counter = 0;
        for (int i = 0; i < _candles.Length; i++)
        {
            if(_candles[i].activeSelf == false && counter < countPower)
            {
                _candles[i].SetActive(true);
                counter += 1;
            }
            else if (_candles[i].activeSelf == false)
            {
                counter = 0;
                break;
            }
        }
        for (int i = 0; i < _candles.Length; i++)
        {
            if(_candles[i].activeSelf == false)
            {
                break;
            }
            if(_candles[i].activeSelf == true && i == _candles.Length - 1)
            {
                _winPanel.SetActive(true);
            }
        }
        for (int i = 0; i < _candles.Length; i++)
        {
            if(_candles[i].activeSelf == true && i == _candles.Length - 4)
            {
                _failPanel.SetActive(true);
            }
        }
    }

}
