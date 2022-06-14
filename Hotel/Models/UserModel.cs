using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Hotel.Models
{
    public class UserModel
    {
        //Fields
        private int id;
        private string name;
        private string password;
        private string role;

        //Properties - Validations
        [DisplayName("User ID")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DisplayName("User Name")]
        [Required(ErrorMessage = "Name is requerid")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "User's name must be between 3 and 50 characters")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DisplayName("User Password")]
        [Required(ErrorMessage = "Password is requerid")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The password must be between 3 and 50 characters")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        [DisplayName("User Role")]
        [Required(ErrorMessage = "Role is requerid")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The role must be between 3 and 50 characters")]
        public string Role
        {
            get { return role; }
            set { role = value; }
        }
    }
}