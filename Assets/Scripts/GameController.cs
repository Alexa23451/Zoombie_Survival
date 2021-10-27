using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public Player player;
	public GameObject enemyPrefab;
	public GameObject targetPrefab;
	public GameObject barrelPrefab;
	public TextMesh infoText;

	public float enemySpawnDistance = 20f;

	public float enemyInterval = 2.0f;
	public float minimumEnemyInterval = 0.5f;
	public float enemyIntervalDecrement = 0.1f;

	private float gameTimer = 0f;
	private float enemyTimer = 0f;
	private float resetTimer = 3f;

    private void Start()
    {
		Cursor.lockState = CursorLockMode.Locked;
		StartCoroutine(SpawnBarrel());
	}

	//Sản sinh ra thùng bom và mục tiêu (Target) trong quá trình game
	IEnumerator SpawnBarrel()
    {
        while (true)
        {
			yield return new WaitForSeconds(4f);
			GameObject barrelBom = Instantiate(barrelPrefab);

			float randomAngle = Random.Range(0f, 2f * Mathf.PI);
			barrelBom.transform.position = new Vector3(
				player.transform.position.x + Mathf.Cos(randomAngle) * UnityEngine.Random.Range(3f, 15f),
				barrelBom.transform.position.y,
				player.transform.position.z + Mathf.Sin(randomAngle) * UnityEngine.Random.Range(3f, 15f)
			);


			GameObject target2 = Instantiate(targetPrefab);

			float randomAngle1 = Random.Range(0f, 2f * Mathf.PI);
			target2.transform.position = new Vector3(
				player.transform.position.x + Mathf.Cos(randomAngle1) * UnityEngine.Random.Range(5f, 20f),
				target2.transform.position.y,
				player.transform.position.z + Mathf.Sin(randomAngle1) * UnityEngine.Random.Range(5f, 20f)
			);
			target2.transform.LookAt(player.transform); 

			target2.GetComponent<TargetHeal>().Init(player, () => player.TakeDamage(-2));
		}

	}

	//Liên tục kiểm tra trạng thái của player và cập nhật GUI về tình trạng của game cho người chơi
    void Update () {
		if (player.Health > 0) {
			gameTimer += Time.deltaTime;
			infoText.text = "Health: " + player.Health;
			infoText.text += "\nTime: " + Mathf.Floor (gameTimer);
		} else {
			infoText.text = "Game over!";
			infoText.text += "\nYou survived for " + Mathf.Floor (gameTimer) + " seconds!";

			resetTimer -= Time.deltaTime;
			if (resetTimer <= 0f) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}

		enemyTimer -= Time.deltaTime;
		if (enemyTimer <= 0) {
			enemyTimer = enemyInterval;
			enemyInterval -= enemyIntervalDecrement;

			if (enemyInterval < minimumEnemyInterval) {
				enemyInterval = minimumEnemyInterval;
			}

			GameObject enemyObject = Instantiate(enemyPrefab);
			enemyObject.GetComponent<Enemy>().Init(player, () => player.TakeDamage(-2));
		}
	}


}
