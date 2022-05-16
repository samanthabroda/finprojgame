using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFlow_Sink : MonoBehaviour
{

    public GameObject WatergameObject;

    int i = 0;
    public int flowSpeed = 70;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        i++;
        if (i % flowSpeed == 0 )
        {
            Instantiate(WatergameObject);
            i = 0;
        }
        
        
    }
}
