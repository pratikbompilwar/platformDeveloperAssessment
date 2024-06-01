using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace assessment_platform_developer.Models
{
	[Serializable]
	public class Customer
	{
		public int ID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City cannot be longer than 50 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        [StringLength(50, ErrorMessage = "State cannot be longer than 50 characters.")]
        public string State { get; set; }

		public string Zip { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [StringLength(50, ErrorMessage = "Country cannot be longer than 50 characters.")]
        public string Country { get; set; }

        [StringLength(200, ErrorMessage = "Notes cannot be longer than 200 characters.")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "Contact Name is required.")]
        [StringLength(50, ErrorMessage = "Contact Name cannot be longer than 50 characters.")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Contact Phone is required")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid contact phone")]
        public string ContactPhone { get; set; }

        [Required(ErrorMessage = "Contact Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid contact email address.")]
        public string ContactEmail { get; set; }

        [Required(ErrorMessage = "Contact Title is required.")]
        [StringLength(100, ErrorMessage = "Contact Title cannot be longer than 100 characters.")]
        public string ContactTitle { get; set; }

      
        [StringLength(300, ErrorMessage = "Contact Notes cannot be longer than 300 characters.")]
        public string ContactNotes { get; set; }

	}


}