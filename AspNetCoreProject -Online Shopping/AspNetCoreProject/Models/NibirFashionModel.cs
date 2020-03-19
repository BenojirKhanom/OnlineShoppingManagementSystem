using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreProject.Models
{
    public class NibirFashionModel
    {
    }
    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        [StringLength(50)]
        public string CategoryName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.CustomerReviews = new HashSet<CustomerReview>();
            this.Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        [Required]
        [Display(Name = "CustomerName")]
        [StringLength(50)]
        public string CustomerName { get; set; }
        [Required]
        [Display(Name = "Address")]
        [StringLength(50)]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Mobile")]
        [DataType(DataType.PhoneNumber)]
        public string Number { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerReview> CustomerReviews { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }

    public partial class CustomerReview
    { 
        [Key]
        public int ReviewId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        [Required]
        [Display(Name = "Comment")]
        [StringLength(50)]
        public string Opinion { get; set; }
        public string ImageFile { get; set; }

        public virtual Customer Customer { get; set; }
    }

    public partial class DeliveryBoy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DeliveryBoy()
        {
            this.ShippingInfoes = new HashSet<ShippingInfo>();
        }

        public int DeliveryBoyId { get; set; }
        [Required]
        [Display(Name = "Delivery BoyName")]
        [StringLength(50)]
        public string DeliveryBoyName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShippingInfo> ShippingInfoes { get; set; }
    }

    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.OrderDtls = new HashSet<OrderDtl>();
        }
        [Key]
        public int EmpID { get; set; }
        [Required]
        [Display(Name = "Emplyee Name")]
        [StringLength(50)]
        public string Name { get; set; }
        public Nullable<int> Age { get; set; }
        [Required]
        [Display(Name = "City")]
        [StringLength(50)]
        public string Country { get; set; }
        public Nullable<decimal> Salary { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDtl> OrderDtls { get; set; }
    }

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.OrderDtls = new HashSet<OrderDtl>();
        }

        public int OrderId { get; set; }
        [Required]
        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        public string Orderdate { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> ShippingId { get; set; }

        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDtl> OrderDtls { get; set; }
        public virtual ShippingInfo ShippingInfo { get; set; }
    }

    public partial class OrderDtl
    {
        public int OrderDtlId { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> EmpID { get; set; }
        [Required]
        [Display(Name = "Quentity")]
        [DataType(DataType.PhoneNumber)]
        public string Quentity { get; set; }
        [Required]
        [Display(Name = "TotalCost")]
        [DataType(DataType.PhoneNumber)]
        public string TotalCost { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.OrderDtls = new HashSet<OrderDtl>();
        }

        public int ProductId { get; set; }
        [Required]
        [Display(Name = "ProductName")]
        [StringLength(50)]
        public string ProductName { get; set; }
        public Nullable<int> CategoryId { get; set; }
        [Required]
        [Display(Name = "Price")]
        [DataType(DataType.PhoneNumber)]
        public string price { get; set; }

        [Display(Name = "Product details")]
        public string ImageFile { get; set; }

        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDtl> OrderDtls { get; set; }
    }

    public partial class ShippingInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShippingInfo()
        {
            this.Orders = new HashSet<Order>();
        }
        [Key]
        public int ShippingId { get; set; }
        public Nullable<int> DeliveryBoyId { get; set; }
        [Required]
        [Display(Name = "Shipping Cost")]
        [DataType(DataType.PhoneNumber)]
        public string ShippingCost { get; set; }
        [Required]
        [Display(Name = "Shipping Date")]
        [DataType(DataType.Date)]
        public string ShippingDate { get; set; }

        public virtual DeliveryBoy DeliveryBoy { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }

}
