using UnityEngine;

public class TestScreenShoot : MonoBehaviour
{
    private GameObject _asd;

    [ContextMenu("Run")]
    private void Screen()
    {
        ScreenCapture.CaptureScreenshot("1.png");
    }

    //private void Update()
    //{
    //    if (_asd == null)
    //        _asd = GameObject.Find("MinimapCamera");
    //    else
    //    {
    //        Debug.Log("drow");
    //        Debug.DrawRay(_asd.transform.position, Vector3.down * 5555f, Color.red);
    //    }
    //}
}