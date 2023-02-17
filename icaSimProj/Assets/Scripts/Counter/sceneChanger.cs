using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class sceneChanger : MonoBehaviour
{
    public delegate void Change();
    public static event Change TimeChanged;
    public int NumLevel;


  

    public void ReturnButton()
    {
        SceneManager.LoadScene(0);
    }
    
  

    public void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;

        // wait 1.5 seconds before change to Scene2
        StartCoroutine(TimeChangedScene());
    }

    IEnumerator TimeChangedScene()
    {
        print(Time.time + " seconds");
        yield return new WaitForSeconds(2f);
        print(Time.time + " seconds");
        SceneManager.LoadScene(Random.Range(1, NumLevel));
 
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        string currentName = current.name;

        if (currentName == null)
        {
            // Scene1 has been removed
            currentName = "Replaced";
        }

        Debug.Log("Scenes: " + currentName + ", " + next.name);
    }

    void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    void ChangeScene(int index)
    {
        Debug.Log("Changing to Scene2");
        SceneManager.LoadScene("Scene2");
        Scene scene = SceneManager.GetSceneByName("Scene2");
        SceneManager.SetActiveScene(scene);
        if (index == 0)
        {
            return;
        }
        SceneManager.LoadScene(index);
    }

    void OnDisable()
    {
        Debug.Log("OnDisable happened for Scene1");
    }
}
