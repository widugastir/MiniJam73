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

    public void ChangeCountCandels (int countPower)
    {
        for (int i = 0; i < _candles.Length; i++)
        {
            if(_candles[i].activeSelf == false && i <= countPower)
            {
                _candles[i].SetActive(true);
            }
            else if (_candles[i].activeSelf == false)
            {
                break;
            }
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.OnResourceChange += ChekcCurrentResource;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnResourceChange -= ChekcCurrentResource;
    }

    private void ChekcCurrentResource(GameManager.ResourceType resourceType, int amount)
    {
        if(GameManager.Instance.GetResource(GameManager.ResourceType.Candles) < 9 
        && GameManager.Instance.GetResource(GameManager.ResourceType.Minions) == 0 
        && GameManager.Instance.GetResource(GameManager.ResourceType.PowerDise) == 12)
            _failPanel.SetActive(true);
        if(GameManager.Instance.GetResource(GameManager.ResourceType.Candles) == 9)
            _winPanel.SetActive(true);
        if(GameManager.ResourceType.Candles == resourceType) 
            ChangeCountCandels(GameManager.Instance.GetResource(GameManager.ResourceType.Candles));
    }

}
