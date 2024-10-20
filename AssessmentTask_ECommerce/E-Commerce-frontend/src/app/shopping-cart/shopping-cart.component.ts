import { Component, OnInit } from '@angular/core';
import { CartService } from '../services/cart.service';
import { Cart } from '../models/cart';
import { Product } from '../models/product';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {
  cart!: Cart;  // Use definite assignment assertion
  discountCode!: string;  // Use definite assignment assertion
  discountedTotal!: number;  // Use definite assignment assertion

  constructor(private cartService: CartService) {}

  ngOnInit() {
    this.loadCart();
  }

  loadCart() {
    const userId = 1;  // Example user ID, change to your dynamic user ID
    this.cartService.getCart(userId).subscribe(cart => {
      this.cart = cart;
    });
  }

  updateQuantity(productId: number, quantity: number) {
    const userId = 1;  // Example user ID, change to your dynamic user ID
    this.cartService.updateQuantity(userId, productId, quantity).subscribe(() => {
      this.loadCart();
    });
  }

  removeItem(productId: number) {
    const userId = 1;  // Example user ID
    this.cartService.removeItem(userId, productId).subscribe(() => {
      this.loadCart();
    });
  }

  applyDiscount() {
    const userId = 1;  // Example user ID
    this.cartService.applyDiscount(userId, this.discountCode).subscribe(cart => {
      this.cart = cart;
      this.discountedTotal = cart.totalPrice;
    });
  }
}
