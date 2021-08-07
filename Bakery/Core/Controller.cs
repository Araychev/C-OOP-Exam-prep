﻿using System.Collections.Generic;
using System.Linq;
using Bakery.Core.Contracts;
using Bakery.Models.BakedFoods;
using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables;
using Bakery.Models.Tables.Contracts;

namespace Bakery.Core
{
   public class Controller : IController
   {
       private List<IBakedFood> bakedFoods;
       private List<IDrink> drinks;
       private List<ITable> tables;
       private decimal totalIncome = 0;

       public Controller()
       {
           bakedFoods = new List<IBakedFood>();
           drinks = new List<IDrink>();
           tables = new List<ITable>();
       }

        public string AddFood(string type, string name, decimal price)
        {
            if (type == "Bread")
            {
                bakedFoods.Add(new Bread(name, price));
            }

            if (type == "Cake")
            {
                bakedFoods.Add(new Cake(name,price));
            }

            return $"Added {name} ({type}) to the menu";
        }

        public string AddDrink(string type, string name, int portion, string brand)
        {
            if (type == "Tea")
            {
                drinks.Add(new Tea(name,portion,brand));
            }

            if (type=="Water")
            {
                drinks.Add(new Water(name,portion,brand));
            }

            return $"Added {name} ({brand}) to the drink menu";
        }

        public string AddTable(string type, int tableNumber, int capacity)
        {

            if (type == "OutsideTable")
            {
                tables.Add(new OutsideTable(tableNumber,capacity));
            }

            if (type == "InsideTable")
            {
                tables.Add(new InsideTable(tableNumber,capacity));
            }

            return $"Added table number {tableNumber} in the bakery";
        }

        public string ReserveTable(int numberOfPeople)
        {
            ITable table = tables.FirstOrDefault(table => !table.IsReserved && table.Capacity >= numberOfPeople);

            if (table == null)
            {
                return $"No available table for {numberOfPeople} people";
            }
            else
            {
                table.Reserve(numberOfPeople);
                return $"Table {table.TableNumber} has been reserved for {numberOfPeople} people";
            }

        }

        public string OrderFood(int tableNumber, string foodName)
        {
            ITable table = tables.FirstOrDefault(table => table.TableNumber == tableNumber);

            if (table == null)
            {
                return $"Could not find table {tableNumber}";
            }
            else
            {
                IBakedFood food = bakedFoods.FirstOrDefault(food => food.Name == foodName);
                if (food == null)
                {
                    return $"No {foodName} in the menu";
                }
                else
                {
                    table.OrderFood(food);
                    return $"Table {tableNumber} ordered {foodName}";
                }
            }

        }

        public string OrderDrink(int tableNumber, string drinkName, string drinkBrand)
        {
            ITable table = tables.FirstOrDefault(table => table.TableNumber == tableNumber);

            if (table == null)
            {
                return $"Could not find table {tableNumber}";
            }
            else
            {
                IDrink drink = drinks.FirstOrDefault(drink => drink.Name == drinkName && drink.Brand == drinkBrand);
                if (drink == null)
                {
                    return $"There is no {drinkName} {drinkBrand} available";
                }
                else
                {
                    table.OrderDrink(drink);
                    return $"Table {tableNumber} ordered {drinkName} {drinkBrand}";
                }
            }
        }

        public string LeaveTable(int tableNumber)
        {
            ITable table = tables.FirstOrDefault(table => table.TableNumber == tableNumber);

           var bill = table.GetBill();
            totalIncome += bill;
            table.Clear();

            return $"Table: {tableNumber}\r\n" +
                   $"Bill: {bill:f2}";
        }

        public string GetFreeTablesInfo()
        {
            string result = "";
            List<ITable> freeTables = tables.Where(table => !table.IsReserved).ToList();
            for (int i = 0; i < freeTables.Count; i++)
            {
                result += freeTables[i].GetFreeTableInfo() + "\r\n";
            }

            return result.TrimEnd();
        }

        public string GetTotalIncome()
        {
            return $"Total income: {totalIncome:f2}lv";
        }
    }
}