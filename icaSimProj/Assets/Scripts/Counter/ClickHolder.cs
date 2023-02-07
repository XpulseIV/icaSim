using System;
using UnityEngine;

namespace Minigames.Maze
{
    public class ClickHolder : MonoBehaviour
    {
        public GameObject Hand;
        public Sprite HandIdle;
        public Sprite HandClick;
        public Sprite HandDrag;

        private void Update()
        {
            Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                this.Hand.GetComponent<SpriteRenderer>().sprite = this.HandDrag;
            }

            if (Input.GetMouseButtonUp(0))
            {
                this.Hand.GetComponent<SpriteRenderer>().sprite = this.HandIdle;
            }

            this.Hand.transform.position = new Vector3(mp.x, mp.y, 0);
        }
    }
}