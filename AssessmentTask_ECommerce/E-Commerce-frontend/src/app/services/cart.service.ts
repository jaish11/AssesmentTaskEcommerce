import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cart } from '../models/cart';  // Assuming you will create this model later
import { CartItem } from '../models/cart';  // Assuming you will create this model as well

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private apiUrl = 'http://localhost:5182/cart'; // Change to your actual API URL

  constructor(private http: HttpClient) {}

  /**
   * Get the user's cart based on their ID.
   * @param userId - The user's ID to fetch their cart
   */
  getCart(userId: number): Observable<Cart> {
    return this.http.get<Cart>(`${this.apiUrl}/${userId}`);
  }

  /**
   * Add a product to the user's cart.
   * @param userId - The user's ID
   * @param cartItem - The cart item (product ID, quantity)
   */
  addToCart(userId: number, cartItem: CartItem): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${userId}`, cartItem);
  }

  /**
   * Update the quantity of a product in the cart.
   * @param userId - The user's ID
   * @param productId - The product ID to update
   * @param quantity - The new quantity
   */
  updateQuantity(userId: number, productId: number, quantity: number): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/update/${userId}`, { productId, quantity });
  }

  /**
   * Remove a product from the user's cart.
   * @param userId - The user's ID
   * @param productId - The product ID to remove
   */
  removeItem(userId: number, productId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/remove/${userId}/${productId}`);
  }

  /**
   * Apply a discount code to the user's cart.
   * @param userId - The user's ID
   * @param discountCode - The discount code
   */
  applyDiscount(userId: number, discountCode: string): Observable<Cart> {
    return this.http.post<Cart>(`${this.apiUrl}/discount/${userId}`, { discountCode });
  }
}
