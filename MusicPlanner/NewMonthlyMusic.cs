//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MusicPlanner
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class NewMonthlyMusic
    {
        public int MusicID { get; set; }
        public System.DateTime Date { get; set; }

        [Required]
        [DisplayName("Church Day")]
        public string churchDay { get; set; }
        [Required]
        public string Opening { get; set; }
        [Required]
        [DisplayName("Open Num")]
        public string OpeningNumber { get; set; }
        [Required]
        public string Psalm { get; set; }
        [Required]
        [DisplayName("Psalm Num")]
        public string PsalmNumber { get; set; }
        [Required]
        public string Preparation { get; set; }
        [Required]
        [DisplayName("Prep Num")]
        public string PreparationNumber { get; set; }
        [Required]
        public string Communion { get; set; }
        [Required]
        [DisplayName("Comm. Num")]
        public string CommunionNumber { get; set; }
        [Required]
        public string Closing { get; set; }
        [Required]
        [DisplayName("Close Num")]
        public string ClosingNumber { get; set; }
        public string Notes { get; set; }
    }
}
