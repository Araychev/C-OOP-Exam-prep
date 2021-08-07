using System;
using System.Collections.Generic;
using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables.Contracts;
using Bakery.Utilities.Messages;

namespace Bakery.Models.Tables
{
    public abstract class Table : ITable
    {
        private List<IBakedFood> foodOrders;
        private List<IDrink> drinkOrders;
        private int _tableNumber;
        private int _capacity;
        private int _numberOfPeople;
        private decimal _pricePerPerson;

        public Table(int tableNumber, int capacity, decimal pricePerPerson)
        {
            foodOrders = new List<IBakedFood>();
            drinkOrders = new List<IDrink>();
            TableNumber = tableNumber;
            Capacity = capacity;
            PricePerPerson =pricePerPerson;
        }

        public int TableNumber
        {
            get => _tableNumber;
           private set => _tableNumber = value;
        }

        public int Capacity
        {
            get => _capacity;

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidTableCapacity);
                }
                _capacity = value;
            }

        }

        public int NumberOfPeople
        {
            get => _numberOfPeople;

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidNumberOfPeople);
                }

                _numberOfPeople = value;
            }
        }

        public decimal PricePerPerson
        {
            get => _pricePerPerson;
           private set => _pricePerPerson = value;
        }

        public bool IsReserved { get; private set; }

        public decimal Price => _numberOfPeople*_pricePerPerson;

        public void Reserve(int numberOfPeople)
        {
            IsReserved = true;
            NumberOfPeople = numberOfPeople;
        }

        public void OrderFood(IBakedFood food)
        {
           foodOrders.Add(food);
        }

        public void OrderDrink(IDrink drink)
        {
            drinkOrders.Add(drink);
        }

        public decimal GetBill()
        {
            decimal bill = 0;
            foreach (var food in foodOrders)
            {
                bill += food.Price;
            }

            foreach (var drink in drinkOrders)
            {
                bill += drink.Price;
            }

            bill += Price;
            return bill;
        }

        public void Clear()
        {
            foodOrders.Clear();
            drinkOrders.Clear();
            _numberOfPeople = 0;
            IsReserved = false;
        }

        public string GetFreeTableInfo()
        {
            return $"Table: {TableNumber}\r\n"+
                   $"Type: {GetType().Name}\r\n"+
                    $"Capacity: {Capacity}\r\n"+
                   $"Price per Person: {PricePerPerson}";
        }
    }
}
