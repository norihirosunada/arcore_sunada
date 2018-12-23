using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleGenerator : MonoBehaviour {

    [SerializeField] GameObject applePrefab;
	// Update is called once per frame
	void Update () {
        Touch touch;
        if (Input.touchCount < 1 ||
           (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            //画面に触れていないorすでに触れているなら何もしない
            return;
        }
        if (Input.GetMouseButtonDown(0)){
            GameObject apple = Instantiate(applePrefab) as GameObject;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 worldDir = ray.direction;
            apple.GetComponent<BombController>().Shoot(worldDir.normalized * 2000);
        }
	}
}
