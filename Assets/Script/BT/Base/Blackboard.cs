using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    // 行为树的共享数据存储中心
    public class Blackboard : Singleton<Blackboard>
    {
        private Dictionary<string, object> data = new Dictionary<string, object>();
    
        // 设置数据
        public void SetValue<T>(string key, T value)
        {
            data[key] = value;
        }
    
        // 获取数据
        public T GetValue<T>(string key, T defaultValue = default)
        {
            if (data.TryGetValue(key, out object value) && value is T typedValue)
            {
                return typedValue;
            }
            return defaultValue;
        }
    
        // 检查数据是否存在
        public bool HasValue(string key)
        {
            return data.ContainsKey(key);
        }
    
        // 清除数据
        public void ClearValue(string key)
        {
            data.Remove(key);
        }
    }
}
