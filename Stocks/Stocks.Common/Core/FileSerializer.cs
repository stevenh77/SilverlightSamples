using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Stocks.Common.Core
{
  public class FileSerializer
  {
    public void SerializeJson(string filename, object obj)
    {
      string json = JsonConvert.SerializeObject(obj);
      Serialize(filename, json);
    }

    public void Serialize(string filename, string text)
    {
      using (StreamWriter writer = new StreamWriter(filename))
        { writer.Write(text); }
    }

    public void Serialize(string filename, IFormatter formatter, object objectToSerialize)
    {
      using (Stream stream = File.Open(filename, FileMode.Create))
        formatter.Serialize(stream, objectToSerialize);
    }

    public string Deserialize(string filename)
    { 
      StringBuilder sb = new StringBuilder();
      using (StreamReader reader = new StreamReader(filename))
        if (reader != null)
          sb.AppendLine(reader.ReadToEnd());

      return sb.ToString();       
    }

    public List<T> DeserializeJson<T>(string filename)
    {
      var json = Deserialize(filename);
      return JsonConvert.DeserializeObject<List<T>>(json);
    }

    public T[] Deserialize<T>(string filename, IFormatter formatter, Type type)
    {
      using (Stream stream = File.Open(filename, FileMode.Open))
      {
        var items = (T[])formatter.Deserialize(stream);
        return items;
      }
    }
  }
}
