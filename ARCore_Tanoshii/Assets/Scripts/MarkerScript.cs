using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class MarkerScript : MonoBehaviour {
    public GameObject andy;//Andy

    //マーカー番号とモデルの紐付けを記録する辞書
    Dictionary<int, GameObject> markerDic = new Dictionary<int, GameObject>();
    //フレーム毎に認識されたマーカーを一時的に覚えておく
    List<AugmentedImage> markers = new List<AugmentedImage>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Session.GetTrackables<AugmentedImage>(markers, TrackableQueryFilter.Updated);

        foreach(var image in markers)
        {
            int index = image.DatabaseIndex;
            GameObject obj = null;
            markerDic.TryGetValue(index, out obj);
            if(image.TrackingState == TrackingState.Tracking && obj == null)
            {
                Anchor anchor = image.CreateAnchor(image.CenterPose);
                switch(index)
                {
                    case 0://Andy
                        obj = GameObject.Instantiate(andy, anchor.transform);
                        obj.transform.localPosition = new Vector3(0, 0.02f, -0.04f);
                        obj.transform.Rotate(-90, 0, 0, Space.Self);
                        obj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                        markerDic.Add(index, obj);
                        break;
                    case 1:
                        break;
                }
            }
            else if(image.TrackingState == TrackingState.Stopped && obj != null)
            {
                markerDic.Remove(image.DatabaseIndex);
                Destroy(obj);
            }
        }
	}
}
