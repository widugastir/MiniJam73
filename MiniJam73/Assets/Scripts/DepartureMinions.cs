using UnityEngine;
using UnityEngine.UI;

public class DepartureMinions : MonoBehaviour
{
    private int _diceCount;
    private int _getDiceCount;
    [SerializeField] private GameObject _perfab;
    [SerializeField] private GameObject _map;
    [SerializeField] private Marker _marker;
    [SerializeField] private GameObject _areaSpawn;
    [SerializeField] private Transform _pathChunksParent;
    [SerializeField] private Image [] Dices;
    [SerializeField] private Sprite [] _sprite;

    public void Departure()
    {
        if (GameManager.Instance.GetResource(GameManager.ResourceType.Minions) >= 2)
        {
            UIManager.Inctance.SetMessagePanel("You can only have two minions at the same time!");
            return;
        }
        for (int i = Dices.Length - 1; i >= 0; i--)
        {
            if(Dices[i].sprite == _sprite[1])
            {
                Dices[i].sprite = _sprite[2];
            }
        }
        GameManager.Instance.AddResource(GameManager.ResourceType.Minions, 1);
        GameManager.Instance.AddResource(GameManager.ResourceType.PowerDise, _diceCount);
        _getDiceCount = _diceCount;
        _diceCount = 0;
        if(_getDiceCount > 0)
        {
            var entityObject = Instantiate(_perfab, _marker.gameObject.transform.position + (Vector3)Random.insideUnitCircle * 100f, Quaternion.identity, _areaSpawn.transform);
            var entity = entityObject.GetComponent<MapEntity>();
            entity.DiceCount = _getDiceCount;
            entity.CurrentMarker = _marker;
            SelectionMinions.Instance.PullEntity.Add(entityObject);
            SelectionMinions.Instance.SetHighlighted(entityObject);
            entityObject.AddComponent<Button>().onClick.AddListener(
                delegate 
                { 
                    SelectionMinions.Instance.SetHighlighted(entityObject); 
                });
            entity._pathChunksParent = _pathChunksParent;
            UIManager.Inctance.EnablePanel(_map);
        }

    }
    public void GetDicePower (bool get)
    {
        if(get && _diceCount < 4)
        {
            _diceCount += 1;
            for (int i = 0; i < Dices.Length; i++)
            {
                if(Dices[i].sprite == _sprite[0])
                {
                    Dices[i].sprite = _sprite[1];
                    break;
                }
            }
        }
        else if(!get)
        {
            if(_diceCount > 0) _diceCount -= 1;
            for (int i = Dices.Length - 1; i >= 0; i--)
            {
                if(Dices[i].sprite == _sprite[1])
                {
                    Dices[i].sprite = _sprite[0];
                    break;
                }
            }
        }
    }
}
