using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Models;

[Table("products")]
[Index("Sku", Name = "UQ__products__DDDF4BE772513298", IsUnique = true)]
public class Product
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(255)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("description")]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("category")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Category { get; set; }

    [Column("price", TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    [Column("quantity")]
    public int? Quantity { get; set; }

    [Column("sku")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Sku { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("is_active")]
    public bool? IsActive { get; set; }
}
