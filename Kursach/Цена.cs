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
    
    public partial class Цена
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Цена()
        {
            this.Товары_куплены = new HashSet<Товары_куплены>();
        }
    
        public System.Guid Id_цена { get; set; }
        public long Цена1 { get; set; }
        public System.DateTime Дата_установки { get; set; }
        public Nullable<System.DateTime> Дата_окончания { get; set; }
        public System.Guid Id_Товара { get; set; }
    
        public virtual Товары Товары { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Товары_куплены> Товары_куплены { get; set; }
    }
}
