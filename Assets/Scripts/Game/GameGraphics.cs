using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameGraphics : MonoBehaviour
{
    public int selectedBottleIndex = -1; // default is -1

    [SerializeField] private Game game;

    [SerializeField] private GameObject bottlePrefab;

    [SerializeField] private GameObject ballPrefab;

    [SerializeField] private List<Transform> bottlePlaces;

    [SerializeField] public List<BottleGraphics> bottleGraphics;

    [Header("Moving ball")]

    [SerializeField] private GameObject ballToMove;

    [SerializeField] private float moveTime = 0.5f;

    public void Initialization(List<Game.Bottle> bottles)
    {
        for (int i = 0; i < bottles.Count; i++)
        {
            GameObject newBottle = Instantiate(bottlePrefab, bottlePlaces[i].position, Quaternion.identity);

            bottleGraphics.Add(newBottle.GetComponent<BottleGraphics>());
        }

        RefreshGameGraphic(bottles);
    }

    public void RefreshGameGraphic(List<Game.Bottle> bottles)
    {
        for (int i = 0; i < bottleGraphics.Count; i++)
        {
            bottleGraphics[i].RefreshBottleGraphic(bottles[i].balls);
            bottleGraphics[i].index = i;
        }
    }

    public void OnClickBottle(int bottleIndex)
    {
        if (selectedBottleIndex == -1)
        {
            selectedBottleIndex = bottleIndex;

            MoveBallUp(bottleIndex);

        } else
        {
            if (CheckSwitchBall(selectedBottleIndex, bottleIndex))
            {
                StartCoroutine(SwitchBall(selectedBottleIndex, bottleIndex));

                //game.SwitchBall(selectedBottleIndex, bottleIndex);
            }

            selectedBottleIndex = -1;
            ballToMove = null;
        }
    }

    private bool CheckSwitchBall(int bottleIndex1, int bottleIndex2)
    {
        bool checkSwitchBall;

        if (game.bottles[bottleIndex2].balls.Count == 0)
        {
            checkSwitchBall = true;
        } else
        {
            var type1 = game.bottles[bottleIndex1].balls[^1].type;
            var type2 = game.bottles[bottleIndex2].balls[^1].type;

            if (type1 == type2)
            {
                checkSwitchBall = true;
            } else
            {
                checkSwitchBall = false;
            }
        }
        return checkSwitchBall;
    }


    private IEnumerator SwitchBall(int bottleIndex1, int bottleIndex2)
    {
        var listBall = game.GetSwitchBallList(bottleIndex1, bottleIndex2);
        //listBall.Reverse();

        //get the target bottle
        BottleGraphics bottleGraphics1 = this.bottleGraphics[bottleIndex1];
        BottleGraphics bottleGraphics2 = this.bottleGraphics[bottleIndex2];

        var listBallCount = listBall.Count;

        var index = bottleGraphics1.ballGraphics.Count;

        //for (int i = index; i > index - listBallCount; i--)
        //{

        //    Debug.Log("i: " + i);

        //    ballToMove = bottleGraphics1.ballGraphics[i - 1].gameObject;

        //    Sequence sequence = DOTween.Sequence();

        //    ballToMove.transform.SetParent(bottleGraphics2.ballParent);

        //    sequence.Append(ballToMove.transform.DOMove(bottleGraphics1.pickUpPosition.position, moveTime / 3))
        //    .Append(ballToMove.transform.DOMove(bottleGraphics2.pickUpPosition.position, moveTime))
        //        .Append(ballToMove.transform.DOLocalMove(new Vector3(0, i, 0), moveTime));
        //}

        for (int i = index; i > index - listBallCount; i--)
        {
            yield return new WaitForSeconds(0.3f);

            Debug.Log("i: " + i);

            ballToMove = bottleGraphics1.ballGraphics[i - 1].gameObject;

            Sequence sequence = DOTween.Sequence();

            ballToMove.transform.SetParent(bottleGraphics2.ballParent);

            sequence.Append(ballToMove.transform.DOMove(bottleGraphics1.pickUpPosition.position, moveTime / 3))
            .Append(ballToMove.transform.DOMove(bottleGraphics2.pickUpPosition.position, moveTime))
            .OnComplete(
            () =>
            {
                if (ballToMove == null) return;

                for (int j = 0; j < listBallCount; j++)
                {
                    Debug.Log("j: " + j);
                    ballToMove.transform.DOLocalMove(new Vector3(0, bottleGraphics2.ballParent.transform.childCount + j - listBallCount, 0), moveTime);
                    Debug.Log("bottle2 child count: " + bottleGraphics2.ballParent.transform.childCount);
                }
            });
        }
    }

    private void DropBallQueue(int listBallCount, BottleGraphics bottleGraphics2)
    {
        if (ballToMove == null) return;

        for (int j = 0; j < listBallCount; j++)
        {
            ballToMove.transform.DOLocalMove(new Vector3(0, bottleGraphics2.ballGraphics.Count + j, 0), moveTime);
        }
    }

    private void MoveBallUp(int bottleIndex)
    {
        //Determine the chosen bottle
        Game.Bottle bottle = game.bottles[bottleIndex];
        BottleGraphics bottleGraphics = this.bottleGraphics[bottleIndex];

        //Get the top ball of that bottle
        if (bottle.balls.Count == 0) return;

        int index = bottle.balls.Count - 1;
        Game.Ball ball = bottle.balls[index];
        BallGraphics ballGraphics = bottleGraphics.ballGraphics[index];

        ballToMove = ballGraphics.gameObject;

        ballToMove.transform.DOMove(bottleGraphics.pickUpPosition.position, moveTime);
    }
}