using System.Collections.Generic;
using UnityEngine;

public class FightPanel : MonoBehaviour
{
    [SerializeField] private Transform _playerDicesParent;
    [SerializeField] private Transform _enemyDicesParent;
    [SerializeField] private GameObject _dicePrefab;

    //private List<Dice> _playerDices = new List<Dice>();
    //private List<Dice> _enemyDices = new List<Dice>();

    public void StartFight()
    {
        gameObject.SetActive(true);
    }
}
