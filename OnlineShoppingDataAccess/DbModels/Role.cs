namespace OnlineShoppingDataAccess
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Role
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Column("Role")]
        [Required]
        [StringLength(50)]
        public string Role1 { get; set; }

        public virtual User User { get; set; }
    }
}
