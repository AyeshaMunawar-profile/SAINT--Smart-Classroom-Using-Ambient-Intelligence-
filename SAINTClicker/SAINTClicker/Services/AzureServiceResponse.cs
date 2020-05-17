using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;
using SAINTClicker.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace SAINTClicker.Services
{
   public  class AzureServiceResponse
    {
        public MobileServiceClient Client { get; private set; }
        private IMobileServiceSyncTable<Responses> responsesTable;

        private async Task Initialize()
        {
            if (Client?.SyncContext?.IsInitialized ?? false)
                return;

            var appUrl = "https://saintclicker.azurewebsites.net/";

            Client = new MobileServiceClient(appUrl);

            // var path = Path.Combine(MobileServiceClient.DefaultDatabasePath, "usersync.db");
            var fileName = "SAINTResponses.db";
            var store = new MobileServiceSQLiteStore(fileName);

            store.DefineTable<Responses>();

            await Client.SyncContext.InitializeAsync(store);

            responsesTable = Client.GetSyncTable<Responses>();//client handles sync tables automatically (offline)
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

                await responsesTable.PullAsync("allResponses", responsesTable.CreateQuery());

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }




            // await userTable.PullAsync("allUser", userTable.CreateQuery());
            //await Client.SyncContext.PushAsync();
        }

        public async Task<IEnumerable<Responses>> GetAllResponses()
        {
            await Initialize();
            await SyncUser();
            var data = await responsesTable
                .OrderBy(c => c.questionid)
                .ToEnumerableAsync(); //this op is on the local database

            return data;
        }


        public async Task<IEnumerable<Responses>> GetResponses(Responses responses)
        {
            await Initialize();
            await SyncUser();
            var data = await responsesTable
                //.Where(user=>user.username)
                .Where(c =>c.myCourse==responses.myCourse && c.myClass==responses.myClass && c.myLectureNo==responses.myLectureNo)
                //  .Select(c => c.firstName).ToListAsync();
                // .OrderBy(c => c.firstName)
                .ToEnumerableAsync(); //this op is on the local database

            return data;
        }


      /*  public async Task<IEnumerable<Responses>> GetIndexCount(int ind)
        {
            await Initialize();
            await SyncUser();
            var data = await responsesTable
                .Where(c => c.answerIndex==ind.ToString())
                //  .Select(c => c.firstName).ToListAsync();
                // .OrderBy(c => c.firstName)
                .ToEnumerableAsync(); //this op is on the local database
            
            return data;
        }*/

        public async Task<bool> AddResponses(Responses responses)
        {
            await Initialize();
            try
            {
                await responsesTable.InsertAsync(responses);//locally insert
                await SyncUser();
                return true;
            }
            catch
            {
                return false;
            }



        }



        
    }
}
