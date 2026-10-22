using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
namespace HeThongBanLeThienAn.Tests;
internal static class CustomerFixtures
{
    public static void Add(SupermarketDbContext db)
    {
        for (int i=1;i<=15;i++)
            if (!db.Customers.Any(c=>c.CustomerId==i)) db.Customers.Add(new Customer {
                CustomerId=i,CustomerName=$"Khách mẫu {i:00}",PhoneNumber=$"DEMO-KH-{i:000}",
                RewardPoints=i==1?120:0,MembershipRank="Chuẩn" });
        db.SaveChanges();
    }
}
