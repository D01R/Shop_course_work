using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kursach.Models
{
    public partial class LogIn
    {
        public string login { get; set; }
        public string password { get; set; }
    }
    public partial class Registration
    {
        public string login { get; set; }
        public string password { get; set; }
        public string Имя { get; set; }
        public string Фамилия { get; set; }
    }
}
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace Kursach
//{
//    public partial class LogIn
//    {
//        public string login { get; set; }
//        public string password { get; set; }
//    }
//    public partial class Registration
//    {
//        public string login { get; set; }
//        public string password { get; set; }
//        public string Имя { get; set; }
//        public string Фамилия { get; set; }
//    }
//}