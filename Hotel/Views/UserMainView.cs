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
    public partial class UserMainView : Form, IUserMainView
    {
        public UserMainView()
        {
            InitializeComponent();
            btnUsers.Click += delegate { ShowUserView?.Invoke(this, EventArgs.Empty); };
        }

        public event EventHandler ShowUserView;
       
    }
}