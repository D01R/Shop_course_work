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
    
    public partial class ProductsWithPrice
    {
        public System.Guid Id_Товара { get; set; }
        public System.Guid Id_Категории { get; set; }
        public System.Guid Id_цена { get; set; }
        public string Товар { get; set; }
        public string Категория { get; set; }
        public long Цена { get; set; }
        public System.DateTime Дата_установки { get; set; }
        public Nullable<System.DateTime> Дата_окончания { get; set; }
    }
}
