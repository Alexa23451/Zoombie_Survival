using UnityEngine;
using System;
using System.Collections;

public class Enemy : MonoBehaviour {

	public event Action OnEnemyDie;
	public ParticleSystem OnDieEff;
	public GameObject vfxText;


	public Vector3 direction;
	public PlayerController _player;

	public int damage = 3;
	public float speed = 3.5f;
	public float distanceToStop = 1f;
	public bool chasingPlayer;

	public float eatingInterval = 0.5f;
	private float eatingTimer = 0f;

	//Hàm khởi tạo kẻ địch, gọi từ GameManager
	public void Init(PlayerController player, Action OnEnemyDead)
    {
		chasingPlayer = true;
		_player = player;

		float randomAngle = UnityEngine.Random.Range(0f, 2f * Mathf.PI);
		transform.position = new Vector3(
			player.transform.position.x + Mathf.Cos(randomAngle) * 20f,
			transform.position.y,
			player.transform.position.z + Mathf.Sin(randomAngle) * 20f
		);

		direction = (player.transform.position - transform.position).normalized;
		transform.LookAt(player.transform);

		OnEnemyDie = OnEnemyDead;
	}

	//Xử lý khi enemy chết
	public void Dead()
    {
		OnEnemyDie?.Invoke();
		SoundManager.Instance.PlayAudio(SoundManager.Instance.hitSound);
		ParticleSystem particleSystem = GameObject.Instantiate(OnDieEff , transform.position , Quaternion.identity);
		particleSystem.Play();
		Destroy(particleSystem, 2f);

		float distance = Vector3.Distance(transform.position, _player.transform.position);
		GameObject infoTxt = GameObject.Instantiate(vfxText ,transform.position, transform.rotation * Quaternion.Euler(0,180,0));
		infoTxt.transform.localScale = Vector3.one * 0.01f * distance;
		Destroy(infoTxt, 1.5f);

		Destroy(gameObject);
	}

	//Update di chuyển đến gần người chơi và gây tấn công giảm máu của ngươi chơi

	void Update () {
		if (Vector3.Distance (transform.position, _player.transform.position) < distanceToStop) {
			chasingPlayer = false;
		}

		if (chasingPlayer) {
			transform.position += direction * speed * Time.deltaTime;
		} else {
			eatingTimer -= Time.deltaTime;
			if (eatingTimer <= 0f) {
				eatingTimer = eatingInterval;

				_player.TakeDamage(damage);
			}
		}
	}
}
