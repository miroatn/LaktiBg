using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LaktiBg.Infrastructure.Constants.DataConstants.UserConstants;
using static LaktiBg.Core.Constants.MessageConstants;

namespace LaktiBg.Core.Models.UserModels
{
    public class UserEditModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(FirstNameMaxLenght, MinimumLength = FirstNameMinLenght, ErrorMessage = LenghtMessage)]
        [Display(Name = "Име")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(LastNameMaxLenght, MinimumLength = LastNameMinLenght, ErrorMessage = LenghtMessage)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Дата на раждане")]
        public DateTime BirthDate { get; set; }

        [StringLength(AddressMaxLenght, MinimumLength = AddressMinLenght, ErrorMessage = LenghtMessage)]
        [Display(Name = "Адрес")]
        public string? Address { get; set; } = string.Empty;

        [StringLength(DescriptionMaxLenght, MinimumLength = DescriptionMinLenght, ErrorMessage = LenghtMessage)]
        [Display(Name = "Описание")]
        public string? Description { get; set; } = string.Empty;

        [StringLength(PhoneMaxLenght, MinimumLength = PhoneMinLenght, ErrorMessage = LenghtMessage)]
        [Display(Name = "Телефон")]
        public string? PhoneNumber { get; set; } = string.Empty;

    }
}
