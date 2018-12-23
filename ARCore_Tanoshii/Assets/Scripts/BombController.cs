using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {

    [SerializeField] GameObject BombParticle;
    [SerializeField] GameObject fireParticle;

    [SerializeField] float DestroyInterval;
    [SerializeField] float FireGeneratetime;

    public GameObject crackImage;

    private GameObject particle;

    public void Shoot(Vector3 dir){
        GetComponent<Rigidbody>().AddForce(dir, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = true;

        particle = Instantiate(BombParticle) as GameObject;
        Vector3 pos = transform.position;
        particle.transform.position = pos;
        StartCoroutine(FireGenerate());
        StartCoroutine(ExecuteBomb());

        //衝突位置にヒビ描画
        var crackObject = Instantiate(crackImage);
        crackObject.transform.position = collision.transform.position;
        crackObject.transform.rotation = collision.transform.rotation;
        crackObject.transform.Rotate(-90, Camera.main.transform.rotation.y - 180, 0, Space.Self);

        //Anchorを設定
        crackObject.transform.parent = transform.parent;
    }

    IEnumerator ExecuteBomb()
    {
        yield return new WaitForSeconds(DestroyInterval);
        Destroy(gameObject);

    }

    IEnumerator FireGenerate(){
        yield return new WaitForSeconds(FireGeneratetime);
        GameObject fire = Instantiate(fireParticle) as GameObject;
        Vector3 pos = transform.position;
        fire.transform.position = pos;
        Destroy(particle);
    }

    // Use this for initialization
    void Start () {
        //Shoot(new Vector3(0, 200, 2000));
	}
	

	// Update is called once per frame
	void Update () {
		
	}
}
