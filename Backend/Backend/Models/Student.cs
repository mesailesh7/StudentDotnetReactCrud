using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Student
{
    //once you put id it will automatically choosen as primary key 
    // public int Id { get; set; }
    // [Required]
    // [MaxLength(30)]
    // public string FirstName { get; set; } = string.Empty;
    // [Required]
    // [MaxLength(30)]
    // public string LastName { get; set; } = string.Empty;

    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string name { get; set; } = string.Empty;

    public int? age { get; set; }

    public string? gender { get; set; } = string.Empty;

    public DateOnly? enrolled_at { get; set; }
}
