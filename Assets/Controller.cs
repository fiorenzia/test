using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour 
{

	public bool isMine;
	public float dx,dy;


	// Update is called once per frame
	void Update () 
	{
		if (!NetworkViewManager.connected) 
		{
			return;
		}

		if (isMine) 
		{
			dx = Input.GetAxis("Horizontal");
			dy = Input.GetAxis ("Vertical");
			transform.Rotate (Vector3.forward,-dx*2);
			transform.Translate(0.0f,dy*0.1f,0.0f);
			GetComponent<NetworkView>().RPC ("MovePlayer",RPCMode.Others,transform.position);
		}
//		if(Input.GetKeyDown("SPACE")
//		{
//			Network.Instantiate()
		}

	void OnCollisuonEnter(Collision col)
	{
		if (col.gameObject.CompareTag ("Bullet")) {
			Destroy (gameObject);
		}
	}

	[RPC]
	public void MovePlayer(Vector3 position)
	{
		transform.position = position;
		transform.Rotate (Vector3.forward,-dx*2);
	}

}
