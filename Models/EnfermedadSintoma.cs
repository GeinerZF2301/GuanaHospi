﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GuanaHospi.Models
{
    [Table("EnfermedadSintoma")]
    public partial class EnfermedadSintoma
    {
        [Key]
        [Column("Id_EnfermedadSintoma")]
        public int IdEnfermedadSintoma { get; set; }
        [Column("Id_Sintoma")]
        public int? IdSintoma { get; set; }
        [Column("Id_Enfermedad")]
        public int? IdEnfermedad { get; set; }

        [ForeignKey(nameof(IdEnfermedad))]
        [InverseProperty(nameof(Enfermedad.EnfermedadSintomas))]
        public virtual Enfermedad IdEnfermedadNavigation { get; set; }
        [ForeignKey(nameof(IdSintoma))]
        [InverseProperty(nameof(Sintoma.EnfermedadSintomas))]
        public virtual Sintoma IdSintomaNavigation { get; set; }
    }
}
