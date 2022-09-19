using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Game : MonoBehaviour
{
    [SerializeField] private GameGraphics gameGraphics;

    public List<Bottle> bottles;

    private void Start()
    {
        bottles = new List<Bottle>();

        bottles.Add(new Bottle
        {
            balls = new List<Ball> { new Ball { type = BallType.RED }, new Ball { type = BallType.RED } }
        });

        bottles.Add(new Bottle
        {
            balls = new List<Ball> { new Ball { type = BallType.RED }, new Ball { type = BallType.RED } }
        });

        gameGraphics.Initialization(bottles);

        PrintBottles();

        SwitchBall(bottles[0], bottles[1]);

        PrintBottles();
    }

    public void PrintBottles()
    {
        Debug.Log("====Bottle====");

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < bottles.Count; i++)
        {
            Bottle b = bottles[i];
            sb.Append("Bottle " + (i + 1) + ": ");

            foreach (var ball in b.balls)
            {
                sb.Append(ball.type);
                sb.Append(", ");
            }
            Debug.Log(sb.ToString());
            sb.Clear();
        }

        bool isWin = CheckWinCondition();

        Debug.Log("Is win: " + isWin);
        Debug.Log("Total bottles is: " + bottles.Count);


        Debug.Log("Bottle 1 balls cound: " + bottles[0].balls.Count);
        Debug.Log("Bottle 2 balls cound: " + bottles[1].balls.Count);
    } // Console debug

    public void SwitchBall(Bottle bottle1, Bottle bottle2)
    {
        //Generate list of bottles
        List<Ball> bottle1Balls = bottle1.balls;
        List<Ball> bottle2Balls = bottle2.balls;

        if (bottle1Balls.Count == 0)
            return;

        if (bottle2Balls.Count == 4)
            return;

        Ball topBall1 = bottle1Balls[bottle1.balls.Count - 1];

        if (bottle2Balls.Count > 0)
        {
            Ball topBall2 = bottle2Balls[bottle2.balls.Count - 1];

            if (topBall1.type != topBall2.type)
            {
                return;
            } else
            {
                bottle1Balls.Remove(topBall1);
                bottle2Balls.Add(topBall1);
            }
        } else
        {
            bottle1Balls.Remove(topBall1);
            bottle2Balls.Add(topBall1);
        }
        //gameGraphics.RefreshGameGraphic(bottles);
    }

    public bool CheckWinCondition()
    {
        bool winFlag = true;

        foreach (var bottle in bottles)
        {
            if (bottle.balls.Count == 0) // pass if it's empty bottle
                continue;
            if (bottle.balls.Count < 4)
            {
                winFlag = false;
                break;
            }

            bool sameTypeFlag = true;

            BallType type = bottle.balls[0].type;

            foreach (var ball in bottle.balls)
            {
                if (ball.type != type)
                {
                    sameTypeFlag = false;
                    break;
                }
            }

            if (!sameTypeFlag)
            {
                winFlag = false;
                break;
            }
        }
        return winFlag;
    }

    public class Bottle
    {
        public List<Ball> balls = new List<Ball>();
    }

    public class Ball
    {
        public BallType type;
    }
}
