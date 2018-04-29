using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System;

namespace pts2.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class client_info
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long id { get; set; }


        [Required(ErrorMessage = "Please Enter your name")]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use alphabets only ")]
        public string firstname { get; set; }

        [Display(Name = "Last Name")]

        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use alphabets only ")]
        public string lastname { get; set; }

        [Required(ErrorMessage = "Please Enter your company name")]
        [Display(Name = "Company")]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Use alphabets and digits only")]
        public string companyname { get; set; }

        [Required(ErrorMessage = "Please select industry")]
        [Display(Name = "Industry")]
        public int industry { get; set; }


        [Required(ErrorMessage = "Please select country")]
        [Display(Name = "Country")]
       public int country { get; set; }

        [Required(ErrorMessage = "Please Enter Address")]
        [Display(Name = "Address 1")]
        [StringLength(200)]
        public string address1 { get; set; }

        [Display(Name = "Address 2")]
        [StringLength(200)]
        public string address2 { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile No")]
        [Display(Name = "Mobile No.")]
        // [Remote("isnoexists", "client_info", ErrorMessage = "user already exists")]
        //  [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        [RegularExpression(@"^([0]|\+91[\-\s]?)?[789]\d{9}$", ErrorMessage = "Entered Mobile No is not valid.")]
        [StringLength(10, ErrorMessage = "The Mobile must contains 10 characters", MinimumLength = 10)]
        public string contact1 { get; set; }

        [Display(Name = "Phone No.")]

        // [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        [RegularExpression(@"^([0]|\+91[\-\s]?)?[789]\d{9}$", ErrorMessage = "Entered Mobile No is not valid.")]
        [StringLength(10, ErrorMessage = "The Mobile must contains 10 characters", MinimumLength = 10)]
        public string contact2 { get; set; }


        [Required(ErrorMessage = "Please Enter Email Address")]
        [Display(Name = "Email")]
  // [Remote("isuserexists","client_info",ErrorMessage="Email already exists")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string email_id { get; set; }
        [Display(Name = "Vat no.")]

        public string vat_no { get; set; }

        public ICollection<enquiry_info> enquiry { get; set; }

    }

    public enum Source
    {
         [Display(Name = "select source")]
        selectsource,
        Email,
        [Display(Name = "Referred by")]
        Referredby,
        Others
    }

    public enum Status
    {
         [Display(Name = "select status")]
        Selectstatus,
        Proposal,
        Enquiry,
        Onhold,
        Accepted,
        Rejected,
    }

    public class enquiry_info
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("client_info")]
        [Column(Order = 2)]
        public long cid { get; set; }

        public client_info client_info { get; set; }

        [Display(Name = "Project Name")]
        [Required]
        public string projectname { get; set; }


        [Display(Name = "Source")]
        [Required]
        public Source source { get; set; }

        public string name { get; set; }

        [Display(Name = "Remark")]

        public string remark1 { get; set; }

        

         [Display(Name = "Status")]
        public Status status { get; set; }

         [Display(Name = "Tag")]
        public string tag { get; set; }


        public ICollection<attachments_info> attachments { get; set; }



    }


    public class attachments_info
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }


        [ForeignKey("enquiry")]
        [Column(Order = 2)]
        public long cid { get; set; }

        public enquiry_info enquiry { get; set; }



        public string fileName { get; set; }

 }

    public class country
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string countryname { get; set; }
    }

    public class industry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string industryname { get; set; }

    }

    public class proposal_info
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("enquiry")]
        [Column(Order = 2)]
        public long? eid { get; set; }

        public enquiry_info enquiry { get; set; }

        [Display(Name = "Enquiry")]

        public string prop_enquiry { get; set; }


      [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime sentdate { get; set; }

         [Required]
        public string sent_to { get; set; }

       

        public string sent_via { get; set; }



        

        public ICollection<pros_attachments_info> pros_attachments { get; set; }



    }


    public class pros_attachments_info
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }


        [ForeignKey("proposal")]
        [Column(Order = 2)]
        public long pid { get; set; }

        public proposal_info proposal { get; set; }



        public string fileName { get; set; }

    }

    public class invoice_main
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("proposal")]
        [Column(Order = 2)]
        public long? pid { get; set; }

        public proposal_info proposal { get; set; }

        public string proposal_name { get; set; }

        public List<invoice_item> invoice_item { get; set; }
    }

    public class invoice_item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("invoice_main")]
        [Column(Order = 2)]
        public long imid { get; set; }

        public invoice_main invoice_main { get; set; }
        [Required]
        public string item { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public float rate { get; set; }
        public float total { get; set; }
    }
   

}
