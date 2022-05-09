using Microsoft.JSInterop;

using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using static Beamlak.Blazor.Utilities.Enums;

namespace Beamlak.Blazor.Utilities
{
    public class BrowserStorage : IBrowserStorage
    {
        readonly IJSRuntime jsruntime;

        public BrowserStorage(IJSRuntime jSRuntime)
        {
            jsruntime = jSRuntime;
        }

        public async Task SaveItemAsync(object key, object value, BrowserStorageType storageType)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key), "Invalid item key (null)");

            string json = JsonSerializer.Serialize(value);
            byte[] data = Encoding.UTF8.GetBytes(json);
            string b64 = Convert.ToBase64String(data);
            await SaveStringAsync(key, b64, storageType);
        }

        public async Task<T> GetItemAsync<T>(object key, BrowserStorageType storageType)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key), "Invalid item key (null)");

            string b64 = await GetStringAsync(key, storageType);
            if (string.IsNullOrWhiteSpace(b64))
                return default;
            byte[] data = Convert.FromBase64String(b64);
            string json = Encoding.UTF8.GetString(data);
            return JsonSerializer.Deserialize<T>(json);
        }

        public async Task SaveStringAsync(object key, string value, BrowserStorageType storageType)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key), "Invalid item key (null)");

            string identifier = "sessionStorage.setItem";
            if (storageType is BrowserStorageType.SessionStorage)
            {
                identifier = "sessionStorage.setItem";
            }
            else if (storageType is BrowserStorageType.LocalStorage)
            {
                identifier = "localStorage.setItem";
            }
            await jsruntime.InvokeVoidAsync(identifier, key.ToString(), value).ConfigureAwait(false);
        }

        public async Task<string> GetStringAsync(object key, BrowserStorageType storageType)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key), "Invalid item key (null)");

            string identifier = "sessionStorage.getItem";
            if (storageType is BrowserStorageType.SessionStorage)
            {
                identifier = "sessionStorage.getItem";
            }
            else if (storageType is BrowserStorageType.LocalStorage)
            {
                identifier = "localStorage.getItem";
            }
            return await jsruntime.InvokeAsync<string>(identifier, key.ToString()).ConfigureAwait(false);
        }
        
        public async Task SaveStringArrayAsync(object key, string[] values, BrowserStorageType storageType)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key), "Invalid item key (null)");

            await SaveStringAsync(key, (values?.Any() ?? false) ? string.Join('\0', values) : "", storageType);
        }

        public async Task<string[]> GetStringArrayAsync(object key, BrowserStorageType storageType)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key), "Invalid item key (null)");

            var data = await GetStringAsync(key, storageType);
            return !string.IsNullOrWhiteSpace(data) ? data.Split('\0') : Array.Empty<string>();
        }

        public async Task RemoveAsync(object key, BrowserStorageType storageType)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key), "Invalid item key (null)");

            string identifier = "sessionStorage.removeItem";
            if (storageType is BrowserStorageType.SessionStorage)
            {
                identifier = "sessionStorage.removeItem";
            }
            else if (storageType is BrowserStorageType.LocalStorage)
            {
                identifier = "localStorage.removeItem";
            }
            await jsruntime.InvokeVoidAsync(identifier, key.ToString()).ConfigureAwait(false);
        }

        public async Task<bool> ContainsKeyAsync(object key, BrowserStorageType storageType)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key), "Invalid item key (null)");

            string identifier = "sessionStorage.getItem";
            if (storageType is BrowserStorageType.SessionStorage)
            {
                identifier = "sessionStorage.getItem";
            }
            else if (storageType is BrowserStorageType.LocalStorage)
            {
                identifier = "localStorage.getItem";
            }
            var value = await jsruntime.InvokeAsync<object>(identifier, key.ToString()).ConfigureAwait(false);
            return value is not null;
        }
    }
}
