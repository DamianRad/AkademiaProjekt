using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KreatorZamowien
{
    public enum Sets {Nothing, Pizza, Hamburger, Fries, HamburgerWithFries, Cheeseburger, CheeseburgerWithFries }
    public enum Drinks { Water, Cola, Pepsi, Fanta, Mirinda }
   
    public class Order
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int TableNumber { get; set; }
        public Sets Sets { get; set; }
        public Drinks Drinks { get; set; }
        public Order()
        {

        }
        public Order(string name, string lastName, int tableNumber, Sets sets, Drinks drinks)
        {
            this.Name = name;
            this.LastName = lastName;
            this.TableNumber = tableNumber;
            this.Sets = sets;
            this.Drinks = drinks;
        }
    }
}
