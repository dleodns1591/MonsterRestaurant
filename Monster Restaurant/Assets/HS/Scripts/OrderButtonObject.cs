using TMPro;
using UnityEngine.UI;

public class OrderButtonObject : Singleton<OrderButtonObject>
{
    public TextMeshProUGUI BtnCookText;
    public TextMeshProUGUI BtnAskText;
    public Button CookingBtn => BtnCookText.transform.parent.GetComponent<Button>();
    public Button ReAskBtn => BtnAskText.transform.parent.GetComponent<Button>();

    public Button[] ChallengeBtns;
}