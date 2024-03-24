using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;
//using System.Runtime.InteropServices;

namespace TestApplication.Model
{
    public class CarDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Make { get; set; }        
        public string? Model { get; set; } 
        public int Year { get; set; }     
        public string? Trim { get; set; }
        public string? Engine { get; set; }

        public bool? Status { get; set; }   



    }
    public class CarDetailReq
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public string? Trim { get; set; }
        public string? Engine { get; set; }

        public bool? Status { get; set; }



    }
}
