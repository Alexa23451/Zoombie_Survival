using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public event Action OnPlayerDie;
	private int health = 100;
    private int point = 0;
    public int Health => health;
    public int Point => point;
	public GameObject bulletPrefab;

    public List<GameObject> weaponList;
    private int currentWeaponId;
    public int enemyKill = 0;

    private void Start()
    {
        currentWeaponId = 0;
        SetWeapon(currentWeaponId);
    }

    private void Update()
    {
        //Change weapon using 1-3 

        if (Input.GetKeyDown(KeyCode.Alpha1) && currentWeaponId != 0)
        {
            currentWeaponId = 0;
            SetWeapon(currentWeaponId);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && currentWeaponId != 1)
        {
            currentWeaponId = 1;
            SetWeapon(currentWeaponId);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && currentWeaponId != 2)
        {
            currentWeaponId = 2;
            SetWeapon(currentWeaponId);
        }
    }

    private void SetWeapon(int id)
    {
        if (id >= weaponList.Count)
            return;

        for(int i=0; i<weaponList.Count; i++)
        {
            weaponList[i].SetActive(i == id);
        }
    }

    public void GetScore(int val) => point += val;

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
