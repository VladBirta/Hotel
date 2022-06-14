using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Views;
using Hotel.Models;
using Hotel._Repositories;
using System.Windows.Forms;

namespace Hotel.Presenters
{
    public class UserMainPresenter
    {
        private IUserMainView mainView;
        private readonly string sqlConnectionString;

        public UserMainPresenter(IUserMainView mainView, string sqlConnectionString)
        {
            this.mainView = mainView;
            this.sqlConnectionString = sqlConnectionString;
            this.mainView.ShowUserView += ShowUsersView;
        }

        private void ShowUsersView(object sender, EventArgs e)
        {
            IUserView view = UserView.GetInstace((UserMainView)mainView);
            IUserRepository repository = new UserRepository(sqlConnectionString);
            new UserPresenter(view, repository);
        }
    }
}