using System;

namespace HotelAccounting
{
    public class AccountingModel : ModelBase
    {
        private double price;
        private int nightsCount;
        private double discount;
        private double total;

        public double Price
        {
            get { return price; }
            set
            {
                if (value < 0) throw new ArgumentException("Price must be non-negative");

                price = value;
                UpdateTotal();
                Notify(nameof(Price));
                Notify(nameof(Total));
            }
        }

        public int NightsCount
        {
            get { return nightsCount; }
            set
            {
                if (value <= 0) throw new ArgumentException("NightsCount must be positive");

                nightsCount = value;
                UpdateTotal();
                Notify(nameof(NightsCount));
                Notify(nameof(Total));
            }
        }

        public double Discount
        {
            get { return discount; }
            set
            {
                discount = value;
                UpdateTotal();
                Notify(nameof(Discount));
                Notify(nameof(Total));
            }
        }

        public double Total
        {
            get { return total; }
            set
            {
                if (value < 0) throw new ArgumentException("Total must be non-negative");

                if (price == 0 || nightsCount == 0)
                {
                    if (value != 0) throw new ArgumentException("Total must be zero");
                    discount = 0;
                }
                else discount = (1 - value / (price * nightsCount)) * 100;

                total = value;
                Notify(nameof(Total));
                Notify(nameof(Discount));
            }
        }

        private void UpdateTotal()
        {
            var newTotal = price * nightsCount * (1 - discount / 100);
            if (newTotal < 0) throw new ArgumentException("Total must be non-negative");
            total = newTotal;
        }
    }
}