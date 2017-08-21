using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class berry : MonoBehaviour
{

    public int x;
    public int y;
    public int z;



	// Update is called once per frame
	void Update ()
    {

        transform.Rotate(x,y,z); 
        	
	}



}
