//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;

//namespace Lantis
//{
//    public class LantisDictronaryList<K, V>
//    {
//        private Dictionary<K, V> dictionary;
//        private List<K> listKey;
//        private List<V> listValue;

//        public LantisDictronaryList()
//        {
//            dictionary = new Dictionary<K, V>();
//            listKey = new List<K>();
//            listValue = new List<V>();
//        }

//        public bool HasKey(K key)
//        {
//            return dictionary.ContainsKey(key);
//        }

//        public bool AddValue(K key, V value)
//        {
//            if (!HasKey(key))
//            {
//                dictionary.Add(key, value);
//                listKey.Add(key);
//                listValue.Add(value);
//            }
//            else
//            {
//                Debug.LogError("key has ready");
//            }

//            return false;
//        }

//        public void RemoveKey(K key)
//        {
//            if (HasKey(key))
//            {
//                listKey.Remove(key);
//                listValue.Remove(dictionary[key]);
//                dictionary.Remove(key);
//            }
//        }

//        public List<K> KeyToList()
//        {
//            return listKey;
//        }

//        public List<V> ValueToList()
//        {
//            return listValue;
//        }

//        public V this[K key]
//        {
//            get
//            {
//                return dictionary[key];
//            }
//        }

//        public void Clear()
//        {
//            dictionary.Clear();
//            listKey.Clear();
//            listValue.Clear();
//        }
//    }
//}