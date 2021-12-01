using UnityEngine.UI;

namespace Eat
{
    public abstract class Product
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public Image Icon { get; set; }
        public string Description { get; set; }
        public bool IsAvaliable { get; set; }
        public abstract void Purchase();
    }
}

