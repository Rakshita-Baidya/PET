using System.Text.Json;

namespace PET.Models
{
    public class PreferencesStoreClone
    {

        public event Action? OnChange;

        public void Set(string key, object value)
        {
            string keyvalue = JsonSerializer.Serialize(value);
            if (keyvalue != null && !string.IsNullOrEmpty(keyvalue))
            {
                Preferences.Set(key, keyvalue);
                OnChange?.Invoke();
            }
        }

        public T Get<T>(string key)
        {
            T UnpackedValue = default;
            string keyvalue = Preferences.Get(key, string.Empty);

            if (keyvalue != null && !string.IsNullOrEmpty(keyvalue))
            {
                UnpackedValue = JsonSerializer.Deserialize<T>(keyvalue);
            }
            return UnpackedValue;
        }

        public void Delete(string key)
        {
            Preferences.Remove(key);
            OnChange?.Invoke();
        }

        public bool Exists(string key)
        {
            return Preferences.ContainsKey(key);
        }

        public void ClearAll()
        {
            Preferences.Clear();
            OnChange?.Invoke();
        }

    }
}
