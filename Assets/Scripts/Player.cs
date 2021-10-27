using UnityEngine;
using System;

public class Player : MonoBehaviour {

    public event Action OnPlayerDie;
	private int health = 100;
    public int Health => health;
	public GameObject bulletPrefab;
	

    //Nhận damage
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
