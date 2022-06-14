using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel.Views
{
    public partial class MainView : Form, IMainView
    {
        public MainView()
        {
            InitializeComponent();
            btnHotels.Click += delegate { ShowHotelView?.Invoke(this, EventArgs.Empty); };
        }

        public event EventHandler ShowHotelView;
    }
}