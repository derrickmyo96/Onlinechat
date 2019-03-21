using UnityEngine.UI;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

public class ChatterManager : ChatterManagerBehavior
{
    //Transform chatContent is content that chatMessage is going into
    public Transform chatContent;
    //GameObject chatMessage is the prefab
    public GameObject chatMessage;

    private string username;

    protected override void NetworkStart()
    {
        base.NetworkStart();
        if (networkObject.IsServer)
            username = "Server";
        else
            username = "Client";
    }

    //Function to send message
    public void WriteMessage(InputField sender)
    {
        if (!string.IsNullOrEmpty(sender.text) && sender.text.Trim().Length > 0)
        {
            //To get rid of new line
            sender.text = sender.text.Replace("\r", string.Empty).Replace("\n", string.Empty);
            networkObject.SendRpc(RPC_TRANSMIT_MESSAGE, Receivers.All, username, sender.text.Trim());

            //Send out text and clear input field
            sender.text = string.Empty;
            //Focus on typing in input field without having to keeping tapping input field
            sender.ActivateInputField();
        }
    }

    //Function to receive message
    public override void TransmitMessage(RpcArgs args)
    {
        //throw new System.NotImplementedException();
        string username = args.GetNext<string>();
        string message = args.GetNext<string>();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(message))
        {
            //Message or username was empty, so don't display to anyone
            return;
        }

        GameObject newMessage = Instantiate(chatMessage, chatContent);
        Text content = newMessage.GetComponent<Text>();

        content.text = string.Format(content.text, username, message);
    }
}
