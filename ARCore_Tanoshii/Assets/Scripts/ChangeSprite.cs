using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour {
    SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        //このオブジェクトのSpriteRendererを取得
		sr = gameObject.GetComponent<SpriteRenderer>();
        // "crack_0〜13" の文字列をランダムで生成
        Sprite crack = Resources.Load<Sprite>("Sprites/crack_" + Random.Range(0, 13));
        sr.sprite = crack;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
