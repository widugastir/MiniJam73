using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Action<ResourceType, int> OnResourceChange;

    #region Player achivements
    public int BonusFightDices = 0;
    #endregion

    public enum ResourceType
    {
        Candles,
        Minions,
        PowerDise
    }

    private Dictionary<ResourceType, int> _resources = new Dictionary<ResourceType, int>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        _resources.Add(ResourceType.Candles, 0);
        _resources.Add(ResourceType.Minions, 0);
        _resources.Add(ResourceType.PowerDise, 0);
    }

    public void AddResource(ResourceType type, int amount)
    {
        print("Resource = " + type + " count = " + _resources[ResourceType.Candles]);
        if(_resources.ContainsKey(type))
        {
            _resources[type] += amount;
            OnResourceChange?.Invoke(type, amount);
        }
    }
    public int GetResource (ResourceType resource)
    {
        int amount = 0;
        amount = _resources[resource];
        return amount;
    }
}
