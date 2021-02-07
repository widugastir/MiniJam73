using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class Dice : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Sprite[] _spritesValue;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private bool _playerOwner = false;
    [HideInInspector] public int Value = -1;
    private Image _image;
    private Animator _animator;

    public static System.Action<Dice, PointerEventData> OnDicePlacement;
    public static System.Action<Dice> OnDiceRoll;

    public void Start()
    {
        _animator = GetComponent<Animator>();
        _image = GetComponent<Image>();
        if (_playerOwner == false)
        {
            GetComponent<Button>().interactable = false;
        }
    }

    public void Disable(bool disableObject = true)
    {
        if(_image == null)
            _image = GetComponent<Image>();

        if(disableObject)
            gameObject.SetActive(false);
        Value = -1;
        _image.sprite = _defaultSprite;
    }

    public void Roll(bool updateSprite = true)
    {
        if (Value == -1)
        {
            Value = DiceController.RollD6();
            if(updateSprite)
                SetImageByValue();
            OnDiceRoll?.Invoke(this);
            MusicManager.Instance.PlaySound(2);
        }
    }

    public void RollWithAnim()
    {
        if (Value == -1)
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();
            Value = DiceController.RollD6();
            _animator.SetTrigger("Roll");
            OnDiceRoll?.Invoke(this);
            MusicManager.Instance.PlaySound(2);
        }
    }

    public void OnMiddleRollAnimationEvent()
    {
        SetImageByValue();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_playerOwner == false || Value != -1) return;
        RollWithAnim();
        OnDicePlacement?.Invoke(this, eventData);
    }

    public void SetImageByValue()
    {
        if(_image == null)
            _image = GetComponent<Image>();
        if (Value != -1)
            _image.sprite = _spritesValue[Value - 1];
        else
            _image.sprite = _defaultSprite;
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
