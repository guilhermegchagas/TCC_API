using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Market_WebAPI.Models
{
    public class CarrinhoReciclagem
    {
        public List<ItemReciclagem> listaItems = new List<ItemReciclagem>();

        public void AddItem(ItemReciclagem item)
        {
            listaItems.Add(item);
        }

        public IEnumerable<ItemReciclagem> GetItems()
        {
            return listaItems;
        }

        public double GetSum()
        {
            return listaItems.Sum(item => item.vl_item_reciclagem);
        }
    }
}