using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Surprise : MonoBehaviour {

	public GameObject[]bodyArray;
	public int i = 0;
	void Start(){
		InvokeRepeating ("instanceBody", 0, 0.2f);

	}
	void instanceBody(){
		if (i < bodyArray.Length) {
			
			bodyArray [i].gameObject.SetActive (true);
			i++;
		} else {
			StartCoroutine (GameOver(3f));
		}
	}

	IEnumerator GameOver(float t){
		PlayerPrefs.SetString ("gift", "0");
		Debug.Log ("over" + PlayerPrefs.GetString ("gift"));
		yield return new WaitForSeconds (t);
		UnityEngine.SceneManagement.SceneManager.LoadScene (0);
	}
}
