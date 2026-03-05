namespace MoustafaApp.Server.Validators
{
    public class CartValidator
    {
        // يتنادي في كل عمليات الكارت
        public void Validate(Cart cart)
        {
            ValidateQuantity(cart);
        }

        // خاص بمرحلة الـ Checkout فقط
        public void ValidateForCheckout(Cart cart)
        {
            if (!cart.CartItems.Any())
                throw new Exception("Cannot checkout empty cart.");

            ValidateQuantity(cart);
        }

        private void ValidateQuantity(Cart cart)
        {
            foreach (var item in cart.CartItems)
            {
                if (item.Quantity <= 0)
                    throw new Exception("Invalid quantity in cart.");
            }
        }
    }
}