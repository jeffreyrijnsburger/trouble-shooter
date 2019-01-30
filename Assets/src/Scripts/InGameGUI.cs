using UnityEngine;

// Tip: Got crosshair from https://forum.unity.com/threads/where-to-put-ongui-function.16444/ and https://answers.unity.com/questions/201103/c-crosshair.html
public class InGameGUI : MonoBehaviour
{
    public Texture2D crosshairImage;
    public Texture2D healthImage;

    void OnGUI()
    {
        // Draw crosshair. JR
        var xMin = Screen.width / 2 - crosshairImage.width / 2;
        var yMin = Screen.height / 2 - crosshairImage.height / 2;

        GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);

        // Draw health. JR
        var player = GetComponent<PlayerScript>();
        var healthFontStyle = new GUIStyle
        {
            fontSize = 32,
            alignment = TextAnchor.MiddleLeft,
            normal = { textColor = Color.white }
        };

        GUI.DrawTexture(new Rect(32, 32, healthImage.width, healthImage.height), healthImage);
        GUI.Label(new Rect(72, 36, 28, 20), player.Health.ToString(), healthFontStyle);
    }
}
