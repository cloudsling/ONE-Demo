using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Demo
{
    public static class LastUpdate
    {
        static StorageFolder cacheFolder = ApplicationData.Current.LocalCacheFolder;
        public static async Task SaveX(string x)
        {
            await SaveTempItemInCacheFolder("x.txt", "dayObj.txt", "question.txt", "article.txt");

            StorageFile file = await cacheFolder.GetFileAsync("x.txt");
            await FileIO.WriteTextAsync(file, x);
        }
        public static async Task SaveDayObj(string x)
        {
            StorageFile file = await cacheFolder.GetFileAsync("dayObj.txt");
            await FileIO.WriteTextAsync(file, x);
        }
        public static async Task<string> LoadDayObj()
        {
            if (await IsExistFileInCache("dayObj.txt"))
            {
                StorageFile file = await cacheFolder.GetFileAsync("dayObj.txt");
                return await FileIO.ReadTextAsync(file);
            }
            return "程序未曾联过网";
        }
        public static async Task<string> LoadX()
        {
            if (await IsExistFileInCache("x.txt"))
            {
                StorageFile file = await cacheFolder.GetFileAsync("x.txt");
                return await FileIO.ReadTextAsync(file);
            }
            return "程序未曾联过网";
        }
        public static async void SaveSth(string sth, string name)
        {
            StorageFile file = await cacheFolder.GetFileAsync(name);
            await FileIO.WriteTextAsync(file, sth);
        }
        public static async Task<string> LoadSth(string name)
        {
            if (await IsExistFileInCache(name))
            {
                StorageFile file = await cacheFolder.GetFileAsync(name);
                return await FileIO.ReadTextAsync(file);
            }
            return "程序未曾联过网";
        }

        public static async Task SaveTempItemInCacheFolder(params string[] nameColl)
        {
            for (int i = 0; i < nameColl.Length; i++)
            {
                if (!await IsExistFileInCache(nameColl[i]))
                {
                    await cacheFolder.CreateFileAsync(nameColl[i]);
                }
            }
        }

        public static async Task<bool> IsExistFileInCache(string name)
        {
            var item = await cacheFolder.TryGetItemAsync(name);
            if (item == null)
            {
                return false;
            }
            return true;
        }
    }
}
