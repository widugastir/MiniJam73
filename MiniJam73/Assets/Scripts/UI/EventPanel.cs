using UnityEngine;
using UnityEngine.UI;

public class EventPanel : MonoBehaviour
{
    [HideInInspector] public Marker SelectedMarker;
    [SerializeField] private Text _name;
    [SerializeField] private Text _comment;
    [HideInInspector] public Text [] _selectButtons;


    private void Start ()
    {
        
    }
    public void SetDataQest(QuestOption questOption, string questName)
    {
        _name.text = questName;
        _comment.text = questOption.Main;
        for(int i = 0; i < questOption.ButtunName.Length; i++)
        {
            _selectButtons[i].text = questOption.ButtunName[i];
        }
    }

    public void SetTargetMinion()
    {
        
    }

    



    /*#region ��������� �����������
    public void PressButton1(MapEntity entity)
    {
        entity.MoveTo(SelectedMarker);
    }
    #endregion*/
}
