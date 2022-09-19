using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGraphics : MonoBehaviour
{
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

        for (int i = 0; i < bottleGraphics.Count; i++)
        {
            bottleGraphics[i].Initialization(bottles[i].balls);
        }
    }

    public void RefreshGameGraphic(List<Game.Bottle> bottles)
    {
        for (int i = 0; i < bottleGraphics.Count; i++)
        {
            bottleGraphics[i].RefreshBottleGraphic(bottles[i].balls);
        }
    }
}
