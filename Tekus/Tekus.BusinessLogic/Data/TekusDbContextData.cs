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

                if (!context.Country.Any())
                {
                    string countryData = File.ReadAllText("../Tekus.BusinessLogic/Data/LoadData/country.json");
                    List<Country> countries =
                    JsonSerializer.Deserialize<List<Country>>(countryData);

                    foreach (Country country in countries)
                    {
                        context.Country.Add(country);
                    }
                    await context.SaveChangesAsync();

                }

                if (!context.Services.Any())
                {
                    string countryData = File.ReadAllText("../Tekus.BusinessLogic/Data/LoadData/services.json");
                    List<Services> services =
                    JsonSerializer.Deserialize<List<Services>>(countryData);

                    foreach (Services service in services)
                    {
                        context.Services.Add(service);
                    }
                    await context.SaveChangesAsync();

                }

                if (!context.Supplier.Any())
                {
      
                    string supplierData = 
                    File.ReadAllText("../Tekus.BusinessLogic/Data/LoadData/supplier.json");
                    List<Supplier> suppliers = JsonSerializer.Deserialize<List<Supplier>>(supplierData);

                    foreach (Supplier supplier in suppliers)
                    {
                        context.Supplier.Add(supplier);
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
