using UnityEngine;
using UnityEngine.UI;

public class DepartureMinions : MonoBehaviour
{
    private int _diceCount;
    private int _getDiceCount;
    [SerializeField] private GameObject _perfab;
    [SerializeField] private Marker _marker;
    [SerializeField] private GameObject _areaSpawn;
    [SerializeField] private Transform _pathChunksParent;
    [SerializeField] private Image [] Dices;
    private void Start()
    {
        
    }

    public void Departure()
    {
        for (int i = Dices.Length - 1; i >= 0; i--)
        {
            if(Dices[i].color == new Color(0.3f, 0.3f, 0.3f))
            {
                Dices[i].color = new Color(0f, 0f, 0f);
            }
        }
        _getDiceCount = _diceCount;
        _diceCount = 0;
        var obj = Instantiate(_perfab, _marker.gameObject.transform.position, Quaternion.identity, _areaSpawn.transform);
        var me = obj.GetComponent<MapEntity>();
        me.DiceCount = _getDiceCount;
        me.CurrentMarker = _marker;
        //obj.AddComponent<Button>().onClick.AddListener(() => print ("true"));
        obj.AddComponent<Button>().onClick.AddListener(
            delegate 
            { 
                SelectionMinions.Instance.Selected(obj); 
            });
        me._pathChunksParent = _pathChunksParent;

    }
    public void GetDicePower (bool get)
    {
        if(get && _diceCount < 4)
        {
            _diceCount += 1;
            for (int i = 0; i < Dices.Length; i++)
            {
                if(Dices[i].color == new Color(0.9f, 0.9f, 0.9f))
                {
                    Dices[i].color = new Color(0.3f, 0.3f, 0.3f);
                    break;
                }
            }
        }
        else if(!get)
        {
            _diceCount -= 1;
            for (int i = Dices.Length - 1; i >= 0; i--)
            {
                if(Dices[i].color == new Color(0.3f, 0.3f, 0.3f))
                {
                    Dices[i].color = new Color(0.9f, 0.9f, 0.9f);
                    break;
                }
            }
        }
        
    }
}
