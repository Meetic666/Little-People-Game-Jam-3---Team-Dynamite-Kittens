using UnityEngine;
using System.Collections;

public class Apathy : MonoBehaviour {

    public int ApathyLevel;

    void OnGUI()
    {
        Rect rect = Camera.main.pixelRect;
        rect.width *= 0.1f;
        rect.height *= 0.1f;
        rect.x += rect.width;

        GUI.TextArea(rect, "Apathy Level: " + ApathyLevel);
    }
}
