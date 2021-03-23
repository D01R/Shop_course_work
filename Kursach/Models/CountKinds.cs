using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kursach
{
    public class CountKinds
    {
        public IEnumerable<Вид_оплаты> kind { get; set; }
        public IEnumerable<Чек_Покупки> chek { get; set; }
        public CountKinds(IEnumerable<Чек_Покупки> chek)
        {
            this.chek = chek;
        }
    }
}