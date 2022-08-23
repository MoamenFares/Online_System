﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Online_System.Models
{
    [Table("Cart")]
    public partial class Cart
    {
        [Key]
        public int User_Id { get; set; }
        [Key]
        public int Product_Id { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("User_Id")]
        [InverseProperty("Carts")]
        public virtual User User { get; set; }
    }
}