using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

    namespace Alborz.DomainLayer.Entities
{
    [Table("AddressTbl")]
    public partial class AddressTbl : BaseEntity<int>
    { 
        [StringLength(150)]
        public string AddressType { get; set; }
        public int? Province { get; set; }
        public int? City { get; set; }
        [StringLength(20)]
        public string PostalCode { get; set; }
        [StringLength(150)]
        public string FirstNameReciver { get; set; }
        [StringLength(150)]
        public string LastNameReciver { get; set; }
        [StringLength(15)]
        public string Phone { get; set; }
        [StringLength(250)]
        public string Email { get; set; }
        [StringLength(350)]
        public string Address { get; set; }
        public int? CustomerId { get; set; }
    }
}
