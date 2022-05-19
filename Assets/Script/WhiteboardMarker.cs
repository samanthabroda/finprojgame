using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Code from this cs file is inspired and reused from: https://youtu.be/sHE5ubsP-E8 
public class WhiteboardMarker : MonoBehaviour
{

    [SerializeField] private Transform markerTip;
    [SerializeField] private int penSize = 5;


    private Renderer Renderer;
    private Color[] colors;
    private float tipHeight;


    private RaycastHit hit;
    private Whiteboard whiteboard;
    private Vector2 touchPosition, lastTouchPosition;
    private bool touchedLastFrame;
    private Quaternion lastTouchRot;

    // Start is called before the first frame update
    void Start()
    {
        Renderer = markerTip.GetComponent<Renderer>();
        colors = Enumerable.Repeat(Renderer.material.color, penSize * penSize).ToArray();
        tipHeight = markerTip.localScale.y;
        
    }

    // Update is called once per frame
    void Update()
    {
        Draw();
    }

    private void Draw()
    {
        if (Physics.Raycast(markerTip.position, transform.up, out hit, tipHeight))
        {
            if (hit.transform.CompareTag("Whiteboard"))
            {
                if (whiteboard == null)
                {
                    whiteboard = hit.transform.GetComponent<Whiteboard>();

                }
                touchPosition = new Vector2(hit.textureCoord.x, hit.textureCoord.y);

                var x = (int)(touchPosition.x * whiteboard.textureDimentions.x - (penSize / 2));
                var y = (int)(touchPosition.y * whiteboard.textureDimentions.y - (penSize / 2));

                if (y < 0 || y > whiteboard.textureDimentions.y || x < 0 || x > whiteboard.textureDimentions.x) return;

                if (touchedLastFrame)
                {
                    whiteboard.texture.SetPixels(x, y, penSize, penSize, colors);

                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(lastTouchPosition.x, x, f);
                        var lerpY = (int)Mathf.Lerp(lastTouchPosition.y, y, f);
                        whiteboard.texture.SetPixels(lerpX, lerpY, penSize, penSize, colors);
                    }

                    transform.rotation = lastTouchRot;

                    whiteboard.texture.Apply();

                }

                lastTouchPosition = new Vector2(x, y);
                lastTouchRot = transform.rotation;
                touchedLastFrame = true;
                return;
            }
        }

        whiteboard = null;
        touchedLastFrame=false;

    }
}
