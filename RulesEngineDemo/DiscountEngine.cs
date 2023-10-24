using System.Reflection;

namespace RulesEngineDemo;

public interface IDiscountRule
{
    public DiscountRule Type { get; }
    decimal Apply(Customer customer, decimal currentDiscount);
}

public enum DiscountRule
{
    PlanDiscountRule,
    LoyaltyDiscountRule,
    CouponDiscountRule
}

public class DiscountEngine
{
    private readonly IEnumerable<IDiscountRule> _rules;

    public DiscountEngine()
    {
        _rules = GetRules();
    }

    public decimal Run(Customer customer)
    {
        var currentDiscount = 0m;

        foreach (var rule in _rules)
        {
            currentDiscount = rule.Apply(customer, currentDiscount);
        }

        return currentDiscount;
    }

    private static IEnumerable<IDiscountRule> GetRules()
    {
        var type = typeof(IDiscountRule);

        var rules = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => type.IsAssignableFrom(x) && !x.IsInterface)
            .Select(x => (IDiscountRule)Activator.CreateInstance(x)!)
            .OrderBy(x=>x.Type)
            .ToList();

        return rules;
    }
}

public class PlanDiscountRule : IDiscountRule
{
    public DiscountRule Type => DiscountRule.PlanDiscountRule;

    public decimal Apply(Customer customer, decimal currentDiscount)
        => customer.Plan is Plan.Internet or Plan.Phone 
            ? currentDiscount + 0.10m 
            : currentDiscount + 0.15m;
}

public class LoyaltyDiscountRule : IDiscountRule
{
    public DiscountRule Type => DiscountRule.LoyaltyDiscountRule;
    public decimal Apply(Customer customer, decimal currentDiscount)
        => customer.Years > 3 ? currentDiscount + 0.05m : currentDiscount;
} 

public class CouponDiscountRule : IDiscountRule
{
    public DiscountRule Type => DiscountRule.CouponDiscountRule;
    
    public decimal Apply(Customer customer, decimal currentDiscount)
    {
        if (customer.Years < 5)
            return customer.Coupon.Discount + currentDiscount > 0.2m
                ? 0.2m
                : customer.Coupon.Discount + currentDiscount;
        
        return customer.Coupon.Discount + currentDiscount > 0.25m
            ? 0.25m
            : customer.Coupon.Discount + currentDiscount;
    }
}