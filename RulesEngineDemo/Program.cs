using RulesEngineDemo;

var discountService = new DiscountService();

var customers = new List<Customer>
{
    new() // 0.2
    {
        Name = "Customer1",
        Plan = Plan.Internet,
        Years = 4,
        Coupon = new Coupon(0.05m)
    },
    new() // 0.2
    {
        Name = "Customer2",
        Plan = Plan.Phone,
        Years = 3,
        Coupon = new Coupon(0.15m)
    },
    new() // 0.25
    {
        Name = "Customer3",
        Plan = Plan.PhoneAndInternet,
        Years = 6,
        Coupon = new Coupon(0.15m)
    }
};

var engine = new DiscountEngine();

foreach (var customer in customers)
{
    var discount = engine.Run(customer);
    Console.WriteLine($"{customer.Name} has a discount of {discount}");
}

Console.ReadKey();