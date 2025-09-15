using Bookify.Domain.Apartments;
using Bookify.Domain.Shared;

namespace Bookify.Domain.Bookings;

public class PricingService
{
    public PricingDetails CalculatePrice(Apartment apartment, DateRange dateRange)
    {
        var currency = apartment.Price.Currency;
        
        // Price for a period
        var priceForPeriod = new Money(
            apartment.Price.Amount * dateRange.LengthInDays,
            currency);
        
        // Amenities up charge
        var percentageUpCharge = apartment.Amenities
            .Sum(amenity => amenity switch
            {
                Amenity.GardenView or Amenity.MountainView => 0.05m,
                Amenity.AirConditioning => 0.01m,
                Amenity.Parking => 0.01m,
                _ => 0m
            });

        var amenityUpCharge = Money.Zero(currency);
        if (percentageUpCharge > 0)
            amenityUpCharge = new Money(
                priceForPeriod.Amount * percentageUpCharge,
                currency);
        
        // Total price
        var totalPrice = Money.Zero(currency);
        totalPrice += priceForPeriod;
        
        if (!apartment.CleaningFee.IsZero())
            totalPrice += apartment.CleaningFee;
        
        totalPrice += amenityUpCharge;
        
        return new PricingDetails(
            priceForPeriod,
            apartment.CleaningFee,
            amenityUpCharge,
            totalPrice);
    }
}