using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed = 10f;
	public Vector3 direction;
	public float lifetime = 4f;

	void Update () {
		transform.position += direction * speed * Time.deltaTime;

		lifetime -= Time.deltaTime;
		if (lifetime <= 0f) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.TryGetComponent(out Enemy enemy)) {
			Destroy (this.gameObject);
			enemy.Dead();
		}

		if (collider.TryGetComponent(out ExplosiveBarrelScript barrelScript))
		{
			barrelScript.explode = true;
			Destroy(this.gameObject);
		}
	}
}
