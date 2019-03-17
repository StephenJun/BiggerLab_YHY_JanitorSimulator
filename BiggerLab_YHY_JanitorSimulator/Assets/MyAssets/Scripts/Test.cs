using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {

        PrintTest("Peter", 6);
	}
	
	// Update is called once per frame
	void Update () {
       // transform.Rotate(Vector3.up);
    }



    public void PrintTest(string name)
    {
        print(name);
    }
    public void PrintTest()
    {
        print("YHY");
    }
    public void PrintTest(string name, int num)
    {
        print(name + num.ToString());
    }
}
