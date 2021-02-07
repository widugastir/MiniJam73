using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandelsController : MonoBehaviour
{
    [SerializeField] private GameObject [] _candles;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _failPanel;
    [SerializeField] private Image _background;
    [SerializeField] private Sprite [] _backgroundSprites;
    public void Start()
    {
        
    }

    public void ChangeCountCandels (int countPower)
    {
        for (int i = 0; i < _candles.Length; i++)
        {
            if(_candles[i].activeSelf == false && i < countPower ) 
            {
                ActivatedAnimations (i);
                _candles[i].SetActive(true);
            }
            else if (_candles[i].activeSelf == false)
            {
                break;
            }
        }
    }
    private IEnumerator ActivatedAnimations (int index)
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 1.5f));
         _candles[index].GetComponent<Animation>().enabled = true;
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
        && GameManager.Instance.GetResource(GameManager.ResourceType.Minions) <= 0 
        && GameManager.Instance.GetResource(GameManager.ResourceType.PowerDise) >= 12)
            _failPanel.SetActive(true);
        if(GameManager.Instance.GetResource(GameManager.ResourceType.Candles) >= 9)
            _winPanel.SetActive(true);
        if(GameManager.ResourceType.Candles == resourceType) 
            ChangeCountCandels(GameManager.Instance.GetResource(GameManager.ResourceType.Candles));
        if(GameManager.Instance.GetResource(GameManager.ResourceType.Candles) > 0) _background.sprite = _backgroundSprites[0];
        if(GameManager.Instance.GetResource(GameManager.ResourceType.Candles) > 3) _background.sprite = _backgroundSprites[1];
        if(GameManager.Instance.GetResource(GameManager.ResourceType.Candles) > 5) _background.sprite = _backgroundSprites[2];
    }

}
