using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BottleGraphics : MonoBehaviour
{
    public int index;

    [SerializeField] private GameObject ballPrefab;

    [SerializeField] private Transform ballParent;

    [SerializeField] private List<BallGraphics> ballGraphics;

    private GameGraphics gameGraphics;

    private void Awake()
    {
        gameGraphics = FindObjectOfType<GameGraphics>();
    }

    private void OnMouseUpAsButton()
    {
        gameGraphics.OnClickBottle(this.index);
    }

    public void RefreshBottleGraphic(List<Game.Ball> balls)
    {
        ballGraphics.Clear();

        foreach (Transform ball in ballParent.transform)
        {
            Destroy(ball.gameObject);
        }

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
}
