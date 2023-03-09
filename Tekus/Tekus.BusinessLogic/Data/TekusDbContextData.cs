using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tekus.Core.Entities;

namespace Tekus.BusinessLogic.Data
{
    public class TekusDbContextData
    {

    public static async Task LoadDataAsync(
    TekusDbContext context,
    ILoggerFactory loggerFactory)
        {

            try
            {
                if (!context.Supplier.Any())
                {
                    string supplierData = 
               File.ReadAllText("../Tekus.BusinessLogic/LoadData/supplier.json");
               List<Supplier> suppliers = JsonSerializer.Deserialize<List<Supplier>>(supplierData);

                    foreach (var supplier in suppliers)
                    {
                        context.Supplier.Add(supplier);
                    }
                   await context.SaveChangesAsync();

                }


                if (!context.Country.Any())
                {
                    string countryData =
               File.ReadAllText("../Tekus.BusinessLogic/LoadData/country.json");
                    List<Country> countries = 
                        JsonSerializer.Deserialize<List<Country>>(countryData);

                    foreach (var country in countries)
                    {
                        context.Country.Add(country);
                    }
                    await context.SaveChangesAsync();

                }


                if (!context.Services.Any())
                {
                    string countryData =
               File.ReadAllText("../Tekus.BusinessLogic/LoadData/services.json");
                    List<Services> services =
                        JsonSerializer.Deserialize<List<Services>>(countryData);

                    foreach (var service in services)
                    {
                        context.Services.Add(service);
                    }
                    await context.SaveChangesAsync();

                }
                 

            }
            catch (Exception e)
            {
                ILogger<TekusDbContextData> logger = loggerFactory.CreateLogger<TekusDbContextData>();
                logger.LogError(e.Message);
           
            
            }




        }


    }
}
