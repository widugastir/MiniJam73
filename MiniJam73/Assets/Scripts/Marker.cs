using UnityEngine;

public class Marker : MonoBehaviour
{
    public void SetActivePanel(EventPanel panel)
    {
        panel.SelectedMarker = this;
        panel.gameObject.SetActive(true);
    }
}
