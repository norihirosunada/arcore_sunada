using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class ShootScript : MonoBehaviour {
    public GameObject andy;
    public GameObject andys;//ドロイド君
    public GameObject crackImage;
    public GameObject applePrefab;

    private int mode = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Touch touch;
        if (Input.touchCount < 1 ||
           (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            //画面に触れていないorすでに触れているなら何もしない
            return;
        }

        //タップした座標にドロイド君を移動
        TrackableHit hit;
        TrackableHitFlags filter = TrackableHitFlags.PlaneWithinPolygon;
        if (Frame.Raycast(touch.position.x, touch.position.y, filter, out hit))
        {
            //平面にヒット＆裏面でなければドロイド君を置く
            if ((hit.Trackable is DetectedPlane) &&
               Vector3.Dot(Camera.main.transform.position - hit.Pose.position, hit.Pose.rotation * Vector3.up) > 0)
            {
                //Anchorを設定
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                //if (mode == 1 )
                //{
                //    //ボールの位置・姿勢指定
                //    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //    sphere.transform.position = Camera.main.transform.TransformPoint(0, 0, 0.5f);
                //    sphere.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                //    sphere.AddComponent<Rigidbody>();
                //    sphere.GetComponent<Rigidbody>().AddForce(Camera.main.transform.TransformDirection(0, 1f, 2f), ForceMode.Impulse);

                //    //Anchorを設定
                //    sphere.transform.parent = anchor.transform;
                //    mode++;
                //    mode = mode % 2;
                //}
                //else if (mode == 0)
                //{

                //    //ドロイド君の位置・姿勢指定
                //    var andysObject = Instantiate(andys);
                //    andysObject.transform.position = hit.Pose.position;
                //    andysObject.transform.rotation = hit.Pose.rotation;
                //    andysObject.transform.Rotate(0, Camera.main.transform.rotation.y, 0, Space.Self);
                //    //Anchorを設定
                //    andysObject.transform.parent = anchor.transform;
                //    mode++;
                //    mode = mode % 2;
                //}

                //apple投げる

                GameObject apple = Instantiate(applePrefab) as GameObject;
                apple.transform.position = Camera.main.transform.TransformPoint(0, 0, 0.5f);
                apple.GetComponent<Rigidbody>().AddForce(Camera.main.transform.TransformDirection(0, 1f, 2f), ForceMode.Impulse);
                apple.transform.parent = anchor.transform;

                //ひびテクスチャ貼り付け
                //var crackObject = Instantiate(crackImage);
                //crackObject.transform.position = hit.Pose.position;
                //crackObject.transform.rotation = hit.Pose.rotation;
                //crackObject.transform.Rotate(-90, Camera.main.transform.rotation.y - 180, 0, Space.Self);
                //Anchorを設定
                //crackObject.transform.parent = anchor.transform;
            }
        }
    }
}
