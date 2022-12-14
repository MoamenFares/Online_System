// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Online_System.Models
{
    public partial class Order
    {
        public Order()
        {
            Products_Orders = new HashSet<Products_Order>();
        }

        [Key]
        public int Id { get; set; }
        public double Total_Price { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Shipping_Address { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Payment_Method { get; set; }
        public int Customer_Id { get; set; }

        [ForeignKey("Customer_Id")]
        [InverseProperty("Orders")]
        public virtual User Customer { get; set; }
        [InverseProperty("Order")]
        public virtual ICollection<Products_Order> Products_Orders { get; set; }
    }
}