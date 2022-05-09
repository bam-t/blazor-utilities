using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Beamlak.Blazor.Utilities.Enums;

namespace Beamlak.Blazor.Utilities
{
    public interface IBrowserStorage
    {
        /// <summary>
        /// Add a value to the browser storage. If the specified key already exists, its value will be replaced with the new value.
        /// </summary>
        /// <remarks>This method is only supported on .NET 5 and above.</remarks>
        /// <param name="key">The key to use to save to and retrieve from browser storage.</param>
        /// <param name="value">The value to store to the browser storage.</param>
        /// <param name="storageType">The storage type to use. Either LocalStorage or SessionStorage.</param>
        public Task SaveItemAsync(object key, object value, BrowserStorageType storageType);

        /// <summary>
        /// Get an item from browser storage. If an item wasn't found, the default value of the specified Type will be returned.
        /// </summary>
        /// <remarks>This method is only supported on .NET 5 and above.</remarks>
        /// <typeparam name="T">The type of the item to get.</typeparam>
        /// <param name="key">The key previously used to save to browser storage.</param>
        /// <param name="storageType">The storage type to use. Either LocalStorage or SessionStorage.</param>
        /// <returns>The object previously saved to browser storage.</returns>
        public Task<T> GetItemAsync<T>(object key, BrowserStorageType storageType);

        /// <summary>
        /// Save a string value to browser storage.
        /// </summary>
        /// <param name="key">The key to use to save to and retrieve from browser storage.</param>
        /// <param name="value">The string value to save to browser storage.</param>
        /// <param name="storageType">The storage type to use. Either LocalStorage or SessionStorage.</param>
        public Task SaveStringAsync(object key, string value, BrowserStorageType storageType);

        /// <summary>
        /// Get a string value from browser storage.
        /// </summary>
        /// <param name="key">The key previously used to save to browser storage.</param>
        /// <param name="storageType">The storage type to use. Either LocalStorage or SessionStorage.</param>
        /// <returns>The string previously saved to browser storage.</returns>
        public Task<string> GetStringAsync(object key, BrowserStorageType storageType);

        /// <summary>
        /// Save an array of string values to browser storage.
        /// </summary>
        /// <param name="key">The key previously used to save to browser storage.</param>
        /// <param name="values">The array of string values to save to browser storage.</param>
        /// <param name="storageType">The storage type to use. Either LocalStorage or SessionStorage.</param>
        public Task SaveStringArrayAsync(object key, string[] values, BrowserStorageType storageType);

        /// <summary>
        /// Get an array of string values from browser storage.
        /// </summary>
        /// <param name="key">The key previously used to save to browser storage.</param>
        /// <param name="storageType">The storage type to use. Either LocalStorage or SessionStorage.</param>
        /// <returns>The array of string values previously saved to browser storage.</returns>
        public Task<string[]> GetStringArrayAsync(object key, BrowserStorageType storageType);

        /// <summary>
        /// Remove an item from browser storage.
        /// </summary>
        /// <param name="key">The key previously used to save to browser storage.</param>
        /// <param name="storageType">The storage type to use. Either LocalStorage or SessionStorage.</param>
        public Task RemoveAsync(object key, BrowserStorageType storageType);

        /// <summary>
        /// Check if a key exists in the browser storage.
        /// </summary>
        /// <param name="key">The key to check in the browser storage.</param>
        /// <param name="storageType">The storage type to use. Either LocalStorage or SessionStorage.</param>
        /// <returns><see cref="true"/> if the key exists in browser storage, <see cref="false"/> otherwise .</returns>
        public Task<bool> ContainsKeyAsync(object key, BrowserStorageType storageType);
    }
}
