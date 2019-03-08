using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BaseSite.App.Persistence
{
    public abstract class KeyedDataStore<TKey, TValue> : IKeyedDataStore<TKey, TValue>
    {

        protected abstract String OutputFile { get; }

        protected virtual IDictionary<TKey, TValue> InitialStore { get => null; }

        private String _fullPath;

        private IDictionary<TKey, TValue> InternalStore { get; set; }

        public ICollection<TKey> Keys => InternalStore.Keys;

        public ICollection<TValue> Values => InternalStore.Values;


        public TValue this[TKey key]
        {
            get => InternalStore[key];
            set
            {
                InternalStore[key] = value;
                Updated?.Invoke(this, new EventArgs());
                SaveSettings();
            }
        }

        public int Count => InternalStore.Count;

        public bool IsReadOnly => InternalStore.IsReadOnly;

        public event EventHandler Updated;

        public KeyedDataStore()
        {
            _fullPath = Path.Combine(Environment.CurrentDirectory, OutputFile);
            if (!File.Exists(_fullPath))
            {
                if (InitialStore != null)
                {
                    InternalStore = InitialStore;
                }
                else
                {
                    InternalStore = new Dictionary<TKey, TValue>();
                }
                var output = JsonConvert.SerializeObject(InternalStore);
                File.WriteAllText(_fullPath, output);
            }
            LoadSettings();
        }

        private void LoadSettings()
        {
            InternalStore = JsonConvert.DeserializeObject<IDictionary<TKey, TValue>>(File.ReadAllText(_fullPath));
        }

        private void SaveSettings()
        {
            File.WriteAllText(_fullPath, JsonConvert.SerializeObject(InternalStore));
        }

        protected void RaiseUpdated(EventArgs args = null)
        {
            if(args == null)
            {
                args = EventArgs.Empty;
            }
            Updated?.Invoke(this, args);
        }

        public void Persist()
        {
            SaveSettings();
        }

        public void Add(TKey key, TValue value)
        {
            InternalStore.Add(key, value);
            Updated?.Invoke(this, new EventArgs());
            SaveSettings();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            InternalStore.Add(item);
            Updated?.Invoke(this, new EventArgs());
            SaveSettings();
        }

        public void Clear()
        {
            InternalStore.Clear();
            Updated?.Invoke(this, new EventArgs());
            SaveSettings();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return InternalStore.Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            return InternalStore.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            InternalStore.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return InternalStore.GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            bool success = InternalStore.Remove(key);
            Updated?.Invoke(this, new EventArgs());
            SaveSettings();
            return success;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            bool success = InternalStore.Remove(item);
            Updated?.Invoke(this, new EventArgs());
            SaveSettings();
            return success;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return InternalStore.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return InternalStore.GetEnumerator();
        }
    }
}
