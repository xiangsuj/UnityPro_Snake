using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class SnakeHead : MonoBehaviour {


	public List<Transform> bodyList = new List<Transform> ();

	public float velocity=0.35f;
	public int step;
	private int x;
	private int y;
	private Vector3 headPos;
	private Transform canvas;
	public GameObject bodyPrefab;
	public AudioClip eatClip;
	public AudioClip dieClip;
	public Sprite[] bodySprites = new Sprite[2];
	private bool isDie=false;
	public GameObject dieEffect;
	public int ratio = 20;
	void Awake(){
		canvas = GameObject.Find ("Canvas").transform;
		gameObject.GetComponent<Image> ().sprite = Resources.Load<Sprite> (PlayerPrefs.GetString ("sh", "sh02"));
		bodySprites[0] = Resources.Load<Sprite> (PlayerPrefs.GetString ("sb01", "sh0201"));
		bodySprites[1] = Resources.Load<Sprite> (PlayerPrefs.GetString ("sb02", "sh0202"));
		if (PlayerPrefs.GetString ("gift").Equals ("1")) {
			ratio = 70;
		}

	}
	void Start(){
		InvokeRepeating ("Move", 0, velocity);
		x = step;
		y = 0;

	}
	void Update(){
		
		if (Input.GetKeyDown (KeyCode.Space)&&MainUIController.Instance.isPause==false&&isDie==false) {
			
			CancelInvoke ();
			InvokeRepeating ("Move", 0, velocity-0.2f);
		}
		if (Input.GetKeyUp (KeyCode.Space)&&MainUIController.Instance.isPause==false&&isDie==false) {
			CancelInvoke ();
			InvokeRepeating ("Move", 0, velocity);
		}
		if (MainUIController.Instance.direction==0&&y!=-step&&MainUIController.Instance.isPause==false&&isDie==false) {
			gameObject.transform.localRotation = Quaternion.Euler (0, 0, 0);
			x = 0;
			y = step;
		}
		if (MainUIController.Instance.direction==2&&y!=step&&MainUIController.Instance.isPause==false&&isDie==false) {
			gameObject.transform.localRotation = Quaternion.Euler (0, 0, 180);

			x = 0;
			y = -step;
		}
		if (MainUIController.Instance.direction==3&&x!=step&&MainUIController.Instance.isPause==false&&isDie==false) {
			gameObject.transform.localRotation = Quaternion.Euler (0, 0, 90);
			x = -step;
			y = 0;
		}
		if (MainUIController.Instance.direction==1&&x!=-step&&MainUIController.Instance.isPause==false&&isDie==false) {
			gameObject.transform.localRotation = Quaternion.Euler (0, 0, -90);
			x = step;
			y=0;
		}
	}


    void Move(){
		headPos = gameObject.transform.localPosition;

		gameObject.transform.localPosition = new Vector3 (headPos.x+x,headPos.y+y,headPos.z);
		/*
		if (bodyList.Count > 0) {
			bodyList.Last ().localPosition = headPos;
			bodyList.Insert (0, bodyList.Last ());
			bodyList.RemoveAt (bodyList.Count - 1);
		}
		*/
		if (bodyList.Count > 0) {
			for (int i = bodyList.Count - 2; i >= 0; i--) {                                           //从后往前开始移动蛇身
				bodyList [i + 1].localPosition = bodyList [i].localPosition;                          //每一个蛇身都移动到它前面一个节点的位置
			}
			bodyList [0].localPosition = headPos; 
		}
	}

	void Grow(){
		AudioSource.PlayClipAtPoint (eatClip, Vector3.zero);
		int index = (bodyList.Count % 2 == 0) ? 0 : 1;
		GameObject body = Instantiate (bodyPrefab,new Vector3(2000,2000,0),Quaternion.identity);
		body.GetComponent<Image> ().sprite = bodySprites [index];
		body.transform.SetParent (canvas, false);
		bodyList.Add (body.transform);


	}


	void Die(){
		AudioSource.PlayClipAtPoint (dieClip, Vector3.zero);
		CancelInvoke ();
		isDie = true;
		Instantiate (dieEffect);
		PlayerPrefs.SetInt ("lastl", MainUIController.Instance.length);
		PlayerPrefs.SetInt ("lasts", MainUIController.Instance.score);
		if (PlayerPrefs.GetInt ("bests", 0) < MainUIController.Instance.score) {
			PlayerPrefs.SetInt ("bestl", MainUIController.Instance.length);
			PlayerPrefs.SetInt ("bests", MainUIController.Instance.score);
		}
		StartCoroutine (GameOver(1.5f));
	}

	IEnumerator GameOver(float t){
		yield return new WaitForSeconds (t);
		UnityEngine.SceneManagement.SceneManager.LoadScene (1);
	}
	private void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.CompareTag ("Food")) {
			Destroy (collision.gameObject);
			MainUIController.Instance.UpdateUI ();
			Grow ();
			FoodMaker.Instance.MakeFood (Random.Range (0, 100) < ratio?true:false);


		} else if (collision.gameObject.CompareTag ("Body")) {
			Die ();
		}else if(collision.gameObject.CompareTag ("Reward")){
			Destroy(collision.gameObject);
			MainUIController.Instance.UpdateUI (Random.Range(5,15)*10);
			Grow();
		} else {
			if (MainUIController.Instance.hasBorder) {
				Die ();
			} else {
				switch(collision.gameObject.name){
				case"Up":
					transform.localPosition = new Vector3 (transform.localPosition.x, -transform.localPosition.y+30, transform.localPosition.z);
					break;
				case"Down":
					transform.localPosition = new Vector3 (transform.localPosition.x, -transform.localPosition.y-30, transform.localPosition.z);
					break;
				case"Left":
					transform.localPosition = new Vector3 (-transform.localPosition.x+180, transform.localPosition.y, transform.localPosition.z);
					break;
				case"Right":
					transform.localPosition = new Vector3 (-transform.localPosition.x+240, transform.localPosition.y, transform.localPosition.z);
					break;
				default:
					break;
				}
			}

		}
	}
}
