using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.1f;

    private SpriteRenderer sr;
    private Vector2 offset;
    private Material backgroundMaterial;
    private Camera mainCamera;
    private ScreenOrientation lastOrientation;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        backgroundMaterial = sr.material;
        offset = backgroundMaterial.mainTextureOffset;

        mainCamera = Camera.main;
        lastOrientation = Screen.orientation;

        FitBackgroundToScreen();
        RotateBackground();
    }

    void Update()
    {

        offset.x += scrollSpeed * Time.deltaTime;
        backgroundMaterial.mainTextureOffset = offset;

        if (Screen.orientation != lastOrientation)
        {
            lastOrientation = Screen.orientation;
            RotateBackground();
            FitBackgroundToScreen();
        }
    }

    void RotateBackground()
    {
        switch (Screen.orientation)
        {

            case ScreenOrientation.Portrait:
                transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                break;

            case ScreenOrientation.PortraitUpsideDown:
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                break;
        }
    }

    void FitBackgroundToScreen()
    {

        float screenHeight = mainCamera.orthographicSize * 2f;
        float screenWidth = screenHeight * mainCamera.aspect;

        float spriteWidth = sr.sprite.bounds.size.x;
        float spriteHeight = sr.sprite.bounds.size.y;

        bool rotated = Mathf.Abs(transform.eulerAngles.z % 180f) > 0.1f;

        float requiredWidth = rotated ? screenHeight : screenWidth;
        float requiredHeight = rotated ? screenWidth : screenHeight;

        float scale = Mathf.Max(requiredWidth / spriteWidth, requiredHeight / spriteHeight);

        transform.localScale = new Vector3(scale, scale, 1f);
    }
}