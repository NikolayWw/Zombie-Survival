using UnityEngine;

public class TestScreenShoot : MonoBehaviour
{
    [ContextMenu("Run")]
    private void Screen()
    {
        ScreenCapture.CaptureScreenshot("1.png");
    }
}