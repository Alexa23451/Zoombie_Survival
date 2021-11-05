using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController Instance;

	public PlayerController player;
	public GameObject enemyPrefab;
	public GameObject targetPrefab;
	public GameObject barrelPrefab;
	public TextMesh infoText;

	public float enemySpawnDistance = 20f;

	public float enemyInterval = 2.0f;
	public float minimumEnemyInterval = 0.5f;
	public float enemyIntervalDecrement = 0.1f;

	public float gameTimer = 0f;
	private float enemyTimer = 0f;

	public GameObject gameoverUI;

	private void Start()
	{
		Instance = this;

		Time.timeScale = 1;
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

			target2.GetComponent<TargetHeal>().Init(player, () => {
				player.enemyKill++;
				player.TakeDamage(-2);
				player.GetScore(50);
			} );
		}

	}

	void Update() {
		SpawnEnemy();
		GameOver();
	}

	//Kiểm tra gameover
	void GameOver()
	{
		if (player.Health > 0)
		{
			gameTimer += Time.deltaTime;
			infoText.text = "Health: " + player.Health;
			infoText.text += "\nTime: " + Mathf.Floor(gameTimer);
			infoText.text += "\nScore: " + player.Point;
		}
		else
		{
			infoText.text = "Game over!";
			infoText.text += "\nYou survived for " + Mathf.Floor(gameTimer) + " seconds!";
			infoText.text += "\nYour Point: " + player.Point;

			Time.timeScale = 0;
			gameoverUI.SetActive(true);
			gameoverUI.GetComponent<UIController>().SetRecord(infoText.text);
			Cursor.lockState = CursorLockMode.Confined;
			player.GetComponent<PlayerMovement>().enabled = false;

		}


	}

	//gọi zoombie
	void SpawnEnemy()
    {
		enemyTimer -= Time.deltaTime;
		if (enemyTimer <= 0)
		{
			enemyTimer = enemyInterval;
			enemyInterval -= enemyIntervalDecrement;

			if (enemyInterval < minimumEnemyInterval)
			{
				enemyInterval = minimumEnemyInterval;
			}

			GameObject enemyObject = Instantiate(enemyPrefab);
			enemyObject.GetComponent<Enemy>().Init(player, () => 
			{
				player.GetScore(100);
				player.TakeDamage(-2); 
			});
		}
	}
}
