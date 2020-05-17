using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;
using SAINTClicker.Models;
using SAINTClicker.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureService))]
namespace SAINTClicker.Services
{
   public class AzureService
    {
        public MobileServiceClient Client { get; private set; }
        private IMobileServiceSyncTable<User> userTable;

        private async Task Initialize()
        {
            if (Client?.SyncContext?.IsInitialized ?? false)
                return;

            var appUrl = "https://saintclicker.azurewebsites.net/";

            Client = new MobileServiceClient(appUrl);

            // var path = Path.Combine(MobileServiceClient.DefaultDatabasePath, "usersync.db");
            var fileName = "SAINTUserDB.db";
            var store = new MobileServiceSQLiteStore(fileName);

            store.DefineTable<User>();

            await Client.SyncContext.InitializeAsync(store);

            userTable = Client.GetSyncTable<User>();//client handles sync tables automatically (offline)
        }

        private async Task SyncUser()
        {
            await Initialize();
            try //try to access the internet for cloud
            {
                //dont try to push pull data if you are offline. therefore use cross connectivity

                //we are offline, skip
                if (!CrossConnectivity.Current.IsConnected)
                    return;
                //IF ONLINE
                await Client.SyncContext.PushAsync();//send anything we need to the server

                await userTable.PullAsync("allUser",userTable.CreateQuery());

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }




           // await userTable.PullAsync("allUser", userTable.CreateQuery());
            //await Client.SyncContext.PushAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            await Initialize();
            await SyncUser();
            var data = await userTable
                .OrderBy(c => c.firstName)
                .ToEnumerableAsync(); //this op is on the local database

            return data;
        }


        public async Task<IEnumerable<User>> GetUsers(User user)
        {
            await Initialize();
            await SyncUser();
            var data = await userTable
                //.Where(user=>user.username)
                .Where(c => c.username == user.username && c.password==user.password)
               //  .Select(c => c.firstName).ToListAsync();
              // .OrderBy(c => c.firstName)
                .ToEnumerableAsync(); //this op is on the local database

            return data;
        }


        public async Task<bool> AddUser(User user)
        {
            await Initialize();
             try
             {
                 await userTable.InsertAsync(user);//locally insert
                 await SyncUser();
                 return true;
             }
             catch
             {
                 return false;
             }
           

            
        }



        public async Task<IEnumerable<User>> GetUsersProfile(User user)
        {
            await Initialize();
            await SyncUser();
            var data = await userTable
                //.Where(user=>user.username)
                .Where(c => c.myclass==user.myclass)
                //  .Select(c => c.firstName).ToListAsync();
                // .OrderBy(c => c.firstName)
                .ToEnumerableAsync(); //this op is on the local database

            return data;
        }


    }
}
