using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Action<ResourceType, int> OnResourceChange;

    public enum ResourceType
    {
        DarkPower,
        DungeonPower,
        Minions
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
        _resources.Add(ResourceType.DarkPower, 0);
        _resources.Add(ResourceType.DungeonPower, 0);
        _resources.Add(ResourceType.Minions, 0);
    }

    public void AddResource(ResourceType type, int amount)
    {
        if(_resources.ContainsKey(type))
        {
            _resources[type] += amount;
            OnResourceChange?.Invoke(type, amount);
        }
    }
}
