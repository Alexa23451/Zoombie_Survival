using UnityEngine;
using System;

public class Player : MonoBehaviour {

    public event Action OnPlayerDie;
	private int health = 100;
    public int Health => health;
	public GameObject bulletPrefab;
	
	void Update () {

		if (Input.GetMouseButtonDown(0)) {
            SoundManager.Instance.PlayAudio(SoundManager.Instance.shotSound);

            GameObject bulletObject = Instantiate(bulletPrefab);
            bulletObject.transform.position = this.transform.position;

            Bullet bullet = bulletObject.GetComponent<Bullet>();
            bullet.direction = transform.forward;

        }
	}

    public void TakeDamage(int healthAmount)
    {
        health -= healthAmount;

        health = Mathf.Clamp(health, 0, 100);

        if(health == 0)
        {
            OnPlayerDie?.Invoke();
        }
    }
}
