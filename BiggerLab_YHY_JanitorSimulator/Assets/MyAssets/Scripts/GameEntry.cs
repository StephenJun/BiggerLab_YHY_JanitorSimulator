using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntry : MonoBehaviour {

	void Start () {

        UIManager.Instance.Init();
        UIManager.Instance.Push(typeof(MenuScreen), null);
	}

	void Update () {
		
	}
}
