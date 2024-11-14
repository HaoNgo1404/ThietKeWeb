using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ThietKeWeb.Models.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Hãy nhập Username.")]
        [Display(Name = "Tên dăng nhập")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Hãy nhập Password.")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}