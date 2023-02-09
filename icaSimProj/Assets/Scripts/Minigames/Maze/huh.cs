using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class huh : MonoBehaviour
{
    public float holep;
    public int w, h, x, y;
    public bool[,] hwalls, vwalls;
    public Transform Level, Player, Goal;
    public GameObject Floor, Wall;
    public CinemachineVirtualCamera cam;

    void Start()
    {
        foreach (Transform child in Level)
            Destroy(child.gameObject);

        hwalls = new bool[w + 1, h];
        vwalls = new bool[w, h + 1];
        var st = new int[w, h];

        void dfs(int x, int y)
        {
            st[x, y] = 1;
            Instantiate(Floor, new Vector3(x, y), Quaternion.identity, Level);

            var dirs = new[]
            {
                (x - 1, y, hwalls, x, y, Vector3.right, 90, KeyCode.A),
                (x + 1, y, hwalls, x + 1, y, Vector3.right, 90, KeyCode.D),
                (x, y - 1, vwalls, x, y, Vector3.up, 0, KeyCode.S),
                (x, y + 1, vwalls, x, y + 1, Vector3.up, 0, KeyCode.W),
            };
            foreach (var (nx, ny, wall, wx, wy, sh, ang, k) in dirs.OrderBy(d => Random.value))
                if (!(0 <= nx && nx < w && 0 <= ny && ny < h) || (st[nx, ny] == 2 && Random.value > holep))
                {
                    wall[wx, wy] = true;
                    Instantiate(Wall, (new Vector3(wx, wy) - sh / 2), Quaternion.Euler(0, 0, ang), Level);
                }
                else if (st[nx, ny] == 0) dfs(nx, ny);
            st[x, y] = 2;
        }
        dfs(0, 0);

        x = Random.Range(0, w);
        y = Random.Range(0, h);
        Player.position = new Vector3(x, y);
        do Goal.position = new Vector3(Random.Range(0, w), Random.Range(0, h));
        while (Vector3.Distance(Player.position, Goal.position) < (w + h) / 4);
        cam.m_Lens.OrthographicSize = (Mathf.Pow(w / 3 + h / 2, 0.7f) + 1);
    }

    void Update()
    {
        (int, int, bool[,], int, int, Vector3, int, KeyCode, int)[] dirs = new[]
        {
            (x - 1, y, hwalls, x, y, Vector3.right, 90, KeyCode.A, 0),
            (x + 1, y, hwalls, x + 1, y, Vector3.right, 90, KeyCode.D, 1),
            (x, y - 1, vwalls, x, y, Vector3.up, 0, KeyCode.S, 2),
            (x, y + 1, vwalls, x, y + 1, Vector3.up, 0, KeyCode.W, 3),
        };
        foreach ((int nx, int ny, bool[,] wall, int wx, int wy, Vector3 sh, int ang, KeyCode k, int idx) in dirs.OrderBy(d => Random.value))
            if (Input.GetKeyDown(k))
                if (wall[wx, wy])
                    Player.position = Vector3.Lerp(Player.position, new Vector3(nx, ny), 0.1f);
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

                    this.Player.transform.eulerAngles = newPlayerRot;

                    (x, y) = (nx, ny);
                }

        Player.position = Vector3.Lerp(Player.position, new Vector3(x, y), Time.deltaTime * 12);
        if (Vector3.Distance(Player.position, Goal.position) < 0.12f)
        {
            if (Random.Range(0, 5) < 3) w++;
            else h++;
            Start();
        }
    }
}