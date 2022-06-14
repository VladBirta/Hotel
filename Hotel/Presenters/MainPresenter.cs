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
    public class MainPresenter
    {
        private IMainView mainView;
        private readonly string sqlConnectionString;

        public MainPresenter(IMainView mainView, string sqlConnectionString)
        {
            this.mainView = mainView;
            this.sqlConnectionString = sqlConnectionString;
            this.mainView.ShowHotelView += ShowHotelsView;
        }

        private void ShowHotelsView(object sender, EventArgs e)
        {
            IHotelView view = HotelView.GetInstace((MainView)mainView);
            IHotelRepository repository = new HotelRepository(sqlConnectionString);
            new HotelPresenter(view, repository);
        }
    }
}