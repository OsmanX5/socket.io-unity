using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class UnityStringDictionary 
{
    [SerializeField]
    List<UnityStringDictionaryElement> dict = new List<UnityStringDictionaryElement>();

    public void Add(UnityStringDictionaryElement element)
    {
        dict.Add(element);
    }
    public UnityStringDictionary() { }
    public UnityStringDictionary(Dictionary<string,string> startDict)
    {
        foreach(KeyValuePair<string,string> kvp in startDict)
        {
            dict.Add(new UnityStringDictionaryElement() { Key = kvp.Key, Value = kvp.Value });
        }
    }
    public Dictionary<string,string> GetDictionary()
    {
        var res = new Dictionary<string,string>();
        foreach (var kvp in dict)
        {
            res[kvp.Key] = kvp.Value;
        }
        return res;
    }
}

[Serializable]
public class UnityStringDictionaryElement
{

    public string Key;
    public string Value;
}
