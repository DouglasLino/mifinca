//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mifinca.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class empleado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public empleado()
        {
            this.planilla = new HashSet<planilla>();
            this.tarea = new HashSet<tarea>();
        }
    
        public int id_empleado { get; set; }
        public int id_finca { get; set; }
        public string nombre_empleado { get; set; }
        public string apellido_empleado { get; set; }
        public string direccion_empleado { get; set; }
        public int telefono_empleado { get; set; }
        public int telefonoEmergencia_empleado { get; set; }
        public string qr_empleado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<planilla> planilla { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tarea> tarea { get; set; }
        public virtual finca finca { get; set; }
    }
}
