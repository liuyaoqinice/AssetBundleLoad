using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using System.Xml.Serialization;

namespace GameData.Scripts
{
    public class ResourcesTest : MonoBehaviour
    {
        public GameObject prefab;


        private void Start()
        {
            // var assetBundle= AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/attack");
            // var obj = Instantiate(assetBundle.LoadAsset<GameObject>("Attack"));

            // var obj= UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/GameData/Prefabs/Attack.prefab");
            // var go = Instantiate(obj);
            
            
            SerilizeTest();
            BinarySerTest();
        }


        private void SerilizeTest()
        {
            var testSerilize = new TestSerilize() {Id = 10, Name = "小明", List = new List<int>() {1, 2, 3, 4}};

            XmlSerilize(testSerilize);
            DeSerilizeTest();
        }


        private void DeSerilizeTest()
        {
            var data = XmlDeSerilize();
            print($"id{data.Id} name {data.Name} list{data.List}");
        }


        private void XmlSerilize(TestSerilize data)
        {
            var sm = new FileStream(Application.dataPath + "/test.xml", FileMode.Create, FileAccess.ReadWrite,
                FileShare.ReadWrite);
            var sw = new StreamWriter(sm, Encoding.UTF8);

            var xml = new XmlSerializer(data.GetType()); //获取需要序列化的类型

            xml.Serialize(sw, data); //序列化
            sw.Close();
            sm.Close();
        }

        private TestSerilize XmlDeSerilize()
        {
            var fs = new FileStream(Application.dataPath + "/test.xml", FileMode.Open, FileAccess.ReadWrite,
                FileShare.ReadWrite);
            var xml = new XmlSerializer(typeof(TestSerilize));
            var testSerilize = (TestSerilize) xml.Deserialize(fs);
            fs.Close();
            return testSerilize;
        }


        #region 二进制转class

        private void BinarySerTest()
        {
            var testSerilize = new TestSerilize() {Id = 10, Name = "二进制", List = new List<int>() {1, 2, 3, 4}};
            BinarySerilize(testSerilize);
            DeBinarySerilize();
        }

        private void BinarySerilize(TestSerilize data)
        {
            var fs = new FileStream(Application.dataPath + "/test.bytes", FileMode.Create, FileAccess.ReadWrite,
                FileShare.ReadWrite);
            var bf=new BinaryFormatter();
            bf.Serialize(fs,data);//序列化
            fs.Close();
        }

        private void DeBinarySerilize()
        {
            var data = BinaryDeSerilize();
            
            print($"id {data.Id} name {data.Name}" );
        }
        
        
        private TestSerilize BinaryDeSerilize()
        {
            var textAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/test.bytes");
            var stream =new MemoryStream(textAsset.bytes);
            var bf = new BinaryFormatter();
            var testSerilize = bf.Deserialize(stream) as TestSerilize; 
            stream.Close();
            return testSerilize;
        }

        #endregion
    }
}