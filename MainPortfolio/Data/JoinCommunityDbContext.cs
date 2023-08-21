using MainPortfolio.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MainPortfolio.Data
{
    public class JoinCommunityDbContext : IdentityDbContext
    {
        public JoinCommunityDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<JoinCommunityModel> Member { get; set; }
    }
}
