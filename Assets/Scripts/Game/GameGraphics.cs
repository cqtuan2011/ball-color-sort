using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGraphics : MonoBehaviour
{
    public int selectedBottleIndex = -1; // default is -1

    [SerializeField] private Game game;

    [SerializeField] private GameObject bottlePrefab;

    [SerializeField] private GameObject ballPrefab;

    [SerializeField] private List<Transform> bottlePlaces;

    [SerializeField] public List<BottleGraphics> bottleGraphics;

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

            //MoveBallUp(bottleIndex);

        } else
        {
            game.SwitchBall(selectedBottleIndex, bottleIndex);

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
        GameObject previewBall = Instantiate(ballPrefab,ballGraphics.gameObject.transform.position, Quaternion.identity);

        previewBall.GetComponent<BallGraphics>().SetColor(BallType.BLUE);

    }
}
