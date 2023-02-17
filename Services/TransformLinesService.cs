using DataProcessing.Models;

namespace DataProcessing.Services
{
    public static class TransformLinesService
    {
        public static List<PaymentTransaction> transformLinesToPaymentTransaction(List<string> lines)
        {
            List<PaymentTransaction> listObject = new List<PaymentTransaction>();
            Parallel.ForEach(lines, line =>
            {
                try
                {
                    line = line.Replace("“", "").Replace("\"", "").Replace("”", "");
                    List<string> mas = line.Split(new char[] { ',' }).ToList<string>();
                    mas.ForEach(x => { x.Trim(); });

                    var temp = mas.Skip(2).Where(x => x == "").FirstOrDefault();
                    if (temp != null)
                        throw new Exception();

                    string name = mas[0] != "" ? mas[0] : mas[1];
                    if (name == "")
                        throw new Exception();

                    Payer payer = new Payer() { Name = name, Account_number = Convert.ToInt64(mas[7]), Date = mas[6], Payment = Convert.ToDecimal(mas[5].Replace(".", ",")) };

                    var city = listObject.FirstOrDefault(x => x.City == mas[2]);
                    if (city != null)
                    {
                        var service = city.Services.FirstOrDefault(x => x.Name == mas[8]);
                        if (service != null)
                        {
                            service.Payers.Add(payer);
                            service.Total += payer.Payment;
                            service.Total = service.Payers.Sum(x => x.Payment);
                        }
                        else
                        {
                            Service serviceNew = new Service() { Name = mas[8], Total = payer.Payment, Payers = new List<Payer>() };
                            serviceNew.Payers.Add(payer);
                            city.Services.Add(serviceNew);
                        }
                        city.Total = city.Services.Sum(x => x.Total);
                    }
                    else
                    {
                        PaymentTransaction paymentTransaction = new PaymentTransaction() { City = mas[2], Total = payer.Payment, Services = new List<Service>() };

                        Service service = new Service() { Name = mas[8], Payers = new List<Payer>() };
                        service.Payers.Add(payer);
                        service.Total = payer.Payment;

                        paymentTransaction.Services.Add(service);
                        listObject.Add(paymentTransaction);
                    }
                    MetaData.Parsed_lines++;
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                    MetaData.Found_errors++;
                }
            });

            return listObject;
        }
    }
}
