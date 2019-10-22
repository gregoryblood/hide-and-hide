using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondDisplay : MonoBehaviour
{
    private bool map = false;
    // Start is called before the first frame update
    void Update()
    {
        if (Display.displays.Length > 1)
        {
            if (Input.GetKeyDown("m"))
            {
                if (map)
                {
                    //Close Map?
                }
                else
                {
                    Display.displays[1].Activate(1920, 1080, 60);
                    map = true;
                }
            }
        }
        

        
    }

}
