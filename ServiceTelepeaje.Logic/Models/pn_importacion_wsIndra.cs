namespace ServiceTelepeaje.Logic.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pn_importacion_wsIndra
    {
        [Key]
        public long id_exp { get; set; }

        public DateTime? DATE_TRANSACTION { get; set; }

        [StringLength(10)]
        public string VOIE { get; set; }

        public long? EVENT_NUMBER { get; set; }

        public long? FOLIO_ECT { get; set; }

        public int? Version_Tarif { get; set; }

        public int? ID_PAIEMENT { get; set; }

        public int? TAB_ID_CLASSE { get; set; }

        [StringLength(10)]
        public string CLASE_MARCADA { get; set; }

        public decimal? MONTO_MARCADO { get; set; }

        public int? ACD_CLASS { get; set; }

        [StringLength(10)]
        public string CLASE_DETECTADA { get; set; }

        public decimal? MONTO_DETECTADO { get; set; }

        [StringLength(50)]
        public string CONTENU_ISO { get; set; }

        public int? CODE_GRILLE_TARIF { get; set; }

        [StringLength(10)]
        public string ID_OBS_MP { get; set; }

        public DateTime? fecha_ext { get; set; }

        public int? Shift_number { get; set; }

        public int? PLAZA { get; set; }
    }
}
