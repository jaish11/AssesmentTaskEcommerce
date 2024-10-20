// src/app/models/cart.ts
import { Product } from './product'; // Ensure correct import path

export interface Cart {
  cartID: number;
  userID: number;
  totalPrice: number;
  cartItems: CartItem[];
}

export interface CartItem {
  cartItemID: number;
  productID: number;
  quantity: number;
  product: Product;  // Correctly reference Product interface here
}
