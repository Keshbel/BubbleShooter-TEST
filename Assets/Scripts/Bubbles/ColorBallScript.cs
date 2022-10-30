using UnityEngine;

public enum BallColor
{
    blue = 1,
    green,
    red,
    violet,
    yellow,
    random
}

public class ColorBallScript : MonoBehaviour
{
    public Sprite[] sprites;
    public BallColor mainColor;

    public void SetColor(BallColor color)
    {
        mainColor = color;
        foreach (Sprite item in sprites)
        {
            if (item.name == "ball_" + color)
            {
                GetComponent<SpriteRenderer>().sprite = item;
                SetSettings(color);
                gameObject.tag = "" + color;
            }
        }
    }

    private void SetSettings(BallColor color)
    {
    }

    public void SetColor(int color)
    {
        mainColor = (BallColor)color;
        GetComponent<SpriteRenderer>().sprite = sprites[color];
    }

    public void ChangeRandomColor()
    {
        MainScript.Instance.GetColorsInGame();
        SetColor((BallColor)MainScript.colorsDict[Random.Range(0, MainScript.colorsDict.Count)]);
        GetComponent<Animation>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -16 && transform.parent == null) { Destroy(gameObject); }
    }
}
