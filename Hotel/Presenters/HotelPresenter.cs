using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hotel.Models;
using Hotel.Views;

namespace Hotel.Presenters
{
    public class HotelPresenter
    {
        //Fields
        private IHotelView view;
        private IHotelRepository repository;
        private BindingSource hotelsBindingSource;
        private IEnumerable<HotelModel> hotelList;

        //Constructor
        public HotelPresenter(IHotelView view, IHotelRepository repository)
        {
            this.hotelsBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;
            //Subscribe event handler methods to view events
            this.view.SearchEvent += SearchHotel;
            this.view.AddNewEvent += AddNewHotel;
            this.view.EditEvent += LoadSelectedHotelToEdit;
            this.view.DeleteEvent += DeleteSelectedHotel;
            this.view.SaveEvent += SaveHotel;
            this.view.CancelEvent += CancelAction;
            //Set hotels bindind source
            this.view.SetHotelListBindingSource(hotelsBindingSource);
            //Load hotel list view
            LoadAllHotelList();
            //Show view
            this.view.Show();
        }

        //Methods
        private void LoadAllHotelList()
        {
            hotelList = repository.GetAll();
            hotelsBindingSource.DataSource = hotelList;//Set data source.
        }
        private void SearchHotel(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                hotelList = repository.GetByValue(this.view.SearchValue);
            else hotelList = repository.GetAll();
            hotelsBindingSource.DataSource = hotelList;
        }
        private void AddNewHotel(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }
        private void LoadSelectedHotelToEdit(object sender, EventArgs e)
        {
            var hotel = (HotelModel)hotelsBindingSource.Current;
            view.HotelId = hotel.Id.ToString();
            view.HotelName = hotel.Name;
            view.IsEdit = true;
        }
        private void SaveHotel(object sender, EventArgs e)
        {
            var model = new HotelModel();
            model.Id = Convert.ToInt32(view.HotelId);
            model.Name = view.HotelName;
            try
            {
                new Common.ModelDataValidation().Validate(model);
                if (view.IsEdit)//Edit model
                {
                    repository.Edit(model);
                    view.Message = "Hotel edited successfuly";
                }
                else //Add new model
                {
                    repository.Add(model);
                    view.Message = "Hotel added sucessfully";
                }
                view.IsSuccessful = true;
                LoadAllHotelList();
                CleanviewFields();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }
        }

        private void CleanviewFields()
        {
            view.HotelId = "0";
            view.HotelName = "";
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanviewFields();
        }
        private void DeleteSelectedHotel(object sender, EventArgs e)
        {
            try
            {
                var hotel = (HotelModel)hotelsBindingSource.Current;
                repository.Delete(hotel.Id);
                view.IsSuccessful = true;
                view.Message = "Hotel deleted successfully";
                LoadAllHotelList();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "An error ocurred, could not delete hotel";
            }
        }

    }
}