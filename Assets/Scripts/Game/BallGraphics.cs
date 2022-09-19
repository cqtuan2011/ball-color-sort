using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGraphics : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetColor(BallType type)
    {
        switch (type)
        {
            case BallType.RED:
                spriteRenderer.color = Color.red;
                break;
            case BallType.GREEN:
                spriteRenderer.color = Color.green;
                break;
            case BallType.ORANGE:
                
                break;
            case BallType.PINK:
                
                break;
            case BallType.PURPLE:
                
                break;
            case BallType.BLUE:
                spriteRenderer.color = Color.blue;
                break;
            case BallType.YELLOW:
                spriteRenderer.color = Color.yellow;
                break;
            default:
                break;
        }
    }
}
