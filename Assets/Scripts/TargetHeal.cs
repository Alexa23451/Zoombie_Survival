using System;
using UnityEngine;

public class TargetHeal : MonoBehaviour
{
    public event Action OnTargetHit;
    public GameObject vfxText;
    public PlayerController _player;

    //Khởi tạo thông tin cho Target
    public void Init(PlayerController player, Action action)
    {
        OnTargetHit = action;
        _player = player;
    }

    //Xử lý va chạm
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnTargetHit?.Invoke();

            float distance = Vector3.Distance(transform.position, _player.transform.position);
            GameObject infoTxt = GameObject.Instantiate(vfxText, transform.position, transform.rotation * Quaternion.Euler(0, 180, 0));
            infoTxt.transform.localScale = Vector3.one * 0.01f * distance;
            Destroy(infoTxt, 1.5f);

            Destroy(gameObject);
            Destroy(other.gameObject);
        }

    }
}
