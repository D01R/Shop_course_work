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
    
    public partial class Дисконтная_карта
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Дисконтная_карта()
        {
            this.Чек_Покупки = new HashSet<Чек_Покупки>();
        }
    
        public System.Guid Номер_Дисконтной_карты { get; set; }
        public short Величина_скидки { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Чек_Покупки> Чек_Покупки { get; set; }
    }
}
