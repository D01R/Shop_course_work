//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kursach
{
    using System;
    using System.Collections.Generic;
    
    public partial class Товары_куплены
    {
        public int Количество { get; set; }
        public System.Guid Id_Чека { get; set; }
        public System.Guid Id_цена { get; set; }
        public System.Guid Id_Товара { get; set; }
    
        public virtual Цена Цена { get; set; }
        public virtual Чек_Покупки Чек_Покупки { get; set; }
    }
}
