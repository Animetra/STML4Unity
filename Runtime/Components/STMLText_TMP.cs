using TMPro;
using UnityEngine;

public class STMLText_TMP : MonoBehaviour
{
    [SerializeField] private string _id;
    public void Awake()
    {
        STMLManager.Initialize("en");
        var textObject = GetComponent<TextMeshProUGUI>();
        Debug.Log(STMLManager.GetText(_id));
        textObject.text = STMLManager.GetText(_id);
    }
}

