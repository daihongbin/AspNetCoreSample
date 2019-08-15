using System;
using System.Collections.Generic;
using System.Linq;
using ConfigurationSample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ConfigurationSample.EFConfigurationProvider
{
    public class EFConfigurationProvider:ConfigurationProvider
    {
        public EFConfigurationProvider(Action<DbContextOptionsBuilder> optionsAction)
        {
            OptionsAction = optionsAction;
        }

        Action<DbContextOptionsBuilder> OptionsAction { get; }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<EFConfigurationContext>();

            OptionsAction(builder);

            using (var dbContext = new EFConfigurationContext(builder.Options))
            {
                dbContext.Database.EnsureCreated();

                Data = !dbContext.Values.Any() ? CreateAndSaveDefaultValues(dbContext) : dbContext.Values.ToDictionary(c => c.Id, c => c.Value);
            }
        }

        #region private_method

        private static IDictionary<string, string> CreateAndSaveDefaultValues(EFConfigurationContext dbContext)
        {
            var configValues = new Dictionary<string, string>
            {
                {"quote1","I aim to misbehave." },
                {"quote2","I swallowed a bug." },
                {"quote3","You can't stop the signal, Mal." }
            };
            
            dbContext.Values.AddRange(configValues.Select(s => new EFConfigurationValue
            {
                Id = s.Key,
                Value = s.Value
            }).ToArray());

            dbContext.SaveChanges();

            return configValues;
        }
        #endregion
    }
}