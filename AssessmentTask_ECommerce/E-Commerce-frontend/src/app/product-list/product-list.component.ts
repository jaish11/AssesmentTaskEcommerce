import { Component, OnInit } from '@angular/core';
import { ProductService } from '../product.service';
import { Product } from '../models/product';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';  // Import FormsModule

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, FormsModule],  // Add FormsModule here
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css'],
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];

  // Variables for adding a new product
  newProductName: string = '';
  newProductPrice: number = 0;
  newProductDescription: string = '';

  // Variables for editing a product
  updatedProductName: string = '';
  updatedProductPrice: number = 0;
  updatedProductDescription: string = '';
  editingProductId: number | null = null;

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    // this.productService.getProducts().subscribe((products) => (this.products = products));
    this.productService.getProducts().subscribe((products)=>(this.products=products))
  }

  addProduct(): void {
    const newProduct: Product = {
      productID: 0,
      productName: this.newProductName,
      price: this.newProductPrice,
      description: this.newProductDescription,
    };
  
    this.productService.addProduct(newProduct).subscribe({
      next: () => {
        console.log('Product added successfully');
        this.newProductName = '';
        this.newProductPrice = 0;
        this.newProductDescription = '';
        this.loadProducts(); // Reload the product list to reflect the new product
      },
      error: (error) => {
        console.error('Error adding product:', error);
        alert('Error adding product. Please try again.'); // Alert user for UI feedback
      }
    });
  }
  

  editProduct(id: number): void {
    const productToEdit = this.products.find(p => p.productID === id);
    if (productToEdit) {
      this.updatedProductName = productToEdit.productName;
      this.updatedProductPrice = productToEdit.price;
      this.updatedProductDescription = productToEdit.description;
      this.editingProductId = id;
    }
  }

  updateProduct(id: number): void {
    const updatedProduct: Product = {
      productID: id,
      productName: this.updatedProductName,
      price: this.updatedProductPrice,
      description: this.updatedProductDescription,
    };

    this.productService.updateProduct(id, updatedProduct).subscribe(
      () => {
        console.log('Product updated successfully');
        this.editingProductId = null;
        this.loadProducts();
      },
      (error) => {
        console.error('Error updating product:', error);
      }
    );
  }

  deleteProduct(id: number): void {
    this.productService.deleteProduct(id).subscribe(() => this.loadProducts());
  }
}
