using UnityEngine;

public class Marker : MonoBehaviour
{
    public void SetActivePanel(EventPanel panel)
    {
        panel.gameObject.SetActive(true);
    }
}
