using System;
using System.ComponentModel.DataAnnotations;

namespace Bai2.Models
{
    public enum Gender
    {
        [Display(Name = "Nam")]
        Male,
        [Display(Name = "Nữ")]
        Female,
        [Display(Name = "Khác")]
        Other
    }

    public class Person
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Họ không được để trống")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Họ phải từ 2-50 ký tự")]
        [Display(Name = "Họ")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Tên phải từ 2-50 ký tự")]
        [Display(Name = "Tên")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Giới tính không được để trống")]
        [Display(Name = "Giới tính")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Số điện thoại phải có 10 chữ số")]
        [Display(Name = "Số điện thoại")]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Nơi sinh không được để trống")]
        [StringLength(100, ErrorMessage = "Nơi sinh không được quá 100 ký tự")]
        [Display(Name = "Nơi sinh")]
        public required string BirthPlace { get; set; }

        [Display(Name = "Đã tốt nghiệp")]
        public bool IsGraduated { get; set; }
    }
}
