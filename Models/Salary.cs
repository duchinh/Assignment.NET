namespace EFCoreWebAPI.Models;

using System;
using System.ComponentModel.DataAnnotations;

public class Salary
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public required Employee Employee { get; set; }
    public decimal Amount { get; set; }  

}