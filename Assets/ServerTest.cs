using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine;

public class ServerTest : MonoBehaviour
{
    [SerializeField]
    private SudokuWizard _sudokuWizard;

    [ContextMenu("Test")]
    public void ServerRequest()
    {
        var path = "https://localhost:7271/level/qwe";
        string responseString;
        using (var client = new WebClient())
        {
            responseString = client.DownloadString(path);
        }
        responseString = responseString.Trim('"');
        responseString = Regex.Unescape(responseString);
        var obj = Serializer.DeserializeFromString<Grid>(responseString);

        _sudokuWizard.Fill(obj);
    }
}
[Serializable]
public class MyClass
{
    public int Id { get; set; }
    public string Name { get; set; }

    public MyClass(int id, string name)
    {
        Id = id;
        Name = name;
    }
}