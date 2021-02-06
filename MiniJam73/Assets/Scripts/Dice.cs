using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class Dice : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Sprite[] _spritesValue;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private bool _playerOwner = false;
    private Image _image;
    [HideInInspector] public int Value = -1;

    public static System.Action<Dice, PointerEventData> OnDicePlacement;

    public void Start()
    {
        _image = GetComponent<Image>();
        if (_playerOwner == false)
        {
            Value = DiceController.RollD6();
            GetComponent<Button>().interactable = false;
        }
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        Value = -1;
        _image.sprite = _defaultSprite;
    }

    public void Roll()
    {
        if (_playerOwner == false) return;
        if (Value == -1)
        {
            Value = DiceController.RollD6();
            SetImageByValue();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_playerOwner == false || Value != -1) return;
        Roll();
    }

    public void SetImageByValue()
    {
        _image.sprite = _spritesValue[Value - 1];
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_playerOwner == false) return;
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_playerOwner == false) return;
        OnDicePlacement?.Invoke(this, eventData);
    }
}
