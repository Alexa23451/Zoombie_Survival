using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class UIController : MonoBehaviour
{
    public Text recordTxt;
    public InputField inputName;
    public Button recordBtn;
    public Transform tableRecord;
    public GameObject prefabElementRecord;
    public JsonManager jsonManager;
    private GameData currentData;
    public const string dataPath = "ZoombieGame";

    private void OnEnable()
    {
        jsonManager = new JsonManager();
        recordBtn.onClick.AddListener(SaveRecord);
        DisplaySaveData();
    }

    [ContextMenu("TEST")]
    public void Test() => Cursor.lockState = CursorLockMode.Confined;

    private void DisplaySaveData()
    {
        currentData = jsonManager.ReadDataFromFile<GameData>(dataPath);

        foreach (var val in currentData.playDatas)
        {
            GameObject newRecord = Instantiate(prefabElementRecord, tableRecord);
            newRecord.GetComponent<RecordElement>().SetInfo(val.Time,
                val.Name,
                val.Score);
        }
    }

    private void SaveRecord()
    {
        GameObject newRecord = Instantiate(prefabElementRecord , tableRecord);
        newRecord.GetComponent<RecordElement>().SetInfo(GameController.Instance.gameTimer.ToString(),
            inputName.text,
            GameController.Instance.player.Point.ToString());
        recordBtn.enabled = false;


        currentData.AddNewData(new PlayData
        {
            Name = inputName.text,
            Score = GameController.Instance.player.Point.ToString(),
            Time = GameController.Instance.gameTimer.ToString(),
        });

        jsonManager.WriteDataToFile(currentData, dataPath);
    }

    public void SetRecord(string txt)
    {
        recordTxt.text = txt;
    }


    public void QuitGame() => Application.Quit();

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
