using UnityEngine;

public class EventPanel : MonoBehaviour
{
    [HideInInspector] public Marker SelectedMarker;

    #region ��������� �����������
    public void PressButton1(MapEntity entity)
    {
        entity.MoveTo(SelectedMarker);
    }
    #endregion
}
