using System;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public sealed class Huh : MonoBehaviour
{
    public float holep;
    public int w, h, x, y;
    private bool[,] _hwalls;
    private bool[,] _vwalls;
    public Transform level, player, goal;
    public GameObject floor, wall;
    public CinemachineVirtualCamera cam;
    public Transform timerPos;

    public bool moveP = true;

    public AstarPath AstarPath;
    public IAstarAI AI;
    public Path Path;

    void Start()
    {
        this.AI = this.player.GetComponent<IAstarAI>();

        foreach (Transform child in this.level)
            Object.Destroy(child.gameObject);

        this._hwalls = new bool[this.w + 1, this.h];
        this._vwalls = new bool[this.w, this.h + 1];
        var st = new int[this.w][];
        for (int index = 0; index < this.w; index++)
        {
            st[index] = new int[this.h];
        }

        void dfs(int x, int y)
        {
            st[x][y] = 1;
            Object.Instantiate(this.floor, new Vector3(x, y), Quaternion.identity, this.level);

            (int, int, bool[,], int, int, Vector3, int, KeyCode)[] dirs =
            {
                (x - 1, y, this._hwalls, x, y, Vector3.right, 90, KeyCode.A),
                (x + 1, y, this._hwalls, x + 1, y, Vector3.right, 90, KeyCode.D),
                (x, y - 1, this._vwalls, x, y, Vector3.up, 0, KeyCode.S),
                (x, y + 1, this._vwalls, x, y + 1, Vector3.up, 0, KeyCode.W),
            };
            foreach ((int nx, int ny, bool[,] wall, int wx, int wy, Vector3 sh, int ang, KeyCode k) in dirs.OrderBy(d => Random.value))
            {
                if (!(0 <= nx && nx < this.w && 0 <= ny && ny < this.h) || (st[nx][ny] == 2 && Random.value > this.holep))
                {
                    wall[wx, wy] = true;
                    Object.Instantiate(this.wall, (new Vector3(wx, wy) - sh / 2), Quaternion.Euler(0, 0, ang), this.level);
                }
                else if (st[nx][ny] == 0) dfs(nx, ny);
            }

            st[x][y] = 2;
        }

        dfs(0, 0);
        this.timerPos.position = new Vector3(Screen.width - 150, Screen.height - 20);
        this.AstarPath.Scan();

        this.x = Random.Range(0, this.w);
        this.y = Random.Range(0, this.h);
        this.player.position = new Vector3(this.x, this.y);
        do
            this.goal.position = new Vector3(Random.Range(0, this.w), Random.Range(0, this.h));
        while (Vector3.Distance(this.player.position, this.goal.position) < (this.w + this.h) / 4);
        this.cam.m_Lens.OrthographicSize = (Mathf.Pow(this.w / 3 + this.h / 2, 0.7f) + 1) / 1.5f;

        SceneManager.activeSceneChanged += ChangedActiveScene;

        // wait 1.5 seconds before change to Scene2
        StartCoroutine(TimeChangedScene());
    }

    public static void ReachedDestination(bool success)
    {
        Debug.Log(success ? "You reached the maze without help" : "Suffer the consequences of failing a simple maze");
        SceneManager.LoadScene(1);
    }
    public delegate void Change();
    public static event Change TimeChanged;
    public int NumLevel;




    public void ReturnButton()
    {
        SceneManager.LoadScene(1);
    }




    IEnumerator TimeChangedScene()
    {
        print(Time.time + " seconds");
        yield return new WaitForSeconds(40f);
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
        SceneManager.LoadScene(index);
    }

    void OnDisable()
    {
        Debug.Log("OnDisable happened for Scene1");
    }

    void Update()
    {
        if (this.AI.reachedDestination)
        {
            this.AI.canMove = false;
            this.moveP = true;

            x = (int)this.player.transform.position.x;
            y = (int)this.player.transform.position.y;

            Huh.ReachedDestination(false);
        }

        if (this.moveP)
        {
            (int, int, bool[,], int, int, Vector3, int, KeyCode, int)[] dirs =
            {
                (this.x - 1, this.y, this._hwalls, this.x, this.y, Vector3.right, 90, KeyCode.A, 0),
                (this.x + 1, this.y, this._hwalls, this.x + 1, this.y, Vector3.right, 90, KeyCode.D, 1),
                (this.x, this.y - 1, this._vwalls, this.x, this.y, Vector3.up, 0, KeyCode.S, 2),
                (this.x, this.y + 1, this._vwalls, this.x, this.y + 1, Vector3.up, 0, KeyCode.W, 3)
            };

            foreach ((int nx, int ny, bool[,] wall, int wx, int wy, Vector3 sh, int ang, KeyCode k, int idx) in
                     dirs.OrderBy(static d => Random.value))
            {
                if (!Input.GetKeyDown(k)) continue;

                if (wall[wx, wy])
                    this.player.position = Vector3.Lerp(this.player.position, new Vector3(nx, ny), 0.1f);
                else
                {
                    Vector3 newPlayerRot = idx switch
                    {
                        0 => new Vector3(0, 0, 90),
                        1 => new Vector3(0, 0, -90),
                        2 => new Vector3(0, 0, 180),
                        3 => new Vector3(0, 0, 0),
                        _ => default
                    };

                    this.player.transform.eulerAngles = newPlayerRot;

                    (this.x, this.y) = (nx, ny);
                }
            }

            this.player.position = Vector3.Lerp(this.player.position, new Vector3(this.x, this.y), Time.deltaTime * 12);

            if (Vector3.Distance(this.player.position, this.goal.position) < 0.12f)
            {
                Huh.ReachedDestination(true);
            }
        }
        else
        {
            Vector3 position = this.player.position;
            float positionX = position.x;
            float positionY = position.y;

            position = new Vector3(positionX, positionY, 0);
            this.player.position = position;
        }
    }
}