using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Chapter01.Original
{
    class Tests
    {
        [Test]
        public void BillPrinter_prints()
        {
            var plays = LoadPlays();
            var invoices = LoadInvoices();
            var subject = new BillPrinter();
            var output = subject.PrintStatement(invoices[0], plays);
            Assert.IsTrue(output.Length > 0);
        }
        private Plays LoadPlays()
        {
            var json = File.ReadAllText("plays.json");
            return JsonConvert.DeserializeObject<Plays>(json);
        }

        private Invoice[] LoadInvoices()
        {
            var json = File.ReadAllText("invoices.json");
            return JsonConvert.DeserializeObject<Invoice[]>(json);
        }
    }


}
