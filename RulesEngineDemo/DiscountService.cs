namespace RulesEngineDemo;

public class DiscountService
{
    public decimal Calculate(Customer customer)
    {
        var discount = 0m;
        
        if (customer.Plan is Plan.Internet or Plan.Phone)
        {
            discount += 0.10m;
        }
        else
        {
            discount += 0.15m;
        }

        if (customer.Years > 3)
        {
            discount += 0.05m;
        }
        
        if (customer.Years < 5)
        {
            if (customer.Coupon.Discount + discount > 0.2m)
            {
                discount = 0.2m;
            }
            else
            {
                discount += customer.Coupon.Discount;
            }
        }
        else
        {
            if (customer.Coupon.Discount + discount > 0.25m)
            {
                discount = 0.25m;
            }
            else
            {
                discount += customer.Coupon.Discount;
            }
        }

        return discount;
    }
}