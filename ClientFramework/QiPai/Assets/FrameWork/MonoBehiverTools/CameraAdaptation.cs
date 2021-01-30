using UnityEngine;

public class CameraAdaptation : MonoBehaviour
{
    private Camera bindCamera;

    void Awake()
    {
        bindCamera = gameObject.GetComponent<Camera>();
        if(bindCamera != null)
        {
            float aspect = Screen.width / Screen.height;
        }
    }
}