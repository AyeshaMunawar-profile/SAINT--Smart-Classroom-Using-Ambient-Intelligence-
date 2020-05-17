using MvvmHelpers;
using SAINTClicker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SAINTClicker.ViewModel
{
   public class UserViewModel: BaseViewModel //implement inotifychanged etc
    {
        public UserViewModel()
        { }
        //observable collection reloads all data after update, rangecollection gives only one update notification
        ObservableRangeCollection<User> Users { get; } = new ObservableRangeCollection<User>();






            ICommand loadUserCommand;
        public ICommand LoadUserCommand =>
            loadUserCommand ?? (loadUserCommand = new Command(async () => await ExecuteLoadUsersCommandAsync()));

        async Task ExecuteLoadUsersCommandAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                //get all coffee
            }
            catch(Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }

        ICommand addUserCommand;
        public ICommand AddUserCommand =>
            addUserCommand ?? (addUserCommand = new Command(async () => await ExecuteAddUsersCommandAsync()));
async Task ExecuteAddUsersCommandAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                //add all coffee
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }
    }

       
    
}
