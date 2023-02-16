using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class sceneChanger : MonoBehaviour
{
    public delegate void Change();
    public static event Change TimeChanged;

    public void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;

        // wait 1.5 seconds before change to Scene2
        StartCoroutine(TimeChangedScene());
    }

    IEnumerator TimeChangedScene()
    {
        print(Time.time + " seconds");
        yield return new WaitForSeconds(1.5f);
        print(Time.time + " seconds");
 
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
        SceneManager.LoadScene(index);
    }

    void OnDisable()
    {
        Debug.Log("OnDisable happened for Scene1");
    }
}
