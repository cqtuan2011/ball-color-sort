using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGraphics : MonoBehaviour
{
    public int selectedBottleIndex = -1; // default is -1

    [SerializeField] private Game game;

    [SerializeField] private GameObject bottlePrefab;

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
        } else
        {
            game.SwitchBall(selectedBottleIndex, bottleIndex);

            selectedBottleIndex = -1;
        }
    } 
}
