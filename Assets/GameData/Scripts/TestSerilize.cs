using System.Collections.Generic;
using System.Xml.Serialization;

namespace GameData.Scripts
{
    [System.Serializable]
    
    public class TestSerilize
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }
        
        [XmlAttribute("name")]//一般数据转换
        public string Name { get; set; }
        
        [XmlElement("List")]//数组类型转换
        public List<int> List { get; set; }
    }
}