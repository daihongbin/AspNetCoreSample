using ConfigurationSample.Models;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationSample.EFConfigurationProvider
{
    public class EFConfigurationContext:DbContext
    {
        public EFConfigurationContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<EFConfigurationValue> Values { get; set; }
    }
}