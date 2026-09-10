using UnityEngine;

public class CameraOrthographicSize : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Awake
    private void Awake()
    {
        float screenAspectRatio = (float)Screen.width / Screen.height;
        float orthographicSize = (6 - (screenAspectRatio - 0.485f) * 11f);
        if(orthographicSize<4)
        {
            orthographicSize = 4;
        }
        Camera.main.orthographicSize = orthographicSize;


    }
}
