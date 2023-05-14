using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class ColorPicker : MonoBehaviour
{
    [SerializeField]
    private Texture2D _texture;
    [SerializeField]
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _texture = _renderer.material.mainTexture as Texture2D;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        //transform.eulerAngles += new Vector3(2 * Time.deltaTime, 2 * Time.deltaTime, 0);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector2 pixelUV = hit.textureCoord;
            pixelUV.x *= _texture.width;
            pixelUV.y *= _texture.height;

            int radius = 10;
            int centerX = (int) pixelUV.x;
            int centerY = (int) pixelUV.y;

            // Loop through all the pixels in a square box around the circle
            for (int x = centerX - radius; x <= centerX + radius; x++)
            {
                for (int y = centerY - radius; y <= centerY + radius; y++)
                {
                    // Calculate the distance from the current pixel to the center of the circle
                    float distance = Mathf.Sqrt(Mathf.Pow(x - centerX, 2) + Mathf.Pow(y - centerY, 2));

                    // If the distance is less than or equal to the radius, draw the pixel
                    if (distance <= radius)
                    {
                        // Draw the pixel at (x,y)
                        // (Assuming you have a canvas or screen object to draw on)
                        _texture.SetPixel(x, y, GameManager.Instance.CollectableColor);
                    }
                }
            }

            _texture.Apply();
        }
    }
}