using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIType 
{
    private string _name;
    private string _path;
    public string Name { get => _name; }
    public string Path { get => _path; }

    public UIType(string name,string path)
    {
        _name = name;
        _path = path;
    }
}
