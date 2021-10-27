using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed = 10f;
	public Vector3 direction;
	public float lifetime = 4f;

	//update liên tục đường đi của viên đạn
	void Update () {
		transform.position += direction * speed * Time.deltaTime;

		lifetime -= Time.deltaTime;
		if (lifetime <= 0f) {
			Destroy (this.gameObject);
		}
	}

	//xử lý va chạm
	void OnTriggerEnter (Collider collider) {

		//va chạm với kẻ địch
		if (collider.TryGetComponent(out Enemy enemy)) {
			Destroy (this.gameObject);
			enemy.Dead();
		}

		//va chạm với thùng bom
		if (collider.TryGetComponent(out ExplosiveBarrelScript barrelScript))
		{
			barrelScript.explode = true;
			Destroy(this.gameObject);
		}
	}
}
