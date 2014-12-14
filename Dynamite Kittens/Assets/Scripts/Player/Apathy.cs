using UnityEngine;
using System.Collections;

public class Apathy : MonoBehaviour {

    public int ApathyLevel;

    void OnGUI()
    {
        Rect rect = Camera.main.pixelRect;
        float screenWidth = rect.width;
        rect.width *= 0.1f;
        rect.height *= 0.1f;
        rect.x = screenWidth - rect.width;

        GUI.TextArea(rect, "Apathy Level: " + ApathyLevel);
    }
}
