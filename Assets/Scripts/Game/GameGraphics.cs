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
            game.SwitchBall(selectedBottleIndex, bottleIndex);

            SwitchBallGraphic(bottleIndex);

            selectedBottleIndex = -1;
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

        //Create new ball graphic for the movement
        ballToMove = Instantiate(ballPrefab,ballGraphics.gameObject.transform.position, Quaternion.identity);
        ballToMove.GetComponent<BallGraphics>().SetColor(ball.type);
        
        Destroy(ballGraphics.gameObject);
        ballToMove.transform.DOMove(bottleGraphics.pickUpPosition.position, moveTime);
    }

    private void SwitchBallGraphic(int toBottleIndex)
    {
        if (ballToMove == null) return;

        BottleGraphics bottleGraphics = this.bottleGraphics[toBottleIndex];

        Sequence sequence = DOTween.Sequence();

        ballToMove.transform.SetParent(bottleGraphics.ballParent);

        sequence.Append(ballToMove.transform.DOMove(bottleGraphics.pickUpPosition.position, moveTime))
            .Append(ballToMove.transform.DOMove(bottleGraphics.pickUpPosition.position, moveTime))
            .Append(ballToMove.transform.DOLocalMove(new Vector3(0, bottleGraphics.ballGraphics.Count - 1, 0), moveTime))
            .OnComplete(DestroyBall);
    }

    private void DestroyBall()
    {
        Destroy(ballToMove);
    }
}
