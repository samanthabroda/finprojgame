using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiteboard : MonoBehaviour
{

    public Texture2D texture;
    public Vector2 textureDimentions = new Vector2(2048, 2048);

    // Start is called before the first frame update
    void Start()
    {
        var renderer = GetComponent<Renderer>();
        texture = new Texture2D((int)textureDimentions.x, (int)textureDimentions.y);
        renderer.material.mainTexture = texture;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
