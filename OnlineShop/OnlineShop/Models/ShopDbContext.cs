using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlineShop.Models
{

    public class ShopDbContext : IdentityDbContext<ApplicationUser>
    {
        public ShopDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public virtual IDbSet<Car> Cars { get; set; }

        

        public static ShopDbContext Create()
        {
            return new ShopDbContext();
        }

       
    }
}