using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPrj.DAL.Entities;

namespace WebPrj.Models
{
    ///<summary>
    ///Класс описывающий корзину товаров
    /// </summary>
    public class Cart
    {
        public Dictionary<int, CartItem> Items { get; set; }

        public Cart() 
        {
            Items = new Dictionary<int, CartItem>();
        }

        ///<summary>
        ///Количество объектов в корзине
        /// </summary>
        public int Count 
        {
            get 
            {
                return Items.Sum(item => item.Value.Quantity);
            }
        }

        ///<summary>
        ///Добавление в корзину
        /// </summary>
        /// <param name="_laptop">Добавляемый объект</param>
        public virtual void AddToCart(Laptop _laptop) 
        {
            //если такой объект уже есть в корзине, то увеличить его количество
            if (Items.ContainsKey(_laptop.LaptopId)) Items[_laptop.LaptopId].Quantity++;

            //иначе, добавить объект в корзину
            else Items.Add(_laptop.LaptopId, new CartItem { laptop = _laptop, Quantity = 1 });
        }

        ///<summary>
        ///Удаление объекта из корзины
        /// </summary>
        /// <param name="id">id удаляемого объекта</param>
        public virtual void RemoveFromCart(int id)
        {
            Items.Remove(id);
        }

        ///<summary>
        ///Очистка корзины
        /// </summary>
        public virtual void ClearAll()
        {
            Items.Clear();
        }
    }

    ///<summary>
    ///Класс описывающий одну позицию в корзине
    /// </summary>
    public class CartItem 
    {
        public Laptop laptop { get; set; } 
        public int Quantity { get; set; }
    }
}
