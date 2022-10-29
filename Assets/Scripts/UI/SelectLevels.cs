using UnityEngine;
using UnityEngine.UI;

public class SelectLevels : MonoBehaviour
{
    int latestFile;
    public GameObject levelPrefab;
    public Vector3 startPosition;
    public Vector2 offset;
    public int countInRow = 4;
    public int countInColumn = 4;
    public Button backButton;
    public Button nextButton;
    int firstShownLevelInGrid;
    
    void Start()
    {
        GenerateGrid();
    }


    /// <summary>
    /// Generate grid layout levels
    /// </summary>
    /// <param name="genfrom"></param> where to start generating the index cursor
    void GenerateGrid(int genfrom = 0)
    {
        int l;
        int posCounter = 0;
        ClearLevels();
        firstShownLevelInGrid = genfrom;
        latestFile = GetLastLevel();
        for (l = genfrom; l < latestFile; l++)
        {
            GameObject level = Instantiate(levelPrefab, transform, true);
            level.GetComponent<Level>().number = l+1;
            level.transform.localPosition = startPosition + Vector3.right * (posCounter % countInRow) * offset.x + Vector3.down * (posCounter / countInColumn) * offset.y;
            level.transform.localScale = Vector3.one;
            if (posCounter + 1 >= countInRow * countInColumn) break;
            posCounter++;
        }
        if (genfrom == 0) backButton.gameObject.SetActive(false);
        else if (genfrom > 0) backButton.gameObject.SetActive(true);
        if (l + 1 >= latestFile) nextButton.gameObject.SetActive(false);
        else nextButton.gameObject.SetActive(true);

    }

    /// <summary>
    /// Clear previously cached level data
    /// </summary>
    void ClearLevels()
    {
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }
    }

    public void Next()
    {
        GenerateGrid(firstShownLevelInGrid + countInRow * countInColumn);
    }

    public void Back()
    {
        GenerateGrid(firstShownLevelInGrid - countInRow * countInColumn);

    }

    /// <summary>
    /// Get the last level, the total number of levels
    /// </summary>
    /// <returns></returns>Number of levels
    int GetLastLevel()
    {
        TextAsset mapText = null;
        for (int i = 1; i < 50000; i++)
        {
            mapText = Resources.Load("Levels/" + i) as TextAsset;
            if (mapText == null)
            {
                return i - 1;
            }
        }
        return 0;
    }
}
