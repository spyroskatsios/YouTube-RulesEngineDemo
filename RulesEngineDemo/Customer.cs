namespace RulesEngineDemo;

public class Customer
{
    public required string Name { get; set; }
    public Plan Plan { get; set; }
    public int Years { get; set; }
    public required Coupon Coupon { get; set; }
}

public enum Plan
{
    Phone = 1,
    Internet = 2,
    PhoneAndInternet = 3
}

public record Coupon(decimal Discount);