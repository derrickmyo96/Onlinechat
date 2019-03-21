using UnityEngine.UI;
using UnityEngine;

public class ChatInputField : MonoBehaviour
{
    public ChatterManager chatManager;
    private InputField inputField;

    private void Start()
    {
        inputField = GetComponent<InputField>();
    }

    public void ValueChanged()
    {
        if (inputField.text.Contains("\n"))
        {
            chatManager.WriteMessage(inputField);
        }
    }

}
