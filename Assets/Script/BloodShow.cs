using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodShow : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
       StartCoroutine(ExpandBlood());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator ExpandBlood()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        Vector3 tempscale = transform.localScale;
        for(int i = 0; i < 20; i++)
        {
            transform.localScale = Vector3.Lerp(tempscale, new Vector3(1, 1, 1),(float)i/19);
            yield return new WaitForSeconds(0.05f);
        }
        for(int i = 0; i < 100; i++)
        {
            transform.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.clear, (float)i / 99);
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }
}
