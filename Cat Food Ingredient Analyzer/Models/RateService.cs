using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace Cat_Food_Ingredient_Analyzer.Models
{
    public class RateService
    {
        mySampleDatabaseEntities db = new mySampleDatabaseEntities();

        public Rate GetRate(string Quote)
        {
            Rate rate = db.Rates.Find(Quote);

            if (rate != null)
            {
                if (rate.ExpiryDateTime > DateTime.Now)
                {
                    return rate;
                }
            }

            string path = "http://www.apilayer.net/api/live?access_key=02b3a6b8d7b281ff7234e9176d812b6f";
            //&source=" + Quote[0] + Quote[1] + Quote[2];
            HttpResponseMessage response = new HttpClient().GetAsync(path).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            dynamic jo = JObject.Parse(responseBody);
            var quotes = jo.quotes;

            foreach (var quote in quotes)
            {
                Rate tRate = db.Rates.Find((string)quote.Name);

                //if exist
                if (tRate != null)
                {
                    tRate.ExchangeRate = (decimal)quote.Value;
                    tRate.ExpiryDateTime = DateTime.Now.AddHours(1);
                    db.SaveChanges();
                }
                //if not exist
                else
                {
                    db.Rates.Add(new Rate
                    {
                        ExchangeRate = (decimal)quote.Value,
                        ExpiryDateTime = DateTime.Now.AddHours(1),
                        Quote = (string)quote.Name
                    });

                    db.SaveChanges();
                }
            }

            return db.Rates.Find(Quote);
        }
    }
}