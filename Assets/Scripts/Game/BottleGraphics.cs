using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BottleGraphics : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ballParent;

    [SerializeField] private List<BallGraphics> ballGraphics;

    public void Initialization(List<Game.Ball> balls)
    {
        for (int i = 0; i < balls.Count; i++)
        {
            var newBall =  Instantiate(ballPrefab);

            newBall.transform.SetParent(ballParent);

            ballGraphics.Add(newBall.GetComponent<BallGraphics>());
        }

        for (int i = 0; i < ballGraphics.Count; i++)
        {
            ballGraphics[i].SetColor(balls[i].type);

            ballGraphics[i].gameObject.transform.localPosition = new Vector2(0, i);
        }
    }

    public void RefreshBottleGraphic(List<Game.Ball> balls)
    {
        this.ballGraphics.Clear();

        BallGraphics[] ballGraphics;

        ballGraphics = ballParent.GetComponentsInChildren<BallGraphics>();

        for (int i = 0; i < ballGraphics.Length; i++)
        {
            this.ballGraphics.Add(ballGraphics[i]);
        }

        for (int i = 0; i < this.ballGraphics.Count; i++)
        {
            this.ballGraphics[i].SetColor(balls[i].type);

            this.ballGraphics[i].gameObject.transform.localPosition = new Vector2(0, i);
        }
    }
}
