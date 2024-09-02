using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;
public class PatternBuilder : MonoBehaviour
{
    public Image Box;
    void Start()
    {
        //PatternBuilding2();
        PatternBuilding();
        
    }

    public void PatternBuilding()
    {
       // var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        StringBuilder patternBuilder = new StringBuilder();
        int difference = 2; int maxRows = 9; int maxColumns = 10; int redColumns = 0; int whiteColumns = maxColumns; int redSequence = 1; int whiteSequence = 1;
        for (int rows = 1; rows <= maxRows; rows++)
        {
            if (rows <= maxColumns / 2)
            {
                whiteColumns -= difference;
                redColumns += difference;
            }
            else
            {
                whiteColumns += difference;
                redColumns -= difference;
            }
            for (int columns = 1; columns <= maxColumns; columns++)
            {
                if ((columns <= redColumns / 2) || (columns > maxColumns - (redColumns / 2)))
                {
                    if (Box)
                    {
                        Image box = Instantiate(Box);
                        box.transform.GetChild(0).GetComponent<Image>().color = Color.red;
                        box.transform.SetParent(this.transform, false);
                        box.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = redSequence.ToString();
                        patternBuilder.Append("   " + redSequence.ToString() + "     ");
                         redSequence++;
                    }
                }
                else
                {
                    if (Box)
                    {
                        Image box = Instantiate(Box);
                        //box.transform.GetChild(0).GetComponent<Image>().color = Color.white;
                        box.transform.SetParent(this.transform, false);
                        box.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = whiteSequence.ToString();
                        patternBuilder.Append("   " + whiteSequence.ToString() + "     ");
                        whiteSequence++;
                    }
                }
            }
            patternBuilder.AppendLine();
        }

        Debug.Log(patternBuilder.ToString());
       // stopwatch.Stop();
        //Debug.Log($"Execution Time: {stopwatch.Elapsed.TotalMilliseconds} ms");
    }
    //    int outerSq =1; int innerSq =1;
public void PatternBuilding2()
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        StringBuilder builder = new StringBuilder();
        for (int i = 1; i < 6; i++)
        {
            for (int j = 1; j <= i; j++)
            {
                builder.Append("<color=red>*</color>");
            }
            for (int k = i; k < 5; k++)
            {
                builder.Append("<color=white>*</color>");
            }
            for (int j = i; j < 5; j++)
            {
                builder.Append("<color=white>*</color>");
            }
            for (int k = 1; k <= i; k++)
            {
                builder.Append("<color=red>*</color>");
            }

            builder.AppendLine();
        }
        for (int i = 1; i < 5; i++)
        {
            for (int k = i; k < 5; k++)
            {
                builder.Append("<color=red>*</color>");
            }
            for (int k = 1; k <= i; k++)
            {
                builder.Append("<color=white>*</color>");
            }
            for (int j = 1; j <= i; j++)
            {
                builder.Append("<color=white>*</color>");
            }
            for (int k = i; k < 5; k++)
            {
                builder.Append("<color=red>*</color>");
            }
            builder.AppendLine();
        }

        Debug.Log(builder.ToString());
        stopwatch.Stop();
        Debug.Log($"Execution Time: {stopwatch.Elapsed.TotalMilliseconds} ms");
    }
   


}
