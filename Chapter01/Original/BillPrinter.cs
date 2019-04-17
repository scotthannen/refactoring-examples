using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Chapter01.Original
{
    class BillPrinter
    {
        internal string PrintStatement(Invoice invoice, Plays plays)
        {
            decimal totalAmount = 0;
            decimal volumeCredits = 0;
            var result = new StringBuilder();
            result.AppendLine($"Statement for {invoice.Customer}");
            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                decimal thisAmount = 0;
                switch (play.Type)
                {
                    case "tragedy":
                    {
                        thisAmount=40000;
                        if (perf.Audience > 30)
                        {
                            thisAmount += 1000 * (perf.Audience - 30);
                        }
                        break;
                    }
                    case "comedy":
                    {
                        thisAmount = 30000;
                        if (perf.Audience > 20)
                        {
                            thisAmount += 10000 + 500 * (perf.Audience - 20);
                        }

                        thisAmount += 300 * perf.Audience;
                            break;
                    }
                    default:
                    {
                        throw new Exception($"Unknown type: {play.Type}");
                    }
                }

                // add volume credits
                volumeCredits += Math.Max(perf.Audience - 30, 0);
                // add extra credit for every ten comedy attendees
                if ("comedy" == play.Type) volumeCredits += Math.Floor(perf.Audience/5m);

                // print line for this order
                result.AppendLine($"    {play.Name}: {thisAmount / 100:C} ({perf.Audience} seats)");
                totalAmount += thisAmount;
            }

            result.AppendLine($"Amount owed is {totalAmount / 100:C}");
            result.AppendLine($"You earned {volumeCredits} credits");
            return result.ToString();
        }
    }

    class Plays : Dictionary<string, Play>
    {}

    class Play
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

    class Invoice
    {
        public string Customer { get; set; }
        public Performance[] Performances { get; set; }
    }

    public class Performance
    {
        public string PlayId { get; set; }
        public int Audience { get; set; }
    }
}
