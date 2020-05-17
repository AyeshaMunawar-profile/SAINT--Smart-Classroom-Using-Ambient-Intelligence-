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
    public class AzureServiceQuiz
    {

        public MobileServiceClient Client { get; private set; }
        private IMobileServiceSyncTable<Quiz> quizTable;

        private async Task Initialize()
        {
            if (Client?.SyncContext?.IsInitialized ?? false)
                return;

            var appUrl = "https://saintclicker.azurewebsites.net/";

            Client = new MobileServiceClient(appUrl);

            // var path = Path.Combine(MobileServiceClient.DefaultDatabasePath, "usersync.db");
            var fileName = "SAINTQuizdb.db";
            var store = new MobileServiceSQLiteStore(fileName);

            store.DefineTable<Quiz>();

            await Client.SyncContext.InitializeAsync(store);

            quizTable = Client.GetSyncTable<Quiz>();//client handles sync tables automatically (offline)
        }

        private async Task SyncQuiz()
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

                await quizTable.PullAsync("allQuiz", quizTable.CreateQuery());

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }




            // await userTable.PullAsync("allUser", userTable.CreateQuery());
            //await Client.SyncContext.PushAsync();
        }

        public async Task<IEnumerable<Quiz>> GetAllQuiz()
        {
            await Initialize();
            await SyncQuiz();
            var data = await quizTable.ReadAsync();
              //  .OrderBy(c => c.myClass)
               // .ToEnumerableAsync(); //this op is on the local database

            return data;
        }


        public async Task<IEnumerable<Quiz>> GetQuiz(Quiz quiz)
        {
            
        await Initialize();
        await SyncQuiz();
            var data = await quizTable
                //.Where(user=>user.username)
                .Where(c => c.myClass==quiz.myClass && c.myCourse==quiz.myCourse && c.myLectureNo==quiz.myLectureNo)
                //  .Select(c => c.firstName).ToListAsync();
                // .OrderBy(c => c.firstName)
                .ToEnumerableAsync(); //this op is on the local database

            return data;

        }

        


        public async Task<bool> AddQuiz(Quiz quiz)
        {
            await Initialize();
            try
            {
                await quizTable.InsertAsync(quiz);//locally insert
                await SyncQuiz();
                return true;
            }
            catch
            {
                return false;
            }



        }



    }
}
