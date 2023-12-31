﻿using BuyLogTracker.Api.Helpers;
using System.ComponentModel.DataAnnotations;

namespace BuyLogTracker.Api.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        private string _phone = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string Phone {
            get { return PhoneNumber.FormatPhoneNumberForDisplay(_phone); }
            set { _phone = PhoneNumber.FormatPhoneNumberForStorage(value); }
        }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string? Email { get; set; }

        public List<PurchaseHistory>? PurchaseHistory { get; set; }
    }
}
